/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ReflectionUtil.cs
//  Info     : 反射辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Aya.Extension;

namespace Aya.Util
{
	public class ReflectionUtil
	{
		#region Property

		/// <summary>
		/// 获取指定属性的值
		/// </summary>
		/// <typeparam name="T">值类型</typeparam>
		/// <param name="target">目标</param>
		/// <param name="propertyName">属性名</param>
		/// <returns>结果</returns>
		public static T GetValue<T>(object target, string propertyName)
		{
			if (target == null) return default(T);
		    var property = target.GetType().GetProperty(propertyName);
            if(property == null) return default(T);
            var ret = property.GetValue(target, null);
			return ret.CastType<T>();
		}

		/// <summary>
		/// 设置指定属性的值
		/// </summary>
		/// <typeparam name="T">值类型</typeparam>
		/// <param name="target">目标</param>
		/// <param name="propertyName">属性名</param>
		/// <param name="value">值</param>
		/// <returns>结果</returns>
		public static bool SetValue<T>(object target, string propertyName, T value)
		{
			if (target == null) return false;
		    var property = target.GetType().GetProperty(propertyName);
		    if (property == null) return false;
		    property.SetValue(target, value, null);
			return true;
		}

	    /// <summary>
	    /// 取出所有静态字段
	    /// </summary>
	    /// <param name="type">类型</param>
	    /// <param name="flags">绑定标记</param>
	    /// <returns>结果</returns>
	    public static FieldInfo[] GetAllStaticFields(Type type, BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
		{
			var info = type.GetFields(flags);
			return info;
		}

		#endregion

		#region Class

		/// <summary>
		/// 获取指定类型的所有子类
		/// </summary>
		/// <param name="type">类型</param>
		/// <returns>结果</returns>
		public static Type[] GetSubClasses(Type type)
		{
			var types = type.Assembly.GetExportedTypes();
			var list = new List<Type>();
			for (var i = 0; i < types.Length; i++)
			{
				if (IsSubClass(types[i], type))
				{
					list.Add(types[i]);
				}
			}
			return list.ToArray();
		}

	    /// <summary>
	    /// 获取指定类型的所有子类
	    /// </summary>
	    /// <param name="type">类型</param>
	    /// <param name="assembly">指定程序集</param>
	    /// <returns>结果</returns>
	    public static Type[] GetSubClasses(Type type, Assembly assembly)
	    {
	        var types = assembly.GetExportedTypes();
	        var list = new List<Type>();
	        for (var i = 0; i < types.Length; i++)
	        {
	            if (IsSubClass(types[i], type))
	            {
	                list.Add(types[i]);
	            }
	        }
	        return list.ToArray();
	    }

        /// <summary>
        /// 是否是某类的子类
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="baseType">基类型</param>
        /// <returns>结果</returns>
        public static bool IsSubClass(Type type, Type baseType)
		{
			var b = type.BaseType;
			while (b != null)
			{
				if (b == baseType)
				{
					return true;
				}
				b = b.BaseType;
			}
			return false;
		}

		/// <summary>
		/// 取出指定命名空间下的所有类
		/// </summary>
		/// <param name="namespcae">命名空间</param>
		/// <returns>结果</returns>
		public static List<Type> GetClassesInNamespace(string namespcae)
		{
			var list = new List<Type>();
			// 取程序集的所有类
			var assembly = Assembly.GetCallingAssembly();
			var types = assembly.GetTypes();
			for (var i = 0; i < types.Length; i++)
			{
				var type = types[i];
				if (type.IsClass
				    && type.FullName != null
				    && type.FullName.IndexOf(namespcae, StringComparison.Ordinal) >= 0)
				{
					list.Add(type);
				}
			}
			return list;
		}

		#endregion

		#region Method

		/// <summary>
		/// 获取当前函数
		/// </summary>
		/// <returns></returns>
		public static MethodBase GetCurrentMethod()
		{
            var result = new StackFrame(1).GetMethod();
		    return result;
		}

		/// <summary>
		/// 获取主调函数
		/// </summary>
		/// <returns></returns>
		public static MethodBase GetFatherMethod()
		{
            var result = new StackFrame(2).GetMethod();
		    return result;
		}

	    /// <summary>
	    /// 取出某各类的所有静态方法
	    /// </summary>
	    /// <param name="type">类</param>
	    /// <param name="flags">绑定标记</param>
	    /// <returns>结果</returns>
	    public static List<MethodInfo> GetStaticMethods(Type type, BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
		{
			if (!type.IsClass) return null;
			var list = new List<MethodInfo>();
			// 取类的所有静态方法
			var methodInfos = type.GetMethods(flags);
			for (var i = 0; i < methodInfos.Length; i++)
			{
				list.Add(methodInfos[i]);
			}
			return list;
		}

		#endregion

		#region Attribute

		/// <summary>
		/// 取出某个类的属性
		/// </summary>
		/// <typeparam name="T">属性类型</typeparam>
		/// <param name="type">类</param>
		/// <returns>结果</returns>
		public static T GetAttribute<T>(Type type) where T : Attribute
		{
			// 取类的属性标签
			var attrs = type.GetCustomAttributes(typeof (T), false);
		    if (attrs.Length == 0) return null;
		    if (attrs[0] is T attribute)
		    {
		        return attribute;
		    }
		    return null;
		}

		/// <summary>
		/// 取出某个方法的属性
		/// </summary>
		/// <typeparam name="T">属性类型</typeparam>
		/// <param name="method">方法</param>
		/// <returns>结果</returns>
		public static T GetAttribute<T>(MethodInfo method) where T : Attribute
		{
			// 取方法的属性标签
			var attrs = method.GetCustomAttributes(typeof (T), false);
		    if (attrs.Length == 0) return null;
		    if (attrs[0] is T attribute)
		    {
		        return attribute;
		    }
		    return null;
		}

		#endregion
	}
}