/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AnimationCurveExtension.cs
//  Info     : AnimationCurve 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public static class AnimationCurveExtension
    {
        /// <summary>
        /// 获取动画曲线的最大值
        /// </summary>
        /// <param name="curve">曲线</param>
        /// <returns>结果</returns>
        public static float GetMaxValue(this AnimationCurve curve)
        {
            var ret = float.MinValue;
            var frames = curve.keys;
            for (var i = 0; i < frames.Length; i++)
            {
                var frame = frames[i];
                var value = frame.value;
                if (value > ret)
                {
                    ret = value;
                }
            }

            return ret;
        }

        /// <summary>
        /// 获取动画曲线的最小值
        /// </summary>
        /// <param name="curve">曲线</param>
        /// <returns>结果</returns>
        public static float GetMinValue(this AnimationCurve curve)
        {
            var ret = float.MaxValue;
            var frames = curve.keys;
            for (var i = 0; i < frames.Length; i++)
            {
                var frame = frames[i];
                var value = frame.value;
                if (value < ret)
                {
                    ret = value;
                }
            }

            return ret;
        }
    }
}