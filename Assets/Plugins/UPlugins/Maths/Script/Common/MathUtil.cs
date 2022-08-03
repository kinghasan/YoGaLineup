/////////////////////////////////////////////////////////////////////////////
//
//  Script   : MathUtil.cs
//  Info     : 数学辅助计算类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Maths
{
	public static partial class MathUtil
	{
        #region Const

        /// <summary>
        /// 浮点精度
        /// </summary>
        public static float FloatPrecision = 1e-6f;

        /// <summary>
        /// 2的平方根
        /// </summary>
	    public const float Sqrt2 = 1.41421356f;

        /// <summary>
        /// 3的平方根
        /// </summary>
        public const float Sqrt3 = 1.73205081f;

        /// <summary>
        /// 2倍 PI
        /// </summary>
        public const float TwoPi = 6.28318531f;

        /// <summary>
        /// 0.5倍 PI
        /// </summary>
        public const float HalfPi = 1.57079633f;

        /// <summary>
        /// 一百万分之一
        /// </summary>
        public const float OneMillionth = 1e-6f;

        /// <summary>
        /// 一百万
        /// </summary>
        public const float Million = 1e6f; 

        #endregion

        #region Approx / Float Test

        /// <summary>
        /// 测试接近指定浮点的值（取决于浮点精度，默认阈值 1e-6）
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="about">靠近</param>
        /// <returns>结果</returns>
        public static bool Approx(float value, float about)
	    {
	        var result = Approx(value, about, FloatPrecision);
            return result;
	    }

        /// <summary>
        /// 测试接近指定浮点的值（取决于浮点精度）
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="about">靠近</param>
        /// <param name="range">误差范围</param>
        /// <returns>结果</returns>
        public static bool Approx(float value, float about, float range)
	    {
	        var result = Math.Abs(value - about) < range;
	        return result;
	    }

        /// <summary>
        /// 测试接近制定向量的值（取决于浮点精度，默认阈值 1e-6）
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="about">靠近</param>
        /// <returns>结果</returns>
        public static bool Approx(Vector3 value, Vector3 about)
	    {
	        var result = Approx(value, about, FloatPrecision);
            return result;
	    }

        /// <summary>
        /// 测试接近制定向量的值（取决于浮点精度）
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="about">靠近</param>
        /// <param name="range">误差范围</param>
        /// <returns>结果</returns>
        public static bool Approx(Vector3 value, Vector3 about, float range)
	    {
	        var result = (value - about).sqrMagnitude < range * range;
	        return result;
	    }

        #endregion

        #region Exp
        
	    /// <summary>
        /// 2的N次方
        /// </summary>
        public static double Exp2(double n)
        {
            var result = Math.Exp(n * 0.69314718055994530941723212145818);
            return result;
        }

        #endregion

        #region Round

        /// <summary>
        /// 四舍五入时保留指定的有效数字<para/>
        /// (double 有效精度 15-17)
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">精度</param>
        /// <returns>结果</returns>
        public static double RoundToSignificantDigits(double value, int digits)
	    {
	        if (Math.Abs(value) < FloatPrecision) return 0.0;
	        var intDigits = (int) Math.Floor(Math.Log10(Math.Abs(value))) + 1;
	        if (intDigits <= digits) return Math.Round(value, digits - intDigits);
	        var scale = Math.Pow(10, intDigits - digits);
            var result = Math.Round(value / scale) * scale;
	        return result;
	    }

        /// <summary>
        /// 四舍五入时保留指定的有效数字<para/>
        /// (float 有效精度 6-9)
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">精度</param>
        /// <returns>结果</returns>
        public static float RoundToSignificantDigitsFloat(float value, int digits)
        {
            var result = (float)RoundToSignificantDigits(value, digits);
            return result;
        }

        /// <summary>
        /// 将指定值按范围线性拟合到[0，1]
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="min">输入最小值</param>
        /// <param name="max">输入最大值</param>
        /// <returns>结果</returns>
        public static float Linear01(float value, float min, float max)
        {
            var result = (value - min) / (max - min);
            return result;
        }

        /// <summary>
        /// 将指定值按范围线性拟合到[0，1] (限制边界)
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="min">输入最小值</param>
        /// <param name="max">输入最大值</param>
        /// <returns>结果</returns>
        public static float Linear01Clamped(float value, float min, float max)
        {
            var result = Mathf.Clamp01((value - min) / (max - min));
            return result;
        }

        /// <summary>
        /// 将指定值和范围，线性拟合到另一个范围区间
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="min">输入最小值</param>
        /// <param name="max">输入最大值</param>
        /// <param name="outputMin">输出最小值</param>
        /// <param name="outputMax">输出最大值</param>
        /// <returns>结果</returns>
        public static float Linear(float value, float min, float max, float outputMin, float outputMax)
        {
            var result = (value - min) / (max - min) * (outputMax - outputMin) + outputMin;
            return result;
        }

        /// <summary>
        /// 将指定值和范围，线性拟合到另一个范围区间 (限制边界)
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="min">输入最小值</param>
        /// <param name="max">输入最大值</param>
        /// <param name="outputMin">输出最小值</param>
        /// <param name="outputMax">输出最大值</param>
        /// <returns>结果</returns>
        public static float LinearClamped(float value, float min, float max, float outputMin, float outputMax)
        {
            var result = Mathf.Clamp01((value - min) / (max - min)) * (outputMax - outputMin) + outputMin;
            return result;
        }

        #endregion

        #region Clamp

        /// <summary>
        /// 限制最小值为0
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static float ClampMin0(this float value)
        {
            return ClampMin(value, 0);
        }

        /// <summary>
        /// 限制最小值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="min">最小值</param>
        /// <returns>结果</returns>
        public static float ClampMin(this float value, float min)
        {
            return value < min ? min : value;
        }

        /// <summary>
        /// 限制最大值为0
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>结果</returns>
        public static float ClampMax0(this float value)
        {
            return ClampMax(value, 0);
        }

        /// <summary>
        /// 限制最大值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="max">最大值</param>
        /// <returns>结果</returns>
        public static float ClampMax(this float value, float max)
        {
            return value > max ? max : value;
        }

        #endregion
    }
}