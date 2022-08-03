/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IAPDebug.cs
//  Info     : IAP 调试工具类
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;
//using GameClient;

namespace Aya.IAP
{
	public static class IAPDebug
	{
		public static bool IsDebug = true;

		#region IAP Log

		/// <summary>
		/// 普通日志
		/// </summary>
		/// <param name="msg">信息</param>
		public static void Log(string msg)
		{
            if(IsDebug)
			// if (IsDebug && Client.Config.DebugLog)
			{
				Debug.Log("<color=green>[IAP]</color>\t" + msg);
			}
		}

		/// <summary>
		/// 警告日志
		/// </summary>
		/// <param name="msg">信息</param>
		public static void Warning(string msg)
		{
		    if (IsDebug)
            // if (IsDebug && Client.Config.DebugLog)
            {
				Debug.Log("<color=yellow>[IAP]</color>\t" + msg);
			}
		}

		/// <summary>
		/// 错误日志
		/// </summary>
		/// <param name="msg">日志</param>
		public static void Error(string msg)
		{
		    if (IsDebug)
            // if (IsDebug && Client.Config.DebugLog)
			{
				Debug.Log("<color=red>[IAP]</color>\t" + msg);
			}
		}

		#endregion
	}
}

