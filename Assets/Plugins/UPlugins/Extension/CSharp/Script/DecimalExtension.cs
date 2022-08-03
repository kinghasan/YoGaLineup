/////////////////////////////////////////////////////////////////////////////
//
//  Script   : DecimalExtension.cs
//  Info     : Decimal 扩展方法
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
    public static class DecimalExtension
    {
        #region Round
        
        /// <summary>
        /// 舍入指定位数的小数点
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="decimalPoints">小数位数</param>
        /// <returns>结果</returns>
        public static decimal RoundDecimalPoints(this decimal value, int decimalPoints)
        {
            var result = Math.Round(value, decimalPoints);
            return result;
        }

        /// <summary>
        /// 舍入2位数的小数点
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static decimal RoundToTwoDecimalPoints(this decimal value)
        {
            var result = Math.Round(value, 2);
            return result;
        }

        #endregion

        #region Abs
        
        /// <summary>
        /// 取绝对值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static decimal Abs(this decimal value)
        {
            var result = Math.Abs(value);
            return result;
        }

        /// <summary>
        /// 取绝对值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static IEnumerable<decimal> Abs(this IEnumerable<decimal> value)
        {
            foreach (var d in value)
            {
                yield return d.Abs();
            }
        } 
        
        #endregion
    }
}
