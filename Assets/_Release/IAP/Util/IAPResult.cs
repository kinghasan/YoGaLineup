/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IAPResult.cs
//  Info     : IAP 回调结果
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Diagnostics;
using Aya.Data.Json;
using Aya.Extension;
using Aya.Util;

namespace Aya.IAP
{
    public class IAPResult
	{
		#region Default Value

		/// <summary>
		/// 构造成功结果
		/// </summary>
		public static IAPResult Success
		{
			get { return new IAPResult("{\"code\":\"0\"}"); }
		}

		/// <summary>
		/// 构造失败结果
		/// </summary>
		public static IAPResult Fail
		{
			get { return new IAPResult("{\"code\":\"-1\"}"); }
		}

		#endregion

		public int Code { get; private set; }

		public bool IsSuccess
		{
			get { return Code == 0; }
		}

		public string Reason { get; private set; }

		private readonly JObject _json;

		public IAPResult(string result)
		{
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
		    set { Set(key, value); }
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
	    /// 设置值
	    /// </summary>
	    /// <param name="key">键</param>
	    /// <param name="value">值</param>
	    public void Set(string key, string value)
	    {
	        _json[key] = value;
	    }

        /// <summary>
        /// SDK回调结果通用处理
        /// </summary>
        /// <param name="result">结果信息</param>
        /// <param name="onSuccess">成功事件</param>
        /// <param name="onFail">失败事件</param>
        public static void OnResult(string result, Action<IAPResult> onSuccess = null, Action<int, string> onFail = null)
		{
			var method = new StackFrame(1).GetMethod().Name.Replace("On", "");
			try
			{
				var ret = new IAPResult(result);
				IAPManager.Instance.CurrentResult = ret;
				if (ret.IsSuccess)
				{
					IAPDebug.Log(method + " Success !");
					if (onSuccess != null)
					{
						onSuccess(ret);
					}
				}
				else
				{
					IAPDebug.Error(method + " Fail ! \t Code : " + ret.Code + "\t Reason : " + ret.Reason);
					if (onFail != null)
					{
						onFail(ret.Code, ret.Reason);
					}
				}
			}
			catch (Exception e)
			{
				IAPDebug.Error(method + " Execute fail ! \t " + result + " Msg : " + e);
			}
		}

		public override string ToString()
		{
			return _json.ToString();
		}
	}
}
