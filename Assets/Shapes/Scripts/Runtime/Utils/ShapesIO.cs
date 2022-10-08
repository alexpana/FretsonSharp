using System.Linq;
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

#endif

// Shapes © Freya Holmér - https://twitter.com/FreyaHolmer/
// Website & Documentation - https://acegikmo.com/shapes/
namespace Shapes
{
    internal static class ShapesIO
    {
#if UNITY_EDITOR

        private static string rootFolder = null;

        public static string RootFolder
        {
            get
            {
                if (rootFolder == null)
                {
                    var shapeAssetsPath = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t:ShapesAssets")[0]);
                    var fileNameLen = "/Resources/Shapes Assets.asset".Length;
                    rootFolder = shapeAssetsPath.Substring(0, shapeAssetsPath.Length - fileNameLen);
                }

                return rootFolder;
            }
        }

        private static string shaderFolder = null;
        public static string ShaderFolder => shaderFolder ?? (shaderFolder = RootFolder + "/Shaders");
        public static string CoreShaderFolder => ShaderFolder + "/Core";
        public static string GeneratedMaterialsFolder => ShaderFolder + "/Generated Materials";
        public static string GeneratedShadersFolder => ShaderFolder + "/Generated Shaders/Resources";
        public static string projectSettingsPath = "ProjectSettings/ProjectSettings.asset";

        private static string ConfigCsharpPath => RootFolder + "/Scripts/Runtime/ShapesConfig.cs";
        internal static string ConfigShadersPath => ShaderFolder + "/Shapes Config.cginc";

        public static void OpenConfigCsharp()
        {
            OpenAssetAtPath(ConfigCsharpPath);
        }

        public static void OpenConfigShaders()
        {
            OpenAssetAtPath(ConfigShadersPath);
        }

        public static TextAsset[] LoadCoreShaders()
        {
            return LoadAllAssets<TextAsset>(CoreShaderFolder).ToArray();
        }

        private static void OpenAssetAtPath(string path)
        {
            AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<Object>(path));
        }

        internal static bool IsUsingVcWithCheckoutEnabled => Provider.enabled && Provider.hasCheckoutSupport;

        internal static bool AssetCanBeEdited(Object asset)
        {
            return AssetDatabase.IsOpenForEdit(asset, StatusQueryOptions.UseCachedIfPossible);
        }

        internal static bool AssetCanBeEdited(string path)
        {
            return AssetDatabase.IsOpenForEdit(path, StatusQueryOptions.UseCachedIfPossible);
        }

        internal static bool AssetsCanBeEdited(params Object[] assets)
        {
            return assets.All(AssetCanBeEdited);
        }

        internal static bool AssetsCanBeEdited(params string[] assets)
        {
            return assets.All(AssetCanBeEdited);
        }

        private static bool TryMakeAssetsEditableByObject(Object[] assets)
        {
            if (AssetsCanBeEdited(assets))
                return true;
#if UNITY_2019_3_OR_NEWER
            var paths = assets.Select(AssetDatabase.GetAssetPath).ToArray();
            return AssetDatabase.MakeEditable(paths);
#else
			Task checkoutTask = Provider.Checkout( assets, CheckoutMode.Asset );
			checkoutTask.Wait();
			return checkoutTask.success;
#endif
        }

        private static bool TryMakeAssetsEditableByPath(string[] paths)
        {
            if (AssetsCanBeEdited(paths))
                return true;
#if UNITY_2019_3_OR_NEWER
            return AssetDatabase.MakeEditable(paths);
#else
			Task checkoutTask = Provider.Checkout( paths, CheckoutMode.Asset );
			checkoutTask.Wait();
			return checkoutTask.success;
#endif
        }

        internal static bool TryMakeAssetsEditable(params Object[] assets)
        {
            if (TryMakeAssetsEditableByObject(assets))
                return true;
            DisplayAssetUnlockFailDialog(assets.Select(AssetDatabase.GetAssetPath).ToArray());
            return false;
        }

        internal static bool TryMakeAssetsEditable(params string[] paths)
        {
            if (TryMakeAssetsEditableByPath(paths))
                return true;
            DisplayAssetUnlockFailDialog(paths);
            return false;
        }

        private static void DisplayAssetUnlockFailDialog(string[] paths)
        {
            var multiple = paths.Length > 1;
            var files = $"file{(multiple ? "s" : "")}";
            const string msg =
                "Shapes failed to access files, likely due to your version control system, please see the console for more info";
            EditorUtility.DisplayDialog($"Failed to open {files} for editing", msg, "weird but ok");
            var log = $"Shapes failed to access the following {files}:\n";
            paths.ForEach(x => log += x + "\n");
            Debug.LogWarning(log);
        }

        private static T LoadAssetWithGUID<T>(string guid) where T : Object
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        private static string[] FindAllAssetGUIDs<T>() where T : Object
        {
            return AssetDatabase.FindAssets($"t:{typeof(T).Name}");
        }

        private static string[] FindAllAssetGUIDs<T>(string path) where T : Object
        {
            return AssetDatabase.FindAssets($"t:{typeof(T).Name}", new[] { path });
        }

        public static T TryLoadSingletonAsset<T>() where T : Object
        {
            var guids = FindAllAssetGUIDs<T>();
            return guids.Length > 0 ? LoadAssetWithGUID<T>(guids[0]) : null;
        }

        public static IEnumerable<T> LoadAllAssets<T>(string path) where T : Object
        {
            return FindAllAssetGUIDs<T>(path).Select(LoadAssetWithGUID<T>);
        }

        private static IEnumerable<T> LoadAllAssets<T>() where T : Object
        {
            return FindAllAssetGUIDs<T>().Select(LoadAssetWithGUID<T>);
        }

#endif
    }
}