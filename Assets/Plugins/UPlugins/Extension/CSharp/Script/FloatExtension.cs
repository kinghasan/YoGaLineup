/////////////////////////////////////////////////////////////////////////////
//
//  Script   : FloatExtension.cs
//  Info     : Float 扩展方法
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
    public static class FloatExtension
    {
        #region Round

        /// <summary>
        /// 舍入指定位数的小数点
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="decimalPoints">小数位数</param>
        /// <returns>结果</returns>
        public static float RoundDecimalPoints(this float value, int decimalPoints)
        {
            var result = (float)Math.Round(value, decimalPoints);
            return result;
        }

        /// <summary>
        /// 舍入2位数的小数点
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static float RoundToTwoDecimalPoints(this float value)
        {
            var result = (float)Math.Round(value, 2);
            return result;
        }

        #endregion

        #region Abs

        /// <summary>
        /// 取绝对值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static float Abs(this float value)
        {
            var result = Math.Abs(value);
            return result;
        }

        /// <summary>
        /// 取绝对值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static IEnumerable<float> Abs(this IEnumerable<float> value)
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
        public static bool IsInRange(this float value, float minValue, float maxValue)
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
        public static double InRange(this float value, float minValue, float maxValue, float defaultValue)
        {
            var result = value.IsInRange(minValue, maxValue) ? value : defaultValue;
            return result;
        }

        #endregion

        #region TimeSpan

        /// <summary>
        /// 转换为天
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>时间片</returns>
        public static TimeSpan ToDays(this float value)
        {
            var result = TimeSpan.FromDays(value);
            return result;
        }

        /// <summary>
        /// 转换为小时
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>时间片</returns>
        public static TimeSpan ToHours(this float value)
        {
            var result = TimeSpan.FromHours(value);
            return result;
        }

        /// <summary>
        /// 转换为毫秒
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>时间片</returns>
        public static TimeSpan ToMilliseconds(this float value)
        {
            var result = TimeSpan.FromMilliseconds(value);
            return result;
        }

        /// <summary>
        /// 转换为分钟
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>时间片</returns>
        public static TimeSpan ToMinutes(this float value)
        {
            var result = TimeSpan.FromMinutes(value);
            return result;
        }

        /// <summary>
        /// 转换为秒
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>时间片</returns>
        public static TimeSpan ToSeconds(this float value)
        {
            var result = TimeSpan.FromSeconds(value);
            return result;
        }

        #endregion
    }
}
