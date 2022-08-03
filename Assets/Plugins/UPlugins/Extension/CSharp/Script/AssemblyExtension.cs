/////////////////////////////////////////////////////////////////////////////
//
//  Script   : TypeExtension.cs
//  Info     : Assembly 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Aya.Extension
{
    public static class AssemblyExtension
    {
        /// <summary>
        /// 获取所有指定条件的类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static List<Type> GetTypes(this Assembly assembly, Predicate<Type> predicate)
        {
            var result = new List<Type>();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (predicate(type))
                {
                    result.Add(type);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取所有指定特性的类型
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="assembly">程序集</param>
        /// <returns>结果</returns>
        public static List<Type> GetTypesWithAttribute<T>(this Assembly assembly) where T : Attribute
        {
            var result = new List<Type>();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var attributes = type.GetAttributes<T>();
                if (attributes != null && attributes.Length > 0)
                {
                    result.Add(type);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取所有指定特性的类型
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="assembly">程序集</param>
        /// <param name="predicate">条件</param>
        /// <returns>结果</returns>
        public static List<Type> GetTypesWithAttribute<T>(this Assembly assembly, Predicate<Type> predicate) where T : Attribute
        {
            var result = new List<Type>();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var attributes = type.GetAttributes<T>();
                if (attributes != null && attributes.Length > 0 && predicate(type))
                {
                    result.Add(type);
                }
            }

            return result;
        }

        /// <summary>
        /// 获取一个指定特性的类型
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="assembly">程序集</param>
        /// <returns>结果</returns>
        public static Type GetTypeWithAttribute<T>(this Assembly assembly) where T : Attribute
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var attributes = type.GetAttributes<T>();
                if (attributes != null && attributes.Length > 0)
                {
                    return type;
                }
            }

            return null;
        }
    }
}
