/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RandUtil.cs
//  Info     : 随机数操作辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Random = System.Random;
using UnityEngine;

namespace Aya.Util
{
	/// <summary>
	/// 随机数操作辅助类
	/// </summary>
	public static class RandUtil
	{
		/// <summary>
		/// 随机数发生器静态实例
		/// </summary>
		internal static Random Rand = new Random();

		/// <summary>
		/// 数字字母集合
		/// </summary>
		private static readonly string[] Char =
		{
			"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e","f",
			"g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
		};

		#region Rand Seed
		/// <summary>
		/// 设置随机数种子
		/// </summary>
		/// <param name="seed">种子</param>
		public static void SetRandSeed(int seed)
		{
			Rand = new Random(seed);
		}

		/// <summary>
		/// 重设随机数种子
		/// </summary>
		public static void ResetRandSeed()
		{
			Rand = new Random();
		}
		#endregion

		#region Rand for CSharp - int,float,bool,enum,byte[],DateTime

		/// <summary>
		/// 生成一个指定范围的随机整数，该随机数范围包括最小值(不包含最大值)
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		public static int RandInt(int min, int max)
		{
			return Rand.Next(min, max);
		}

		/// <summary>
		/// 生成一个指定范围的随机浮点数，该随机数范围包括最小值(不包含最大值)
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		public static float RandFloat(float min, float max)
		{
			return (float)(min + Rand.NextDouble() * (max - min));
		}

		/// <summary>
		/// 生成一个0.0到1.0的随机小数
		/// </summary>
		public static float RandZeroToOne()
		{
			return (float)Rand.NextDouble();
		}

		/// <summary>
		/// 生成一个true或false的值
		/// </summary>
		/// <param name="value">false的概率，默认0.5</param>
		/// <returns>结果</returns>
		public static bool RandBool(float value = 0.5f)
		{
			return Rand.NextDouble() > value;
		}

		/// <summary>
		/// 随机生成枚举值
		/// </summary>
		/// <typeparam name="T">枚举类型</typeparam>
		/// <returns>结果</returns>
		public static T RandEnum<T>() where T : struct
		{
			var type = typeof(T);
			if (type.IsEnum == false) throw new InvalidOperationException();
			var array = Enum.GetValues(type);
			var index = Rand.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
			return (T)array.GetValue(index);
		}

		/// <summary>
		/// 随机生成字节数组
		/// </summary>
		/// <param name="length">长度</param>
		/// <returns>结果</returns>
		public static byte[] RandBytes(int length)
		{
			var data = new byte[length];
			Rand.NextBytes(data);
			return data;
		}

		/// <summary>
		/// 随机生成日期
		/// </summary>
		/// <param name="minValue">最小日期</param>
		/// <param name="maxValue">最大日期</param>
		/// <returns>结果</returns>
		public static DateTime RandDateTime(DateTime minValue, DateTime maxValue)
		{
			var ticks = minValue.Ticks + (long)((maxValue.Ticks - minValue.Ticks) * Rand.NextDouble());
			return new DateTime(ticks);
		}

		/// <summary>
		/// 随机生成日期
		/// </summary>
		/// <returns>结果</returns>
		public static DateTime RandDateTime()
		{
			return RandDateTime(DateTime.MinValue, DateTime.MaxValue);
		}

		#endregion

		#region Rand for Unity - Vector,Quaternion,Color

		/// <summary>
		/// 随机生成一个向量
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		/// <returns>结果</returns>
		public static Vector2 RandVector2(float min = float.MinValue, float max = float.MaxValue)
		{
			return new Vector2(RandFloat(min, max), RandFloat(min, max));
		}

		/// <summary>
		/// 随机生成一个向量
		/// </summary>
		/// <param name="from">开始</param>
		/// <param name="to">结束</param>
		/// <returns>结果</returns>
		public static Vector2 RandVector2(Vector2 from, Vector2 to)
		{
			return new Vector2(RandFloat(from.x, to.x), RandFloat(from.y, to.y));
		}

		/// <summary>
		/// 随机生成一个向量
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		/// <returns>结果</returns>
		public static Vector3 RandVector3(float min = float.MinValue, float max = float.MaxValue)
		{
			return new Vector3(RandFloat(min, max), RandFloat(min, max), RandFloat(min, max));
		}

		/// <summary>
		/// 随机生成一个向量
		/// </summary>
		/// <param name="from">开始</param>
		/// <param name="to">结束</param>
		/// <returns>结果</returns>
		public static Vector3 RandVector3(Vector3 from, Vector3 to)
		{
			return new Vector3(RandFloat(from.x, to.x), RandFloat(from.y, to.y), RandFloat(from.z, to.z));
		}

