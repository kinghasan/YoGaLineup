/////////////////////////////////////////////////////////////////////////////
//
//  Script : YieldBuilder.cs
//  Info   : Yield 构建器，用于提供 Yield 对象并实现复用，减少 new yield 开销
//  Author : ls9512
//  E-mail : ls9512@vip.qq.com
//
//  Warning : 使用该类创建延时对象，可以提高复用率减少创建开销，但同时需要注意同样数值和类型的延时，是同一个对象，只能在单线延时流程上使用，不能交替、重叠使用。
//
//  Copyright : Aya Game Studio 2020
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Aya.Async
{
    public static class YieldBuilder
    {
        #region Cache

        private static readonly WaitForEndOfFrame WaitForEndOfFrameCache = new WaitForEndOfFrame();
        private static readonly WaitForFixedUpdate WaitForFixedUpdateCache = new WaitForFixedUpdate();
        private static readonly Dictionary<string, WaitForSeconds> WaitForSecondsCache = new Dictionary<string, WaitForSeconds>();
        private static readonly Dictionary<string, WaitForSecondsRealtime> WaitForSecondsRealtimeCache = new Dictionary<string, WaitForSecondsRealtime>();
        private static readonly Dictionary<Func<float>, WaitForSecondsUserTime> WaitForSecondsUserTimeCache = new Dictionary<Func<float>, WaitForSecondsUserTime>();

        #endregion

        #region Async Task -> Coroutine

        public static IEnumerator AsyncTask(Action action)
        {
            var complete = false;
            AsyncTaskInternal(action, () => { complete = true; });
            while (!complete)
            {
                yield return null;
            }

            yield return null;
        }

        private static async void AsyncTaskInternal(Action action, Action onDone)
        {
            await Task.Run(action);
            onDone();
        }

        #endregion

        #region Wait Builder

        public static WaitForEndOfFrame WaitForEndOfFrame()
        {
            return WaitForEndOfFrameCache;
        }

        public static WaitForFixedUpdate WaitForFixedUpdate()
        {
            return WaitForFixedUpdateCache;
        }

        public static WaitForSeconds WaitForSeconds(float second)
        {
            var key = second.ToString("F2");
            if (WaitForSecondsCache.TryGetValue(key, out var result)) return result;
            result = new WaitForSeconds(second);
            WaitForSecondsCache.Add(key, result);
            return result;
        }

        public static WaitForSecondsRealtime WaitForSecondsRealtime(float second)
        {
            var key = second.ToString("F2");
            if (WaitForSecondsRealtimeCache.TryGetValue(key, out var result)) return result;
            result = new WaitForSecondsRealtime(second);
            WaitForSecondsRealtimeCache.Add(key, result);
            return result;
        }

        public static WaitForSecondsUserTime WaitForSecondsUserTime(float second, Func<float> deltaTimeGetter)
        {
            if (WaitForSecondsUserTimeCache.TryGetValue(deltaTimeGetter, out var result)) return result;
            result = new WaitForSecondsUserTime(second, deltaTimeGetter);
            WaitForSecondsUserTimeCache.Add(deltaTimeGetter, result);
            return result;
        }

        public static WaitUntil WaitUntil(Func<bool> predicate)
        {
            return new WaitUntil(predicate);
        }

        public static WaitWhile WaitWhile(Func<bool> predicate)
        {
            return new WaitWhile(predicate);
        } 

        #endregion
    }
}
