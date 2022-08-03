/////////////////////////////////////////////////////////////////////////////
//
//  Script   : TypeExtension.cs
//  Info     : Type 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Aya.Extension
{
    public static class TypeExtension
    {
        #region Cache

        public static Assembly Assembly { get; private set; }
        public static Type[] Types { get; private set; }

        static TypeExtension()
        {
            Assembly = Assembly.GetExecutingAssembly();
            Types = Assembly.GetTypes();
        }

        #endregion

        #region Attribute

        /// <summary>
        /// 获取所有指定Attribute的字段
        /// </summary>
        /// <typeparam name="T">Attribute类型</typeparam>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static Dictionary<FieldInfo, List<T>> GetFieldsWithAttribute<T>(this Type type) where T : Attribute
        {
            var attrType = typeof(T);
            var list = new Dictionary<FieldInfo, List<T>>();
            var fieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            for (var i = 0; i < fieldInfos.Length; i++)
            {
                var fieldInfo = fieldInfos[i];
                if (fieldInfo.IsDefined(attrType, false))
                {
                    list.Add(fieldInfo, new List<T>());
                    var attrs = fieldInfo.GetCustomAttributes(false);
                    for (var j = 0; j < attrs.Length; j++)
                    {
                        var attr = attrs[j];
                        if (attr is T)
                        {
                            list[fieldInfo].Add(attr as T);
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有指定Attribute的属性
        /// </summary>
        /// <typeparam name="T">Attribute类型</typeparam>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static Dictionary<PropertyInfo, List<T>> GetPropertiesWithAttribute<T>(this Type type) where T : Attribute
        {
            var attrType = typeof(T);
            var list = new Dictionary<PropertyInfo, List<T>>();
            var propertyInfos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            for (var i = 0; i < propertyInfos.Length; i++)
            {
                var propertyInfo = propertyInfos[i];
                if (propertyInfo.IsDefined(attrType, false))
                {
                    list.Add(propertyInfo, new List<T>());
                    var attrs = propertyInfo.GetCustomAttributes(false);
                    for (var j = 0; j < attrs.Length; j++)
                    {
                        var attr = attrs[j];
                        if (attr is T)
                        {
                            list[propertyInfo].Add(attr as T);
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取所有指定Attribute的方法
        /// </summary>
        /// <typeparam name="T">Attribute类型</typeparam>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static Dictionary<MethodInfo, List<T>> GetMethodsWithAttribute<T>(this Type type) where T : Attribute
        {
            var attrType = typeof(T);
            var list = new Dictionary<MethodInfo, List<T>>();
            var methodInfos = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            for (var i = 0; i < methodInfos.Length; i++)
            {
                var methodInfo = methodInfos[i];
                if (methodInfo.IsDefined(attrType, false))
                {
                    list.Add(methodInfo, new List<T>());
                    var attrs = methodInfo.GetCustomAttributes(false);
                    for (var j = 0; j < attrs.Length; j++)
                    {
                        var attr = attrs[j];
                        if (attr is T)
                        {
                            list[methodInfo].Add(attr as T);
                        }
                    }
                }
            }
            return list;
        }
        #endregion

        #region Default
        
        /// <summary>
        /// 获取类型的默认值
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static object DefaultValue(this Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        /// <summary>
        /// 是否为可空类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static bool IsNullableType(this Type type)
        {
            if (!type.IsPrimitive && !type.IsValueType)
            {
                return !type.IsEnum;
            }
            return false;
        }

        #endregion

        #region Types

        /// <summary>
        /// 获取类型的所有子类
        /// </summary>
        /// <param name="baseType">基类</param>
        /// <returns>结果</returns>
        public static List<Type> GetSubTypes(this Type baseType)
        {
            var types = baseType.Assembly == Assembly ? Types : baseType.Assembly.GetTypes();
            var result = new List<Type>();
            for (var i = 0; i < types.Length; i++)
            {
                var type = types[i];
                if (type != baseType && baseType.IsAssignableFrom(type))
                {
                    result.Add(type);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取已经定义的类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static List<Type> GetDefinedTypes(this Type type)
        {
            var types = type.Assembly.GetTypes();
            var result = new List<Type>();
            for (var i = 0; i < types.Length; i++)
            {
                var t = types[i];
                if (t.IsDefined(type, true))
                {
                    result.Add(t);
                }
            }
            return result;
        }

        #endregion

        #region Interface
        
        /// <summary>
        /// 是否实现指定接口
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static bool ImplementsInterface<T>(this Type type)
        {
            return !type.IsInterface && type.GetInterfaces().Contains(typeof(T));
        }

        #endregion

        #region Enumerable

        /// <summary>
        /// 获取可迭代的值类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static Type GetEnumerableElementType(this Type type)
        {
            if (type == null) return null;
            if (!typeof(IEnumerable).IsAssignableFrom(type)) return null;
            if (type.HasElementType || type.IsArray) return type.GetElementType();
            if (type.IsGenericType)
            {
                var args = type.GetGenericArguments();
                if (args.Length == 1)
                {
                    return args[0];
                }

                if (args.Length == 2)
                {
                    return args[1];
                }
            }

            return null;
        }

        /// <summary>
        /// 是否为可迭代类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static bool IsEnumerableCollection(this Type type)
        {
            if (type == null) return false;
            var result = typeof(IEnumerable).IsAssignableFrom(type) && (type.IsGenericType || type.IsArray);
            return result;
        }

        #endregion

        #region Property Type & Field Type

        /// <summary>
        /// 获取所有指定类型的属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="propertyType">属性类型</param>
        /// <returns>结果</returns>
        public static List<PropertyInfo> GetPropertiesWithType(this Type type, Type propertyType)
        {
            var result = new List<PropertyInfo>();
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == propertyType)
                {
                    result.Add(property);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取所有指定类型的字段
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="fieldType">字段类型</param>
        /// <returns>结果</returns>
        public static List<FieldInfo> GetFieldsWithType(this Type type, Type fieldType)
        {
            var result = new List<FieldInfo>();
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.FieldType == fieldType)
                {
                    result.Add(field);
                }
            }

            return result;
        }

        #endregion
    }
}
