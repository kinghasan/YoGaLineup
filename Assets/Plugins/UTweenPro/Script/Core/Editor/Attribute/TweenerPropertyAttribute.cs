#if UNITY_EDITOR
using System;
using System.Reflection;
using UnityEditor;

namespace Aya.TweenPro
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class TweenerPropertyAttribute : Attribute
    {
        public string PropertyName { get; set; }
        public TweenerPropertyAttribute()
        {
        }


        public TweenerPropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public static void CacheProperty(object target, SerializedObject targetObject)
        {
            CacheProperty(target, targetObject.FindProperty);
        }

        public static void CacheProperty(object target, SerializedProperty targetProperty)
        {
            CacheProperty(target, targetProperty.FindPropertyRelative);
        }

        public static void CacheProperty(object target, Func<string, SerializedProperty> propertyGetter)
        {
            var targetType = target.GetType();
            var filedInfoList = targetType.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var fieldInfo in filedInfoList)
            {
                var attribute = fieldInfo.GetCustomAttribute(typeof(TweenerPropertyAttribute)) as TweenerPropertyAttribute;
                if (attribute == null) continue;

                var propertyName = attribute.PropertyName;
                if (string.IsNullOrEmpty(propertyName))
                {
                    var suffix = "Property";
                    if (fieldInfo.Name.EndsWith(suffix))
                    {
                        propertyName = fieldInfo.Name.Substring(0, fieldInfo.Name.Length - suffix.Length);
                    }
                    else
                    {
                        propertyName = fieldInfo.Name;
                    }
                }

                var property = propertyGetter(propertyName);
                fieldInfo.SetValue(target, property);
            }
        }
    }
}
#endif