		/// <summary>
		/// 随机生成一个向量
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		/// <returns>结果</returns>
		public static Vector4 RandVector4(float min = float.MinValue, float max = float.MaxValue)
		{
			return new Vector4(RandFloat(min, max), RandFloat(min, max), RandFloat(min, max), RandFloat(min, max));
		}

		/// <summary>
		/// 随机生成一个向量
		/// </summary>
		/// <param name="from">开始</param>
		/// <param name="to">结束</param>
		/// <returns>结果</returns>
		public static Vector4 RandVector4(Vector4 from, Vector4 to)
		{
			return new Vector4(RandFloat(from.x, to.x), RandFloat(from.y, to.y), RandFloat(from.z, to.z), RandFloat(from.w, to.w));
		}

		/// <summary>
		/// 随机生成一个旋转
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		/// <returns>结果</returns>
		public static Quaternion RandQuaternion(float min = 0f, float max = 360f)
		{
			return Quaternion.Euler(RandFloat(min, max), RandFloat(min, max), RandFloat(min, max));
		}

		/// <summary>
		/// 随机生成一个旋转
		/// </summary>
		/// <param name="from">开始</param>
		/// <param name="to">结束</param>
		/// <returns>结果</returns>
		public static Quaternion RandQuaternion(Quaternion from, Quaternion to)
		{
			return new Quaternion(RandFloat(from.x, to.x), RandFloat(from.y, to.y), RandFloat(from.z, to.z), RandFloat(from.w, to.w));
		}

		/// <summary>
		/// 随机生成一个颜色（0-1）
		/// </summary>
		/// <param name="min">最小值（0 - 1）</param>
		/// <param name="max">最大值（0 - 1）</param>
		/// <param name="alpha">透明度（0 - 1）</param>
		/// <returns>结果</returns>
		public static Color RandColor(float min = 0f, float max = 1f, float alpha = 1f)
		{
			return new Color(RandFloat(min, max), RandFloat(min, max), RandFloat(min, max), alpha);
		}

		/// <summary>
		/// 随机生成一个颜色（0-255）
		/// </summary>
		/// <param name="min">最小值（0 - 255）</param>
		/// <param name="max">最大值（0 - 255）</param>
		/// <param name="alpha">透明度（0 - 255）</param>
		/// <returns>结果</returns>
		public static Color RandColor256(float min = 0f, float max = 256f, float alpha = 256f)
		{
			return new Color(RandFloat(min, max) / 255f, RandFloat(min, max) / 255f, RandFloat(min, max) / 255f, alpha / 255f);
		}

		#endregion

		#region Int List & Array

		/// <summary>
		/// 生成随机数字集合
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		/// <param name="count">数量</param>
		/// <param name="allowRepeat">允许重复</param>
		/// <returns>结果</returns>
		public static List<int> RandIntList(int min, int max, int count, bool allowRepeat = false)
		{
			if (min > max)
            {
                throw new ArgumentException();
            }

			if (count > max - min + 1)
			{
                throw new ArgumentOutOfRangeException();
			}

			var list = new List<int>();
			for (var i = 0; i < count; i++)
			{
				var num = RandInt(min, max + 1);
				if (!allowRepeat)
				{
					if (list.IndexOf(num) >= 0)
					{
						i--;
						continue;
					}
				}

				list.Add(num);
			}

			return list;
		}

		/// <summary>
		/// 生成随机数组
		/// </summary>
		/// <param name="min">最小值</param>
		/// <param name="max">最大值</param>
		/// <param name="count">数量</param>
		/// <param name="allowRepeat">允许重复</param>
		/// <returns>结果</returns>
		public static int[] RandIntArray(int min, int max, int count, bool allowRepeat = false)
		{
			return RandIntList(min, max, count, allowRepeat).ToArray();
		}

		#endregion

		#region String

		/// <summary>
		/// 生成随机数字字符串
		/// </summary>
		/// <param name="length">长度</param>
		/// <returns>结果</returns>
		public static string RandNumString(int length)
		{
			var str = new StringBuilder();
			for (var i = 0; i < length; i++)
			{
				str.Append(Char[RandInt(0, 10)]);
			}
			return str.ToString();
		}

		/// <summary>
		/// 生成随机英文数字混合字符串
		/// </summary>
		/// <param name="length">长度</param>
		/// <param name="randUpOrLow">是否随机大小写(false则全部小写)</param>
		/// <returns>结果</returns>
		public static string RandNumAndCharString(int length, bool randUpOrLow = true)
		{
			var str = new StringBuilder();
			for (var i = 0; i < length; i++)
			{
				var index = RandInt(0, 36);
				var strTemp = Char[index];
				// 1/2概率转换为大写
				if (index > 9 && randUpOrLow == true && RandInt(0, 10) > 4)
				{
					strTemp = strTemp.ToUpper();
				}
				str.Append(strTemp);
			}
			return str.ToString();
		}

