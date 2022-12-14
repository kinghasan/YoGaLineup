/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IntExtension.cs
//  Info     : Int 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2019
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace Aya.Extension
{
    public static class IntExtension
    {
        #region Abs

        /// <summary>
        /// 取绝对值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static int Abs(this int value)
        {
            var result = Math.Abs(value);
            return result;
        }

        /// <summary>
        /// 取绝对值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static IEnumerable<int> Abs(this IEnumerable<int> value)
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
        public static bool IsInRange(this int value, int minValue, int maxValue)
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
        public static double InRange(this int value, int minValue, int maxValue, int defaultValue)
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
        public static void Times(this int value, Action action)
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
        public static void Times(this int value, Action<int> action)
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
        public static bool IsEven(this int value)
        {
            var result = value % 2 == 0;
            return result;
        }

        /// <summary>
        /// 是否是奇数
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>结果</returns>
        public static bool IsOdd(this int value)
        {
            var result = value % 2 != 0;
            return result;
        }

        #endregion

        #region Index

        /// <summary>
        /// 获取有效的数组索引(>=0)
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>结果</returns>
        public static int GetArrayIndex(this int value)
        {
            var result = value == 0 ? 0 : value;
            return result;
        }

        /// <summary>
        /// 是否在数组索引范围内
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="arrayToCheck">数组</param>
        /// <returns>结果</returns>
        public static bool IsIndexInArray(this int value, Array arrayToCheck)
        {
            var result = value.GetArrayIndex().IsInRange(arrayToCheck.GetLowerBound(0), arrayToCheck.GetUpperBound(0));
            return result;
        }

        #endregion

        #region Prime

        /// <summary>
        /// 计算规定值最近的二的次幂
        /// </summary>
        /// <param name="value">规定的值</param>
        /// <returns>容量</returns>
        public static int ToPrime(this int value)
        {
            value = Math.Max(0, value);

            var result = 0;
            for (var i = 2; i < int.MaxValue; i = i << 1)
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