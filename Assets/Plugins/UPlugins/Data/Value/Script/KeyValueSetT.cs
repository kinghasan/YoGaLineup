/////////////////////////////////////////////////////////////////////////////
//
//  Script   : KeyValueSetT.cs
//  Info     : 键值对集合 - 泛型基类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using Aya.Data.Json;
using Aya.Extension;

namespace Aya.Data
{
	[Serializable]
	public class KeyValueSet<TKey, TValue>
	{
		/// <summary>
		/// 键值字典
		/// </summary>
		protected Dictionary<TKey, TValue> Dic = new Dictionary<TKey, TValue>();

		/// <summary>
		/// 空构造函数，仅用于框架反射创建集合，勿擅自调用！
		/// </summary>
		public KeyValueSet() {
		}

		/// <summary>
		/// 索引器
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="defaultValue">默认值</param>
		/// <returns>结果</returns>
		public TValue this[TKey key, TValue defaultValue = default(TValue)]
		{
			get { return Dic.GetOrAdd(key, defaultValue); }
			set { Dic[key] = value; }
		}

		/// <summary>
		/// 获取
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="defaultValue">默认值</param>
		/// <returns>结果</returns>
		public TValue Get(TKey key, TValue defaultValue = default(TValue))
		{
			return this[key, defaultValue];
		}

		/// <summary>
		/// 获取（类型转换）
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="key">键</param>
		/// <returns>结果</returns>
		public T Get<T>(TKey key)
		{
			var ret = Get(key);
			return ret.CastType<T>();
		}

		/// <summary>
		/// 设置
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="value">值</param>
		/// <returns>结果</returns>
		public TValue Set(TKey key, TValue value)
		{
			Dic.AddOrReplace(key, value);
			return value;
		}

		/// <summary>
		/// 设置（类型转换）
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="key">键</param>
		/// <param name="value">值</param>
		/// <returns>结果</returns>
		public T Set<T>(TKey key, object value) 
		{
			Set(key, value.CastType<TValue>());
			return value.CastType<T>();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		public void Clear()
		{
			Dic.Clear();
		}

		/// <summary>
		/// 转换为Json
		/// </summary>
		/// <returns>结果</returns>
		public string ToJson()
		{
			return JsonUtil.Format(JsonMapper.ToJson(Dic));
		}

		/// <summary>
		/// 转换为字符串
		/// </summary>
		/// <returns>结果</returns>
		public override string ToString()
		{
			return ToJson();
		}
	}
}

