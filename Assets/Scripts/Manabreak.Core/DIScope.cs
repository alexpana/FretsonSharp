using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.PubSub;
using Messaging;
using Sirenix.Utilities;
using UnityEngine;

namespace Core
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor)]
    public sealed class Inject : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class Component : Attribute
    {
    }

    // TODO: try to automatically ignore assemblies not dependencies of the main assembly (the assembly of the main controller class)
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MainController : Attribute
    {
        public Type[] AdditionalTypes = new Type[0];
        public string[] IgnoredAssemblies;
        public string[] IgnoredTypes;
    }

    #region Exceptions

    public class NoInstanceToInjectException : Exception
    {
        public NoInstanceToInjectException(string message) : base(message)
        {
        }
    }

    public class InvalidMainController : Exception
    {
        public InvalidMainController(Type controllerType) :
            base($"Class {controllerType} is not annotated with [MainController]")
        {
        }
    }

    public class ScopeNotFinalizedException : Exception
    {
        public ScopeNotFinalizedException(string message) : base(message)
        {
        }
    }

    public class InstanceAlreadyExistsException : Exception
    {
        public InstanceAlreadyExistsException(string message) : base(message)
        {
        }
    }

    #endregion

    public interface IInstanceResolver
    {
        T Resolve<T>() where T : class;
        T Resolve<T>(string name) where T : class;
        void InjectIn(object obj);
        void InjectIn(GameObject obj);
    }

    internal struct NamedInstance
    {
        public string Name;
        public object Instance;
    }

    public static class DI
    {
        public static T Resolve<T>(string name) where T : class
        {
            return DIScope.Root.Resolve<T>(name);
        }

        public static T Resolve<T>() where T : class
        {
            return DIScope.Root.Resolve<T>();
        }

        public static T Resolve<T>(Type type) where T : class
        {
            Debug.Log($"Resolving generic type {typeof(T)} with actual type {type}");
            var method = typeof(DIScope).GetMethod(nameof(DIScope.Resolve), new Type[] { });
            var bindMethod = method.MakeGenericMethod(type);
            return (T)bindMethod.Invoke(DIScope.Root, new object[0]);
        }

        public static GameObject ResolvePrefab(string name)
        {
            // TODO: we can cache the PrefabManager to reduce the number of resolves at runtime
            return DIScope.Root.Resolve<PrefabManager>().GetPrefab(name);
        }

        public static GameObject ResolvePrefab(int id)
        {
            return DIScope.Root.Resolve<PrefabManager>().GetPrefab(id);
        }

        public static IPublisher<T> ResolvePublisher<T>() where T : IMessage
        {
            return Resolve<IPublisher<T>>();
        }

        public static ISubscriber<T> ResolveSubscriber<T>() where T : IMessage
        {
            return Resolve<ISubscriber<T>>();
        }
    }

    public sealed class DIScope : IInstanceResolver, IDisposable
    {
        private readonly DisposableGroup m_DisposableGroup = new();

        private readonly Dictionary<Type, LazyBindDescriptor>
            m_LazyBindDescriptors = new();

        private readonly HashSet<object> m_ObjectsWithInjectedDependencies = new();

        private readonly DIScope m_Parent;

        private readonly Dictionary<Type, List<NamedInstance>> m_TypesToNamedInstances = new();

        private bool m_Disposed;

        private MainController m_mainController;

        private GameObject m_SceneObject;

        private bool m_ScopeConstructionComplete;
        private Assembly mainAssembly;

        public DIScope(DIScope parent = null)
        {
            m_Parent = parent;
        }

        public void Dispose()
        {
            if (!m_Disposed)
            {
                m_TypesToNamedInstances.Clear();
                m_ObjectsWithInjectedDependencies.Clear();
                m_DisposableGroup.Dispose();
                m_Disposed = true;
            }
        }

        public T Resolve<T>(string name) where T : class
        {
            var type = typeof(T);

            if (!m_ScopeConstructionComplete)
                throw new ScopeNotFinalizedException(
                    $"Trying to Resolve type {type}, but the DISCope is not yet finalized! You should call FinalizeScopeConstruction before any of the Resolve calls.");

            // if we have this type as lazy-bound instance - we are going to instantiate it now
            if (m_LazyBindDescriptors.TryGetValue(type, out var lazyBindDescriptor))
            {
                var instance = (T)InstantiateLazyBoundObject(lazyBindDescriptor);
                m_LazyBindDescriptors.Remove(type);
                foreach (var interfaceType in lazyBindDescriptor.InterfaceTypes)
                    m_LazyBindDescriptors.Remove(interfaceType);

                return instance;
            }

            if (!m_TypesToNamedInstances.TryGetValue(type, out var instances))
            {
                if (m_Parent != null) return m_Parent.Resolve<T>();

                throw new NoInstanceToInjectException($"Injection of type {type} failed.");
            }

            foreach (var namedInstance in instances)
                if (namedInstance.Name.Equals(name))
                    return (T)namedInstance.Instance;

            throw new NoInstanceToInjectException($"Injection of type {type} failed.");
        }

        public T Resolve<T>() where T : class
        {
            return Resolve<T>(typeof(T).Name);
        }

        public void InjectIn(object obj)
        {
            if (obj == null) return;

            if (m_ObjectsWithInjectedDependencies.Contains(obj)) return;

            if (CachedReflectionUtility.TryGetInjectableMethod(obj.GetType(), out var injectionMethod))
            {
                var parameters = CachedReflectionUtility.GetMethodParameters(injectionMethod);

                var paramColleciton = new object[parameters.Length];

                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];

                    var genericResolveMethod = CachedReflectionUtility.GetTypedResolveMethod(parameter.ParameterType);
                    var resolved = genericResolveMethod.Invoke(this, null);
                    paramColleciton[i] = resolved;
                }

                injectionMethod.Invoke(obj, paramColleciton);
                m_ObjectsWithInjectedDependencies.Add(obj);
            }
        }

        public void InjectIn(GameObject go)
        {
            var components = go.GetComponentsInChildren<Component>(true);

            foreach (var component in components) InjectIn(component);
        }

        ~DIScope()
        {
            Dispose();
        }

        public void BindFields(object o)
        {
            var type = o.GetType();
            foreach (var fieldInfo in type.GetFields())
            {
                var autoBind = fieldInfo.GetAttribute<AutoBind>();
                if (autoBind != null)
                {
                    var objectName = autoBind.Name;
                    if (objectName == null) objectName = fieldInfo.Name;

                    var memberValue = fieldInfo.GetMemberValue(o);
                    if (memberValue == null) Debug.LogWarning($"AutoBind field '{objectName}' has null value!");

                    BindNamedInstanceToType(memberValue, objectName, fieldInfo.FieldType);
                }
            }
        }

        public void BindInstanceAsSingle<T>(T instance) where T : class
        {
            BindInstanceToType(instance, typeof(T));
        }

        public void BindInstanceAsSingle<T>(T instance, string name) where T : class
        {
            // Debug.Log($"BindInstanceAsSingle({name}, {instance})");
            BindNamedInstanceToType(instance, name, typeof(T));
        }

        public void BindInstanceAsSingle<TImplementation, TInterface>(TImplementation instance)
            where TImplementation : class, TInterface
            where TInterface : class
        {
            BindInstanceAsSingle<TInterface>(instance);
            BindInstanceAsSingle(instance);
        }

        public void BindInstanceInterfacesOnlyAsSingle<TImplementation, TInterface1, TInterface2>(
            TImplementation instance)
            where TImplementation : class, TInterface1, TInterface2
            where TInterface1 : class
            where TInterface2 : class
        {
            BindInstanceAsSingle<TInterface1>(instance);
            BindInstanceAsSingle<TInterface2>(instance);
        }

        public void BindInstanceAsSingle<TImplementation, TInterface, TInterface2, TInterface3>(
            TImplementation instance)
            where TImplementation : class, TInterface, TInterface2, TInterface3
            where TInterface : class
            where TInterface2 : class
            where TInterface3 : class
        {
            BindInstanceAsSingle<TInterface>(instance);
            BindInstanceAsSingle<TInterface2>(instance);
            BindInstanceAsSingle<TInterface3>(instance);
            BindInstanceAsSingle(instance);
        }

        private void BindInstanceToType(object instance, Type type)
        {
            if (IsIgnored(type))
            {
                Debug.Log($"Ignoring {type} since it's ignored by the main controller");
                return;
            }

            BindNamedInstanceToType(instance, type.Name, type);
        }

        private void BindNamedInstanceToType(object instance, string name, Type type)
        {
            if (IsIgnored(type))
            {
                LogIgnoredType(type);
                return;
            }

            List<NamedInstance> namedInstances;
            if (!m_TypesToNamedInstances.TryGetValue(type, out namedInstances))
            {
                namedInstances = new List<NamedInstance>();
                m_TypesToNamedInstances[type] = namedInstances;
            }

            foreach (var namedInstance in namedInstances)
                if (namedInstance.Name.Equals(name))
                {
                    Debug.LogWarning($"Instance with name {name} is already bound to type {type}, skipping");
                    return;
                }

            // Debug.Log($"BindNamedInstanceToType {name} - {type}");
            namedInstances.Add(new NamedInstance { Name = name, Instance = instance });
        }

        public void BindAsSingle<TImplementation, TInterface>()
            where TImplementation : class, TInterface
            where TInterface : class
        {
            LazyBind(typeof(TImplementation), typeof(TInterface));
        }

        public void BindAsSingle<TImplementation, TInterface, TInterface2>()
            where TImplementation : class, TInterface, TInterface2
            where TInterface : class
            where TInterface2 : class
        {
            LazyBind(typeof(TImplementation), typeof(TInterface), typeof(TInterface2));
        }

        public void BindAsSingle<TImplementation, TInterface, TInterface2, TInterface3>()
            where TImplementation : class, TInterface, TInterface2, TInterface3
            where TInterface : class
            where TInterface2 : class
            where TInterface3 : class
        {
            LazyBind(typeof(TImplementation), typeof(TInterface), typeof(TInterface2));
        }

        public void BindAsSingle<T>()
            where T : class
        {
            LazyBind(typeof(T));
        }

        private void LazyBind(Type type, params Type[] typeAliases)
        {
            if (IsIgnored(type))
            {
                LogIgnoredType(type);
                return;
            }

            var descriptor = new LazyBindDescriptor(type, typeAliases);

            foreach (var typeAlias in typeAliases) m_LazyBindDescriptors[typeAlias] = descriptor;

            m_LazyBindDescriptors[type] = descriptor;
        }

        private bool IsIgnored(Type type)
        {
            return m_mainController.IgnoredTypes?.Contains(type.Name) == true ||
                   m_mainController.IgnoredAssemblies?.Contains(type.Assembly.GetName().Name) == true;
        }

        private static void LogIgnoredType(Type type)
        {
            Debug.Log($"Ignoring {type} since it's ignored by the main controller");
        }

        private object InstantiateLazyBoundObject(LazyBindDescriptor descriptor)
        {
            object instance;
            if (CachedReflectionUtility.TryGetInjectableConstructor(descriptor.Type, out var constructor))
            {
                var parameters = GetResolvedInjectionMethodParameters(constructor);
                instance = constructor.Invoke(parameters);
            }
            else
            {
                instance = Activator.CreateInstance(descriptor.Type);
                InjectIn(instance);
            }

            AddToDisposableGroupIfDisposable(instance);

            BindInstanceToType(instance, descriptor.Type);

            if (descriptor.InterfaceTypes != null)
                foreach (var interfaceType in descriptor.InterfaceTypes)
                    BindInstanceToType(instance, interfaceType);

            return instance;
        }

        private void AddToDisposableGroupIfDisposable(object instance)
        {
            if (instance is IDisposable disposable) m_DisposableGroup.Add(disposable);
        }

        /// <summary>
        ///     This method forces the finalization of construction of DI Scope. It would inject all the instances passed to it
        ///     directly.
        ///     Objects that were bound by just type will be instantiated on their first use.
        /// </summary>
        public static void Initialize(object app)
        {
            Root = new DIScope();
            Root.InitializeInstance(app);
        }

        private void InitializeInstance(object app)
        {
            m_SceneObject = new GameObject("DI");

            m_mainController = app.GetType().GetAttribute<MainController>();
            mainAssembly = app.GetType().Assembly;

            if (m_mainController == null) throw new InvalidMainController(app.GetType());

            BindInstanceAsSingle<DIScope, IInstanceResolver>(Root);

            BindFields(app);
            AutoBindComponents();
            AutoBindMessagesChannels();

            if (m_ScopeConstructionComplete) return;

            m_ScopeConstructionComplete = true;

            var uniqueObjects = new HashSet<object>();
            foreach (var namedInstances in m_TypesToNamedInstances.Values)
            foreach (var namedInstance in namedInstances)
                uniqueObjects.Add(namedInstance.Instance);

            foreach (var objectToInject in uniqueObjects) InjectIn(objectToInject);
        }

        private void AutoBindComponents()
        {
            // Debug.Log($"Main assembly = {mainAssembly}");
            // AppDomain.CurrentDomain.GetAssemblies()
            //     .Where(a => mainAssembly.IsDependentOn(a))
            //     .ForEach(a => Debug.Log($"Discovered assembly {a}"));

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => mainAssembly.IsDependentOn(a))
                .SelectMany(s => s.GetTypes())
                .Where(t => t.GetAttribute<Component>() != null && !IsIgnored(t))
                .Concat(m_mainController.AdditionalTypes);

            var monoBehaviors = new List<Type>();
            var plainClasses = new List<Type>();

            foreach (var type in types)
                if (typeof(MonoBehaviour).IsAssignableFrom(type))
                    monoBehaviors.Add(type);
                else
                    plainClasses.Add(type);

            monoBehaviors.Sort((x, y) => ExecutionOrder(x) - ExecutionOrder(y));

            foreach (var type in monoBehaviors)
            {
                var component = m_SceneObject.AddComponent(type);
                BindNamedInstanceToType(component, type.Name, type);
                foreach (var typeInterface in type.GetInterfaces())
                    BindNamedInstanceToType(component, typeInterface.Name, typeInterface);
            }

            foreach (var type in plainClasses)
            {
                Debug.Log($"LazyBinding {type}");
                LazyBind(type, type.GetInterfaces());
            }
        }

        private static int ExecutionOrder(Type t)
        {
            var property = t.GetAttribute<DefaultExecutionOrder>();
            if (property != null) return property.order;

            return 0;
        }

        public void AutoBindMessagesChannels()
        {
            var type = typeof(IMessage);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (var declaringType in types)
            {
                var method = typeof(DIScope).GetMethod(nameof(BindInstanceInterfacesOnlyAsSingle));
                var messageChannelType = typeof(MessageChannel<>).MakeGenericType(declaringType);
                var publisherType = typeof(IPublisher<>).MakeGenericType(declaringType);
                var subscriberType = typeof(ISubscriber<>).MakeGenericType(declaringType);
                var bindMethod = method.MakeGenericMethod(messageChannelType, publisherType, subscriberType);
                bindMethod.Invoke(this, new[] { Activator.CreateInstance(messageChannelType) });
                // Debug.Log($"Bound message channel for {declaringType.Name}");
            }
        }

        private object[] GetResolvedInjectionMethodParameters(MethodBase injectionMethod)
        {
            var parameters = CachedReflectionUtility.GetMethodParameters(injectionMethod);

            var paramColleciton = new object[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];

                var genericResolveMethod = CachedReflectionUtility.GetTypedResolveMethod(parameter.ParameterType);
                var resolved = genericResolveMethod.Invoke(this, null);
                paramColleciton[i] = resolved;
            }

            return paramColleciton;
        }

        public void DebugContents()
        {
            Debug.Log("Contents of DI scope:");

            foreach (var bind in m_TypesToNamedInstances)
            foreach (var namedInstance in bind.Value)
                Debug.Log($"* {bind.Key} -> ({namedInstance.Name}, {namedInstance.Instance}");
        }

        private struct LazyBindDescriptor
        {
            public readonly Type Type;
            public readonly Type[] InterfaceTypes;

            public LazyBindDescriptor(Type type, Type[] interfaceTypes)
            {
                Type = type;
                InterfaceTypes = interfaceTypes;
            }
        }

        private static class CachedReflectionUtility
        {
            private static readonly Dictionary<Type, MethodBase> k_CachedInjectableMethods = new();

            private static readonly Dictionary<Type, ConstructorInfo> k_CachedInjectableConstructors = new();

            private static readonly Dictionary<MethodBase, ParameterInfo[]> k_CachedMethodParameters = new();

            private static readonly Dictionary<Type, MethodInfo> k_CachedResolveMethods = new();

            private static readonly Type k_InjectAttributeType =
                typeof(Inject);

            private static readonly HashSet<Type> k_ProcessedTypes = new();

            private static MethodInfo k_ResolveMethod;

            public static bool TryGetInjectableConstructor(Type type, out ConstructorInfo method)
            {
                CacheTypeMethods(type);
                return k_CachedInjectableConstructors.TryGetValue(type, out method);
            }

            /// <summary>
            ///     Inspect the type argument searching for constructors ande method annotated with the [Inject] attribute.
            ///     The methods, constructors and their argument types are cached
            /// </summary>
            /// <param name="type"></param>
            private static void CacheTypeMethods(Type type)
            {
                if (k_ProcessedTypes.Contains(type)) return;

                var constructors = type.GetConstructors();
                foreach (var constructorInfo in constructors)
                {
                    var foundInjectionSite = constructorInfo.IsDefined(k_InjectAttributeType);
                    if (foundInjectionSite)
                    {
                        k_CachedInjectableConstructors[type] = constructorInfo;
                        var methodParameters = constructorInfo.GetParameters();
                        k_CachedMethodParameters[constructorInfo] = methodParameters;
                        break;
                    }
                }

                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (var methodInfo in methods)
                {
                    var foundInjectionSite = methodInfo.IsDefined(k_InjectAttributeType);
                    if (foundInjectionSite)
                    {
                        k_CachedInjectableMethods[type] = methodInfo;
                        var methodParameters = methodInfo.GetParameters();
                        k_CachedMethodParameters[methodInfo] = methodParameters;
                        break;
                    }
                }

                k_ProcessedTypes.Add(type);
            }

            public static bool TryGetInjectableMethod(Type type, out MethodBase method)
            {
                CacheTypeMethods(type);
                return k_CachedInjectableMethods.TryGetValue(type, out method);
            }

            public static ParameterInfo[] GetMethodParameters(MethodBase injectionMethod)
            {
                return k_CachedMethodParameters[injectionMethod];
            }

            /// <summary>
            ///     Return a MethodInfo for <c>DIScope.Resove&lt;T&gt;</c> and cache the result
            /// </summary>
            /// <param name="parameterType">the type parameter of the Resolve method</param>
            /// <returns></returns>
            public static MethodInfo GetTypedResolveMethod(Type parameterType)
            {
                if (!k_CachedResolveMethods.TryGetValue(parameterType, out var resolveMethod))
                {
                    if (k_ResolveMethod == null) k_ResolveMethod = typeof(DIScope).GetMethod("Resolve", new Type[] { });

                    resolveMethod = k_ResolveMethod!.MakeGenericMethod(parameterType);
                    k_CachedResolveMethods[parameterType] = resolveMethod;
                }

                return resolveMethod;
            }
        }

        #region Singleton

        public static DIScope Root { get; private set; }

        #endregion
    }
}