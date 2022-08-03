/////////////////////////////////////////////////////////////////////////////
//
//  Script   : MonoBehaviourExtension.cs
//  Info     : MonoBehaviour 异步扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using UnityEngine;

namespace Aya.Async
{
    public static class MonoBehaviourExtension 
    {
        /// <summary>  
        /// 延时指定时间缩放执行 Action 
        /// </summary>
        /// <param name="monoBehaviour">MonoBehaviour</param>
        /// <param name="action">事件</param>
        /// <param name="seconds">时间</param>
        /// <param name="deltaTimeGetter">用户提供的时间间隔</param>
        /// <returns>结果</returns>
        public static Coroutine ExecuteDelayUserTime(this MonoBehaviour monoBehaviour, Action action, float seconds, Func<float> deltaTimeGetter)
        {
            return monoBehaviour.StartCoroutine(ExecuteDelayUserTimeCoroutine(action, seconds, deltaTimeGetter));
        }

        private static IEnumerator ExecuteDelayUserTimeCoroutine(Action action, float seconds, Func<float> deltaTimeGetter)
        {
            yield return new WaitForSecondsUserTime(seconds, deltaTimeGetter);
            action();
        }
    }
}
