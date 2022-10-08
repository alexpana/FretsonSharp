using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battlehub.Utils
{
    public static class ExposeProperties
    {
        public static void Expose(PropertyField[] properties)
        {
            var emptyOptions = new GUILayoutOption[0];

            EditorGUILayout.BeginVertical(emptyOptions);

            foreach (var field in properties)
            {
                EditorGUILayout.BeginHorizontal(emptyOptions);

                switch (field.Type)
                {
                    case SerializedPropertyType.Integer:
                        field.SetValue(EditorGUILayout.IntField(field.Name, (int)field.GetValue(), emptyOptions));
                        break;

                    case SerializedPropertyType.Float:
                        field.SetValue(EditorGUILayout.FloatField(field.Name, (float)field.GetValue(), emptyOptions));
                        break;

                    case SerializedPropertyType.Boolean:
                        field.SetValue(EditorGUILayout.Toggle(field.Name, (bool)field.GetValue(), emptyOptions));
                        break;

                    case SerializedPropertyType.String:
                        field.SetValue(EditorGUILayout.TextField(field.Name, (string)field.GetValue(), emptyOptions));
                        break;

                    case SerializedPropertyType.Vector2:
                        field.SetValue(
                            EditorGUILayout.Vector2Field(field.Name, (Vector2)field.GetValue(), emptyOptions));
                        break;

                    case SerializedPropertyType.Vector3:
                        field.SetValue(
                            EditorGUILayout.Vector3Field(field.Name, (Vector3)field.GetValue(), emptyOptions));
                        break;


                    case SerializedPropertyType.Enum:
                        field.SetValue(EditorGUILayout.EnumPopup(field.Name, (Enum)field.GetValue(), emptyOptions));
                        break;

                    case SerializedPropertyType.ObjectReference:
                        field.SetValue(EditorGUILayout.ObjectField(field.Name, (Object)field.GetValue(),
                            field.GetPropertyType(), true, emptyOptions));
                        break;
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }

        public static PropertyField[] GetProperties(object obj)
        {
            var fields = new List<PropertyField>();

            var infos = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var info in infos)
            {
                if (!(info.CanRead && info.CanWrite))
                    continue;

                var attributes = info.GetCustomAttributes(true);

                var isExposed = false;

                foreach (var o in attributes)
                    if (o.GetType() == typeof(ExposePropertyAttribute))
                    {
                        isExposed = true;
                        break;
                    }

                if (!isExposed)
                    continue;

                var type = SerializedPropertyType.Integer;

                if (PropertyField.GetPropertyType(info, out type))
                {
                    var field = new PropertyField(obj, info, type);
                    fields.Add(field);
                }
            }

            return fields.ToArray();
        }
    }


    public class PropertyField
    {
        private readonly MethodInfo m_Getter;
        private readonly PropertyInfo m_Info;
        private readonly object m_Instance;
        private readonly MethodInfo m_Setter;

        public PropertyField(object instance, PropertyInfo info, SerializedPropertyType type)
        {
            m_Instance = instance;
            m_Info = info;
            Type = type;

            m_Getter = m_Info.GetGetMethod();
            m_Setter = m_Info.GetSetMethod();
        }

        public SerializedPropertyType Type { get; }

        public string Name => ObjectNames.NicifyVariableName(m_Info.Name);

        public object GetValue()
        {
            return m_Getter.Invoke(m_Instance, null);
        }

        public void SetValue(object value)
        {
            m_Setter.Invoke(m_Instance, new[] { value });
        }

        public Type GetPropertyType()
        {
            return m_Info.PropertyType;
        }

        public static bool GetPropertyType(PropertyInfo info, out SerializedPropertyType propertyType)
        {
            propertyType = SerializedPropertyType.Generic;

            var type = info.PropertyType;

            if (type == typeof(int))
            {
                propertyType = SerializedPropertyType.Integer;
                return true;
            }

            if (type == typeof(float))
            {
                propertyType = SerializedPropertyType.Float;
                return true;
            }

            if (type == typeof(bool))
            {
                propertyType = SerializedPropertyType.Boolean;
                return true;
            }

            if (type == typeof(string))
            {
                propertyType = SerializedPropertyType.String;
                return true;
            }

            if (type == typeof(Vector2))
            {
                propertyType = SerializedPropertyType.Vector2;
                return true;
            }

            if (type == typeof(Vector3))
            {
                propertyType = SerializedPropertyType.Vector3;
                return true;
            }

            if (type.IsEnum)
            {
                propertyType = SerializedPropertyType.Enum;
                return true;
            }

            // COMMENT OUT to NOT expose custom objects/types
            propertyType = SerializedPropertyType.ObjectReference;
            return true;

            //return false;
        }
    }
}