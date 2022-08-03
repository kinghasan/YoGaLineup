/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AnalysisDebug.cs
//  Info     : 统计分析 调试工具类
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;
//using GameClient;

namespace Aya.Analysis
{
	public static class AnalysisDebug
	{
		public static bool IsDebug = true;

		#region Analysis Log

		/// <summary>
		/// 普通日志
		/// </summary>
		/// <param name="msg">信息</param>
		public static void Log(string msg)
		{
			//if (IsDebug)
                Debug.Log("<color=green>[Analysis]</color>\t" + msg);
		}

		/// <summary>
		/// 警告日志
		/// </summary>
		/// <param name="msg">信息</param>
		public static void Warning(string msg)
		{
			if (IsDebug) Debug.Log("<color=yellow>[Analysis]</color>\t" + msg);
		}

		/// <summary>
		/// 错误日志
		/// </summary>
		/// <param name="msg">日志</param>
		public static void Error(string msg)
		{
			if (IsDebug) Debug.Log("<color=red>[Analysis]</color>\t" + msg);
		}

		#endregion
	}
}