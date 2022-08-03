/////////////////////////////////////////////////////////////////////////////
//
//  Script   : ParticleSystemExtension.cs
//  Info     : ParticleSystem 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.Extension
{
    public static class ParticleSystemExtension
    {
        /// <summary>
        /// 获取粒子特效的播放时长(allowLoop = true 时，Loop 则为1个周期的长度，否则为-1)
        /// </summary>
        /// <param name="particle">粒子</param>
        /// <param name="allowLoop">是否允许计算循环特效时间</param>
        /// <returns>结果</returns>
        public static float GetDuration(this ParticleSystem particle, bool allowLoop = false)
        {
            if (!particle.emission.enabled) return 0f;
            if (particle.main.loop && !allowLoop)
            {
                return -1f;
            }
            if (particle.emission.rateOverTime.GetMinValue() <= 0)
            {
                return particle.main.startDelay.GetMaxValue() + particle.main.startLifetime.GetMaxValue();
            }
            else
            {
                return particle.main.startDelay.GetMaxValue() + Mathf.Max(particle.main.duration, particle.main.startLifetime.GetMaxValue());
            }
        }
    }
}