/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ObjectExtension.cs
//  Info     : 对象扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
#define AYA_USECURITY
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Aya.Extension
{
    public static class ObjectExtension
    {
        #region Null

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static bool IsNull(this object obj)
        {
            var result = ReferenceEquals(obj, null);
            return result;
        }

        /// <summary>
        /// 是否非空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static bool IsNotNull(this object obj)
        {
            var result = !ReferenceEquals(obj, null);
            return result;
        }

        #endregion

        #region Type

        /// <summary>
        /// 转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static T CastType<T>(this object obj)
        {
            try
            {
                var result = (T) obj.CastType(typeof(T));
                return result;
            }
            catch
            {
                var result = default(T);
                return result;
            }
        }


        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static object CastType(this object obj, Type type)
        {
            try
            {
                var result = Convert.ChangeType(obj, type, CultureInfo.InvariantCulture);
                return result;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e);
                var result = type.DefaultValue();
                return result;
            }
        }

        /// <summary>
        /// 转换为指定类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>结果</returns>
        public static T ConvertTo<T>(this object obj, T defaultValue = default(T))
        {
            if (obj != null)
            {
                var targetType = typeof(T);

                if (obj.GetType() == targetType) return (T) obj;

                var converter = TypeDescriptor.GetConverter(obj);
                if (converter.CanConvertTo(targetType))
                {
                    return (T) converter.ConvertTo(obj, targetType);
                }

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter.CanConvertFrom(obj.GetType()))
                {
                    return (T) converter.ConvertFrom(obj);
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// 是否可以转换为指定类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static bool CanConvertTo<T>(this object obj)
        {
            if (obj != null)
            {
                var targetType = typeof(T);

                var converter = TypeDescriptor.GetConverter(obj);
                if (converter.CanConvertTo(targetType))
                {
                    return true;
                }

                converter = TypeDescriptor.GetConverter(targetType);
                if (converter.CanConvertFrom(obj.GetType()))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Interface

        /// <summary>
        /// 是否实现指定接口
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static bool ImplementsInterface<T>(this object obj)
        {
            var type = obj.GetType();
            var result = !type.IsInterface && type.GetInterfaces().Contains(typeof(T));
            return result;
        }

        #endregion

        #region Inherit / Assignable

        /// <summary>
        /// 是否继承自指定类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static bool IsInherits(this object obj, Type type)
        {
            var objectType = obj.GetType();

            do
            {
                if (objectType == type)
                {
                    return true;
                }

                if ((objectType == objectType.BaseType) || (objectType.BaseType == null))
                {
                    return false;
                }

                objectType = objectType.BaseType;
            } while (true);
        }

        /// <summary>
        /// 是否可以转换为指定类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static bool IsAssignableTo<T>(this object obj)
        {
            var result = obj.IsAssignableTo(typeof(T));
            return result;
        }

        /// <summary>
        /// 是否可以转换为指定类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static bool IsAssignableTo(this object obj, Type type)
        {
            var objectType = obj.GetType();
            var result = type.IsAssignableFrom(objectType);
            return result;
        }

        #endregion

        #region Field

        /// <summary>
        /// 获取字段(包含私有/公有/静态)
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldName">属性名</param>
        /// <param name="flags">绑定标记</param>
        /// <returns>结果</returns>
        public static object GetField(this object obj, string fieldName,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
        {
            var fieldInfo = obj.GetType().GetField(fieldName, flags);
            return fieldInfo != null ? fieldInfo.GetValue(obj) : null;
        }

        /// <summary>
        /// 设置字段(包含私有/公有/静态)
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldName">属性名</param>
        /// <param name="value">值</param>
        /// <param name="flags">绑定标记</param>
        public static void SetField(this object obj, string fieldName, object value,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
        {
            var fieldInfo = obj.GetType().GetField(fieldName, flags);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(obj, value);
            }
        }

        /// <summary>
        /// 获取包含指定特性的字段(包含私有/公有/静态)
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="flags">绑定标记</param>
        /// <returns>字段列表</returns>
        public static List<FieldInfo> GetFieldsWithAttribute<T>(this object obj,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static) where T : Attribute
        {
            var result = new List<FieldInfo>();
            var fields = obj.GetType().GetFields(flags);
            for (var i = 0; i < fields.Length; i++)
            {
                var fieldInfo = fields[i];
                var attribute = fieldInfo.GetCustomAttribute<T>();
                if (attribute != null)
                {
                    result.Add(fieldInfo);
                }
            }

            return result;
        }

        #endregion

        #region Property

        /// <summary>
        /// 获取属性(包含私有/公有/静态)
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="flags">绑定标记</param>
        /// <returns>结果</returns>
        public static object GetProperty(this object obj, string propertyName,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName, flags);
            return propertyInfo != null ? propertyInfo.GetValue(obj, null) : null;
        }

        /// <summary>
        /// 设置属性(包含私有/公有/静态)
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">值</param>
        /// <param name="flags">结果</param>
        public static void SetProperty(this object obj, string propertyName, object value,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName, flags);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(obj, value, null);
            }
        }


        /// <summary>
        /// 获取包含指定特性的属性(包含私有/公有/静态)
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="flags">绑定标记</param>
        /// <returns>属性列表</returns>
        public static List<PropertyInfo> GetPropertiesWithAttribute<T>(this object obj,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static) where T : Attribute
        {
            var result = new List<PropertyInfo>();
            var properties = obj.GetType().GetProperties(flags);
            for (var i = 0; i < properties.Length; i++)
            {
                var propertyInfo = properties[i];
                var attribute = propertyInfo.GetCustomAttribute<T>();
                if (attribute != null)
                {
                    result.Add(propertyInfo);
                }
            }

            return result;
        }

        #endregion

        #region Method

        /// <summary>
        /// 获取方法（包括私有/公有/静态)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <param name="flags">绑定标记</param>
        /// <returns></returns>
        public static MethodInfo GetMethod(this object obj, string methodName,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
        {
            var methodInfo = obj.GetType().GetMethod(methodName, flags);
            return methodInfo;
        }

        /// <summary>
        /// 执行方法(包含私有/公有/静态)
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="methodName">方法名</param>
        /// <param name="param">参数</param>
        /// <returns>结果</returns>
        public static object InvokeMethod(this object obj, string methodName, params object[] param)
        {
            var methodInfo = GetMethod(obj, methodName);
            return methodInfo != null ? methodInfo.Invoke(obj, param) : null;
        }

        /// <summary>
        /// 执行方法(包含私有/公有/静态)
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="methodName">方法名</param>
        /// <param name="flags">绑定标记</param>
        /// <param name="param">参数</param>
        /// <returns>结果</returns>
        public static object InvokeMethod(this object obj, string methodName, BindingFlags flags, params object[] param)
        {
            var methodInfo = GetMethod(obj, methodName, flags);
            return methodInfo != null ? methodInfo.Invoke(obj, param) : null;
        }

        /// <summary>
        /// 获取包含指定特性的方法(包含私有/公有/静态)
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="flags">绑定标记</param>
        /// <returns>方法列表</returns>
        public static List<MethodInfo> GetMethodsWithAttribute<T>(this object obj,
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static) where T : Attribute
        {
            var result = new List<MethodInfo>();
            var methods = obj.GetType().GetMethods(flags);
            for (var i = 0; i < methods.Length; i++)
            {
                var methodInfo = methods[i];
                var attribute = methodInfo.GetCustomAttribute<T>();
                if (attribute != null)
                {
                    result.Add(methodInfo);
                }
            }

            return result;
        }

        #endregion

        #region Attribute

        /// <summary>
        /// 获取特性（包括继承）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static T GetAttribute<T>(this object obj) where T : Attribute
        {
            var result = GetAttribute<T>(obj, true);
            return result;
        }

        /// <summary>
        /// 获取特性
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="includeInherited">是否包括继承</param>
        /// <returns>结果</returns>
        public static T GetAttribute<T>(this object obj, bool includeInherited) where T : Attribute
        {
            var type = (obj as Type ?? obj.GetType());
            var attributes = type.GetCustomAttributes(typeof(T), includeInherited);
            var first = attributes.First();
            var result = first as T;
            return result;
        }

        /// <summary>
        /// 获取所有特性（包括继承）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static T[] GetAttributes<T>(this object obj) where T : Attribute
        {
            var result = GetAttributes<T>(obj, true);
            return result;
        }

        /// <summary>
        /// 获取所有特性
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="includeInherited">是否包括继承</param>
        /// <returns>结果</returns>
        public static T[] GetAttributes<T>(this object obj, bool includeInherited) where T : Attribute
        {
            var type = (obj as Type ?? obj.GetType());
            var attributes = type.GetCustomAttributes(typeof(T), includeInherited);
            var result = attributes.ToArray(v => v as T);
            return result;
        }

        #endregion

        #region As

        /// <summary>
        /// 转换为 string
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static string AsString(this object obj)
        {
            var result = ReferenceEquals(obj, null) ? null : $"{obj}";
            return result;
        }

        /// <summary>
        /// 转换为 string
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="formatProvider">转换器</param>
        /// <returns>结果</returns>
        public static string AsString(this object obj, IFormatProvider formatProvider)
        {
            var result = string.Format(formatProvider, "{0}", obj);
            return result;
        }

        /// <summary>
        /// 转换为区域无关的 string
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static string AsInvariantString(this object obj)
        {
            var result = string.Format(CultureInfo.InvariantCulture, "{0}", obj);
            return result;
        }

        /// <summary>
        /// 转换为 int
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static int AsInt(this object obj)
        {
            var result = obj.CastType<int>();
            return result;
        }

        /// <summary>
        /// 转换为 long
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static long AsLong(this object obj)
        {
            var result = obj.CastType<long>();
            return result;
        }

        /// <summary>
        /// 转换为 short
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static short AsShort(this object obj)
        {
            var result = obj.CastType<short>();
            return result;
        }

        /// <summary>
        /// 转换为 float
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static float AsFloat(this object obj)
        {
            var result = obj.CastType<float>();
            return result;
        }

        /// <summary>
        /// 转换为 double
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static double AsDouble(this object obj)
        {
            var result = obj.CastType<double>();
            return result;
        }

        /// <summary>
        /// 转换为 decimal
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>结果</returns>
        public static decimal AsDecimal(this object obj)
        {
            var result = obj.CastType<decimal>();
            return result;
        }

        #endregion

        #region Copy Property / Field

        /// <summary>
        /// 从另一个对象复制所有属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="source">数据源</param>
        public static void CopyPropertiesFrom(this object obj, object source)
        {
            CopyPropertiesFrom(obj, source, string.Empty);
        }

        /// <summary>
        /// 从另一个对象复制所有属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="source">数据源</param>
        /// <param name="ignoreProperty"></param>
        public static void CopyPropertiesFrom(this object obj, object source, string ignoreProperty)
        {
            CopyPropertiesFrom(obj, source, new[] {ignoreProperty});
        }

        /// <summary>
        /// 从另一个对象复制所有属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="source">数据源</param>
        /// <param name="ignoreProperties">忽略属性</param>
        public static void CopyPropertiesFrom(this object obj, object source, string[] ignoreProperties)
        {
            var type = source.GetType();
            if (obj.GetType() != type)
            {
                throw new ArgumentException("The source type must be the same as the target");
            }

            var ignoreList = new List<string>();
            foreach (var item in ignoreProperties)
            {
                if (!string.IsNullOrEmpty(item) && !ignoreList.Contains(item))
                {
                    ignoreList.Add(item);
                }
            }

            foreach (var propertyInfo in type.GetProperties())
            {
                if (!propertyInfo.CanWrite || !propertyInfo.CanRead || ignoreList.Contains(propertyInfo.Name)) continue;
                var val = propertyInfo.GetValue(source, null);
                propertyInfo.SetValue(obj, val, null);
            }
        }

        /// <summary>
        /// 从另一个对象复制所有字段值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="source">数据源</param>
        public static void CopyFieldsFrom(this object obj, object source)
        {
            CopyFieldsFrom(obj, source, string.Empty);
        }

        /// <summary>
        /// 从另一个对象复制所有字段值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="source">数据源</param>
        /// <param name="ignoreField">忽略字段</param>
        public static void CopyFieldsFrom(this object obj, object source, string ignoreField)
        {
            CopyFieldsFrom(obj, source, new[] {ignoreField});
        }

        /// <summary>
        /// 从另一个对象复制所有字段值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="source">数据源</param>
        /// <param name="ignoreFields">忽略字段</param>
        public static void CopyFieldsFrom(this object obj, object source, string[] ignoreFields)
        {
            var type = source.GetType();
            if (obj.GetType() != type)
            {
                throw new ArgumentException("The source type must be the same as the target");
            }

            var ignoreList = new List<string>();
            foreach (var item in ignoreFields)
            {
                if (!string.IsNullOrEmpty(item) && !ignoreList.Contains(item))
                {
                    ignoreList.Add(item);
                }
            }

            foreach (var fieldInfo in type.GetFields())
            {
                if (ignoreList.Contains(fieldInfo.Name)) continue;
                var val = fieldInfo.GetValue(source);
                fieldInfo.SetValue(obj, val);
            }
        }

        /// <summary>
        /// 从另一个对象复制所有属性和字段的值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="source">数据源</param>
        public static void CopyPropertiesAndFieldsFrom(this object obj, object source)
        {
            CopyPropertiesFrom(obj, source);
            CopyFieldsFrom(obj, source);
        }

        #endregion
    }
}