		/// <summary>
		/// 生成随机十六进制字符串
		/// </summary>
		/// <param name="length">长度</param>
		/// <returns>结果</returns>
		public static string RandHexString(int length)
		{
			var str = new StringBuilder();
			for (var i = 0; i < length; i++)
			{
				var index = RandInt(0, 16);
				var strTemp = Char[index];
				str.Append(strTemp);
			}
			return str.ToString();
		}

		/// <summary>
		/// 生成随机GUID
		/// </summary>
		/// <returns>结果</returns>
		public static string RandGuid()
		{
			return Guid.NewGuid().ToString();
		}

		#endregion

		#region Sort

		/// <summary>
		/// 随机打乱字符串内容(不支持中文)
		/// </summary>
		/// <param name="str">源字符串</param>
		/// <returns>结果</returns>
		public static string StringToRand(string str)
		{
			var chars = str.ToCharArray();
			var count = str.Length * 2;
			for (var i = 0; i < count; i++)
			{
				var index1 = Rand.Next(0, chars.Length);
				var item1 = chars[index1];
				var index2 = Rand.Next(0, chars.Length);
				var item2 = chars[index2];
				var temp = item2;
				chars[index2] = item1;
				chars[index1] = temp;
			}

			return new string(chars);
		}

		/// <summary>
		/// 随机打乱数组顺序
		/// </summary>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="array">数组</param>
		public static T[] ArrayToRand<T>(T[] array)
		{
			var count = array.Length * 3;
			for (var i = 0; i < count; i++)
			{
				var index1 = Rand.Next(0, array.Length);
				var item1 = array[index1];
				var index2 = Rand.Next(0, array.Length);
				var item2 = array[index2];
				var temp = item2;
				array[index2] = item1;
				array[index1] = temp;
			}

			return array;
		}

		/// <summary>
		/// 随机打乱集合顺序
		/// </summary>
		/// <param name="list">数组</param>
		public static IList ListToRand(IList list)
		{
			var count = list.Count * 3;
			for (var i = 0; i < count; i++)
			{
				var index1 = Rand.Next(0, list.Count);
				var item1 = list[index1];
				var index2 = Rand.Next(0, list.Count);
				var item2 = list[index2];
				var temp = item2;
				list[index2] = item1;
				list[index1] = temp;
			}

			return list;
		}

		/// <summary>
		/// 随机打乱集合顺序
		/// </summary>
		/// <typeparam name="T">对象类型</typeparam>
		/// <param name="list">数组</param>
		public static IList<T> ListToRand<T>(IList<T> list)
		{
			var count = list.Count * 3;
			for (var i = 0; i < count; i++)
			{
				var index1 = Rand.Next(0, list.Count);
				var item1 = list[index1];
				var index2 = Rand.Next(0, list.Count);
				var item2 = list[index2];
				var temp = item2;
				list[index2] = item1;
				list[index1] = temp;
			}

			return list;
		}

		#endregion

		#region Weight Rand

		/// <summary>
		/// 带权重的随机
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="list">候选元素列表</param>
		/// <param name="weights">权重列表</param>
		/// <returns>结果</returns>
		public static T Random<T>(IList<T> list, List<int> weights)
		{
			var weightCount = 0;
			for (var i = 0; i < weights.Count; i++)
			{
				weightCount += weights[i];
			}

			var rand = RandInt(0, weightCount);
			weightCount = 0;
			var index = -1;
			do
			{
				weightCount += weights[index + 1];
				index++;
			} while (weightCount < rand);

			var result = list[index];
			return result;
		}

		/// <summary>
		/// 带权重的随机
		/// </summary>
		/// <typeparam name="T">类型</typeparam>
		/// <param name="list">候选元素列表</param>
		/// <param name="weights">权重列表</param>
		/// <returns>结果</returns>
		public static T Random<T>(IList<T> list, List<float> weights)
		{
			var weightCount = 0f;
			for (var i = 0; i < weights.Count; i++)
			{
				weightCount += weights[i];
			}

			var rand = RandFloat(0f, weightCount);
			weightCount = 0f;
			var index = -1;
			do
			{
				weightCount += weights[index + 1];
				index++;
			} while (weightCount < rand);

			var result = list[index];
			return result;
		}

		#endregion
	}
}