/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AnimationExtension.cs
//  Info     : Animation 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Extension
{
    public static class AnimationExtension
    {
        #region Event

        /// <summary>
        /// 添加动画事件
        /// </summary>
        /// <param name="animation">动画</param>
        /// <param name="clipName">片段名</param>
        /// <param name="methodName">事件名</param>
        /// <param name="time">触发时间</param>
        /// <returns>animation</returns>
        public static Animation AddEvent(this Animation animation, string clipName, string methodName, float time)
        {
            var clip = animation[clipName].clip;
            var animationEvent = new AnimationEvent
            {
                functionName = methodName,
                time = time
            };
            clip.AddEvent(animationEvent);
            return animation;
        }

        /// <summary>
        /// 移除动画事件
        /// </summary>
        /// <param name="animation">动画</param>
        /// <param name="clipName">片段名</param>
        /// <param name="methodName">事件名</param>
        /// <returns>animation</returns>
        public static Animation RemoveEvent(this Animation animation, string clipName, string methodName)
        {
            var clip = animation[clipName].clip;
            var list = clip.events.ToList();
            for (var i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (e.functionName != methodName) continue;
                list.Remove(e);
                break;
            }

            clip.events = list.ToArray();
            return animation;
        }

        #endregion

        /// <summary>
        /// 设置动画速度
        /// </summary>
        /// <param name="animation">动画</param>
        /// <param name="speed">速度</param>
        /// <returns>animation</returns>
        public static Animation SetSpeed(this Animation animation, float speed)
        {
            animation[animation.clip.name].speed = speed;
            return animation;
        }

        /// <summary>
        /// 设置动画时间
        /// </summary>
        /// <param name="animation">动画</param>
        /// <param name="time">时间</param>
        /// <returns>animation</returns>
        public static Animation SetTime(this Animation animation, float time)
        {
            animation[animation.clip.name].time = time;
            return animation;
        }

        /// <summary>
        /// 获取正在播放的片段
        /// </summary>
        /// <param name="animation">动画</param>
        /// <returns>结果</returns>
        public static AnimationClip GetPlayingClip(this Animation animation)
        {
            AnimationClip clip = null;
            var weight = 0f;
            var enumerator = animation.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (AnimationState) enumerator.Current;
                    if (current == null) continue;
                    if (!current.enabled || (!(weight < current.weight))) continue;
                    weight = current.weight;
                    clip = current.clip;
                }
            }
            finally
            {
                if (enumerator is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            return clip;
        }
    }
}