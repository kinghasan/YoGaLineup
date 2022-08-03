/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IComparableExtension.cs
//  Info     : IComparable 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;

namespace Aya.Extension
{
    public static class IComparableExtension 
    {
        /// <summary>
        /// 是否在两个值之间
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="lowerBound">下界</param>
        /// <param name="upperBound">上界</param>
        /// <param name="includeLowerBound">包含下界</param>
        /// <param name="includeUpperBound">包含上界</param>
        /// <returns>结果</returns>
        public static bool Between<T>(this T value, T lowerBound, T upperBound, bool includeLowerBound = false, bool includeUpperBound = false) where T : IComparable<T>
        {
            if (value == null) throw new ArgumentNullException();
            var lowerCompareResult = value.CompareTo(lowerBound);
            var upperCompareResult = value.CompareTo(upperBound);
            var result = (includeLowerBound && lowerCompareResult == 0) ||
                         (includeUpperBound && upperCompareResult == 0) ||
                         (lowerCompareResult > 0 && upperCompareResult < 0);
            return result;
        }

        /// <summary>
        /// 限制在两个值之间
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="min">最小</param>
        /// <param name="max">最大</param>
        /// <returns>结果</returns>
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            if (value == null) throw new ArgumentNullException();
            var result = value.LessThan(min) ? min : value.GreaterThan(max) ? max : value;
            return result;
        }

        /// <summary>
        /// 小于
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="other">比较值</param>
        /// <returns>结果</returns>
        public static bool LessThan<T>(this T value, T other) where T : IComparable<T>
        {
            if (value == null) throw new ArgumentNullException();
            var result = value.CompareTo(other) < 0;
            return result;
        }

        /// <summary>
        /// 小于等于
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="other">比较值</param>
        /// <returns>结果</returns>
        public static bool LessOrEqual<T>(this T value, T other) where T : IComparable<T>
        {
            if (value == null) throw new ArgumentNullException();
            var result = value.CompareTo(other) <= 0;
            return result;
        }

        /// <summary>
        /// 大于
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="other">比较值</param>
        /// <returns>结果</returns>
        public static bool GreaterThan<T>(this T value, T other) where T : IComparable<T>
        {
            if (value == null) throw new ArgumentNullException();
            var result = value.CompareTo(other) > 0;
            return result;
        }

        /// <summary>
        /// 大于等于
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="other">比较值</param>
        /// <returns>结果</returns>
        public static bool GreaterOrEqual<T>(this T value, T other) where T : IComparable<T>
        {
            if (value == null) throw new ArgumentNullException();
            var result = value.CompareTo(other) >= 0;
            return result;
        }
    }
}
