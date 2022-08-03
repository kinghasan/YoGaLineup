/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SDKInfo.cs
//  Info     : SDK 信息基类
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using Aya.Data.Json;

namespace Aya.SDK
{
	[Serializable]
	public abstract class SDKInfo 
	{
		/// <summary>
		/// 转换为JSON字符串
		/// </summary>
		/// <returns>结果</returns>
		public virtual string ToJson()
		{
			var json = JsonMapper.ToJson(this);
			return json;
		}

		public override string ToString() 
		{
			return ToJson();
		}
	}
}
