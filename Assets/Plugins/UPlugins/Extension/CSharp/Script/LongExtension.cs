/////////////////////////////////////////////////////////////////////////////
//
//  Script   : LongExtension.cs
//  Info     : Long 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace Aya.Extension
{
    public static class LongExtension
    {
        #region Abs

        /// <summary>
        /// 取绝对值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static long Abs(this long value)
        {
            var result = Math.Abs(value);
            return result;
        }

        /// <summary>
        /// 取绝对值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static IEnumerable<long> Abs(this IEnumerable<long> value)
        {
            foreach (var d in value)
            {
                yield return d.Abs();
            }
        }

        #endregion

        #region Range

        /// <summary>
        /// 是否在范围内
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns>结果</returns>
        public static bool IsInRange(this long value, long minValue, long maxValue)
        {
            var result = value >= minValue && value <= maxValue;
            return result;
        }

        /// <summary>
        /// 限制在范围内，超出范围则给定默认值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>结果</returns>
        public static double InRange(this long value, long minValue, long maxValue, long defaultValue)
        {
            var result = value.IsInRange(minValue, maxValue) ? value : defaultValue;
            return result;
        }

        #endregion

        #region Times

        /// <summary>
        /// 重复指定次数
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="action">Action</param>
        public static void Times(this long value, Action action)
        {
            for (var i = 0; i < value; i++)
            {
                action();
            }
        }

        /// <summary>
        /// 重复指定次数
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="action">Action</param>
        public static void Times(this int value, Action<long> action)
        {
            for (var i = 0; i < value; i++)
            {
                action(i);
            }
        }

        #endregion

        #region Odd / Even

        /// <summary>
        /// 是否是偶数
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>结果</returns>
        public static bool IsEven(this long value)
        {
            var result = value % 2 == 0;
            return result;
        }

        /// <summary>
        /// 是否是奇数
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>结果</returns>
        public static bool IsOdd(this long value)
        {
            var result = value % 2 != 0;
            return result;
        }

        #endregion

        #region Prime

        /// <summary>
        /// 计算规定值最近的二的次幂
        /// </summary>
        /// <param name="value">规定的值</param>
        /// <returns>容量</returns>
        public static long ToPrime(this long value)
        {
            value = Math.Max(0, value);

            long result = 0;
            for (long i = 2; i < long.MaxValue; i = i << 1)
            {
                if (i < value)
                {
                    continue;
                }
                result = i;
                break;
            }

            return result;
        }

        #endregion
    }
}
