/////////////////////////////////////////////////////////////////////////////
//
//  Script   : StrUtil.cs
//  Info     : 字符串操作辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System.Text;

namespace Aya.Util
{
	/// <summary>
	/// 字符串操作辅助类
	/// </summary>
	public static class StrUtil
	{
		#region Valid / Compare

		/// <summary>
		/// 检查字符串有效
		/// </summary>
		/// <param name="str">字符串</param>
		/// <returns>结果</returns>
		public static bool IsValid(string str)
		{
			if (str == null) return false;
			return str.Trim() != "";
		}

		/// <summary>
		/// 是否相同
		/// </summary>
		/// <param name="str1">字符串1</param>
		/// <param name="str2">字符串2</param>
		/// <returns>结果</returns>
		public static bool Equals(string str1, string str2)
		{
			if (str1 == null)
			{
				return str2 == null;
			}
			else
			{
				return str1.Equals(str2);
			}
		}

		#endregion

		#region Split / Combine

		/// <summary>
		/// 将字符串分割成int数组
		/// </summary>
		/// <param name="str">字符串</param>
		/// <param name="split">分隔符</param>
		/// <returns>结果</returns>
		public static int[] SplitToInt(string str, char split)
		{
			var result = str.Split(split);
			var values = new[] {result.Length};
			for (var i = 0; i < result.Length; i++)
			{
				values[i] = int.Parse(result[i]);
			}
			return values;
		}

		/// <summary>
		/// 将字符串分割成float数组
		/// </summary>
		/// <param name="str">字符串</param>
		/// <param name="split">分隔符</param>
		/// <returns>结果</returns>
		public static float[] SplitToFloat(string str, char split)
		{
			var result = str.Split(split);
			var values = new float[] {result.Length};
			for (var i = 0; i < result.Length; i++)
			{
				values[i] = float.Parse(result[i]);
			}
			return values;
		}

		/// <summary>
		/// 数组转为字符串
		/// </summary>
		/// <param name="array">对象数组</param>
		/// <param name="split">分隔符</param>
		/// <returns>结果</returns>
		public static string ArrayToString(object[] array, string split = "")
		{
			var str = new StringBuilder();
			for (var i = 0; i < array.Length; i++)
			{
				str.Append(array[i]);
				if (i != array.Length - 1)
				{
					str.Append(split);
				}
			}
			return str.ToString();
		}

		#endregion

		#region SBC / DBC Convert

		/// <summary>
		/// 转为全角(SBC case)
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string ToSBC(string input)
		{
			//半角转全角：
			var c = input.ToCharArray();
			for (var i = 0; i < c.Length; i++)
			{
				if (c[i] == 32)
				{
					c[i] = (char) 12288;
					continue;
				}
				if (c[i] < 127)
					c[i] = (char) (c[i] + 65248);
			}
			return new string(c);
		}

		/// <summary>
		///  转为半角(SBC case)
		/// </summary>
		/// <param name="input">输入</param>
		/// <returns></returns>
		public static string ToDBC(string input)
		{
			var c = input.ToCharArray();
			for (var i = 0; i < c.Length; i++)
			{
				if (c[i] == 12288)
				{
					c[i] = (char) 32;
					continue;
				}
				if (c[i] > 65280 && c[i] < 65375)
					c[i] = (char) (c[i] - 65248);
			}
			return new string(c);
		}

		#endregion
	}
}