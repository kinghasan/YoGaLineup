/////////////////////////////////////////////////////////////////////////////
//
//  Script   : KeyValueSetString.cs
//  Info     : 键值对集合 - 字符串字典
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using Aya.Data.Json;

namespace Aya.Data
{
	[Serializable]
	public class KeyValueSet : KeyValueSet<string, string>
	{
		/// <summary>
		/// 空构造函数，仅用于框架反射创建集合，勿擅自调用！
		/// </summary>
		public KeyValueSet()
		{
		}

		/// <summary>
		/// 用于通过字符串创建键值集合
		/// </summary>
		/// <param name="str">序列化字符串</param>
		public KeyValueSet(string str)
		{
			if (string.IsNullOrEmpty(str)) return;
			var dic = JsonMapper.ToObject<Dictionary<string, string>>(str);
			Dic = dic;
		}

		/// <summary>
		/// 序列化存储对象
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="key">键</param>
		/// <param name="value">值</param>
		public void SetObject<T>(string key, T value)
		{
			Set(key, JsonMapper.ToJson(value));
		}

		/// <summary>
		/// 反序列化读取对象
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="key">键</param>
		/// <returns>值</returns>
		public T GetObject<T>(string key)
		{
			return JsonMapper.ToObject<T>(Get(key));
		}
	}
}