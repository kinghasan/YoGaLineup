/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AnalysisUtil.cs
//  Info     : 统计分析通用工具方法
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;

namespace Aya.Analysis
{
	public static class AnalysisUtil
	{
		/// <summary>
		/// 参数数组转换为参数字典
		/// </summary>
		/// <param name="args">参数(按键值顺序排列，必须为偶数个)</param>
		/// <returns>结果</returns>
		public static Dictionary<string, object> ParamToDic(params object[] args) {
			if (args.Length % 2 != 0)
			{
				AnalysisDebug.Error("Param should be pair!");
			}
			var dict = new Dictionary<string, object>();
			for (var i = 0; i + 1 < args.Length; i += 2)
			{
				dict.Add(args[i].ToString(), args[i + 1]);
			}
			return dict;
		}
	}
}

