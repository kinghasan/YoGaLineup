/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SDKResult.cs
//  Info     : SDK 回调结果
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Diagnostics;
using Aya.Data.Json;
using Aya.Extension;
using Aya.Util;

namespace Aya.SDK
{
	public class SDKResult 
	{
		#region Default Value
		/// <summary>
		/// 构造成功结果
		/// </summary>
		public static SDKResult Success
		{
			get
			{
				return new SDKResult("{\"code\":\"0\"}");
			}
		}

		/// <summary>
		/// 构造失败结果
		/// </summary>
		public static SDKResult Fail
		{
			get
			{
				return new SDKResult("{\"code\":\"-1\"}");
			}
		} 
		#endregion

		public int Code { get; private set; }

		public bool IsSuccess
		{
			get { return Code == 0; }
		}

		public string Reason { get; private set; }

		private readonly JObject _json;

		public SDKResult(string result) {
			_json = JObject.OptParse(result);
			Code = _json["code"].AsString().CastType<int>();
			Reason = !IsSuccess ? _json["reason"].AsString() : "";
		}

		/// <summary>
		/// 获取值
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="def">默认值</param>
		/// <returns>值</returns>
		public string this[string key, string def = null]
		{
			get { return Get(key, def); }
		}

		/// <summary>
		/// 获取值
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="def">默认值</param>
		/// <returns>值</returns>
		public string Get(string key, string def = null)
		{
			var value = _json[key].AsString();
			return value ?? def;
		}

		/// <summary>
		/// SDK回调结果通用处理
		/// </summary>
		/// <param name="result">结果信息</param>
		/// <param name="onSuccess">成功事件</param>
		/// <param name="onFail">失败事件</param>
		public static void OnResult(string result, Action<SDKResult> onSuccess = null, Action<int, string> onFail = null)
		{
			var method = new StackFrame(1).GetMethod().Name.Replace("On", "");
			try
			{
				var ret = new SDKResult(result);
				if (ret.IsSuccess)
				{
					SDKDebug.Log(method + " Success !");
					if (onSuccess != null)
					{
						onSuccess(ret);
					}
				}
				else
				{
					SDKDebug.Error(method + " Fail ! \t Code : " + ret.Code + "\t Reason : " + ret.Reason);
					if (onFail != null)
					{
						onFail(ret.Code, ret.Reason);
					}
				}
			}
			catch (Exception e)
			{
				SDKDebug.Error(method + " Execute fail ! \t " + result + " Msg : " + e);
			}
		}


		public override string ToString() {
			return _json.ToString();
		}
	}

}
