/////////////////////////////////////////////////////////////////////////////
//
//  Script   : MonoBehaviourExtension.cs
//  Info     : MonoBehaviour 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Warning : MonoBehaviour扩展的延时协程，在依赖对象被销毁后，会失效！如需长期生效，请使用Delay。
//
//  Copyright : Aya Game Studio 2016
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Aya.Extension
{
    public static class MonoBehaviourExtension
    {
        #region Prefab

        public static bool IsPrefab(this MonoBehaviour monoBehaviour)
        {
            var result = monoBehaviour.gameObject.IsPrefab();
            return result;
        }

        #endregion

        #region Coroutine

        /// <summary>
        /// 当条件满足返回true时，执行Action
        /// </summary>
        /// <param name="monoBehaviour">MonoBehaviour</param>
        /// <param name="action">事件</param>
        /// <param name="condition">条件</param>
        /// <returns>结果</returns>
        public static Coroutine ExecuteWhen(this MonoBehaviour monoBehaviour, Action action, Func<bool> condition)
        {
            return monoBehaviour.StartCoroutine(ExecuteWhenCoroutine(action, condition));
        }

        /// <summary>
        /// 当条件满足时停止，否则一直执行Action
        /// </summary>
        /// <param name="monoBehaviour">MonoBehaviour</param>
        /// <param name="action">事件</param>
        /// <param name="condition">条件</param>
        /// <returns>结果</returns>
        public static Coroutine ExecuteUntil(this MonoBehaviour monoBehaviour, Action action, Func<bool> condition)
        {
            return monoBehaviour.StartCoroutine(ExecuteUntilCoroutine(action, condition));
        }

        /// <summary>  
        /// 延时执行Action 
        /// </summary>
        /// <param name="monoBehaviour">MonoBehaviour</param>
        /// <param name="action">事件</param>  
        /// <param name="seconds">时间</param>
        /// <param name="timeScale">时间缩放</param>
        /// <returns>结果</returns>
        public static Coroutine ExecuteDelay(this MonoBehaviour monoBehaviour, Action action, float seconds, bool timeScale = true)
        {
            return monoBehaviour.StartCoroutine(ExecuteDelayCoroutine(action, seconds, timeScale));
        }

        /// <summary>  
        /// 下一帧执行
        /// </summary>  
        /// <param name="monoBehaviour">MonoBehaviour</param>  
        /// <param name="action">事件</param>
        /// <param name="count">次数</param>
        /// <returns>结果</returns>
        public static Coroutine ExecuteNextFrame(this MonoBehaviour monoBehaviour, Action action, int count = 1)
        {
            return monoBehaviour.StartCoroutine(ExecuteNextFrameCoroutine(action, count));
        }

        /// <summary>  
        /// 帧结束时执行
        /// </summary>  
        /// <param name="monoBehaviour">MonoBehaviour</param>  
        /// <param name="action">事件</param>  
        /// <returns>结果</returns>
        public static Coroutine ExecuteEndOfFrame(this MonoBehaviour monoBehaviour, Action action)
        {
            return monoBehaviour.StartCoroutine(ExecuteEndOfFrameCoroutine(action));
        }

        /// <summary>
        /// 延迟指定帧数后执行
        /// </summary>
        /// <param name="monoBehaviour">MonoBehaviour</param>
        /// <param name="action">事件</param>
        /// <param name="frames">帧数</param>
        /// <returns>结果</returns>
        public static Coroutine ExecuteAfterFrames(this MonoBehaviour monoBehaviour, Action action, int frames)
        {
            return monoBehaviour.StartCoroutine(ExecuteAfterFramesCoroutine(action, frames));
        }

        /// <summary>
        /// 执行协程
        /// </summary>
        /// <param name="monoBehaviour">MonoBehaviour</param>
        /// <param name="coroutine">协程</param>
        /// <returns>结果</returns>
        public static Coroutine Co(this MonoBehaviour monoBehaviour, Func<IEnumerator> coroutine)
        {
            return monoBehaviour.StartCoroutine(CoCoroutine(coroutine));
        }

        /// <summary>
        /// 重启携程
        /// </summary>
        /// <param name="monoBehaviour">MonoBehaviour</param>
        /// <param name="methodName">方法名</param>
        /// <returns>结果</returns>
        public static Coroutine RestartCoroutine(this MonoBehaviour monoBehaviour, string methodName)
        {
            monoBehaviour.StopCoroutine(methodName);
            return monoBehaviour.StartCoroutine(methodName);
        }

        /// <summary>
        /// 同步执行协程并返回
        /// </summary>
        /// <param name="monoBehaviour">MonoBehaviour</param>
        /// <param name="enumerator">协程</param>
        /// <returns>协程</returns>
        public static Coroutine StartCoroutineSync(this MonoBehaviour monoBehaviour, IEnumerator enumerator)
        {
            return monoBehaviour.StartCoroutine(ToFixedCoroutine(enumerator)); ;
        }

        #region Private

        private static IEnumerator ExecuteWhenCoroutine(Action action, Func<bool> condition)
        {
            while (condition != null && !condition())
            {
                yield return null;
            }

            action();
        }

        private static IEnumerator ExecuteUntilCoroutine(Action action, Func<bool> condition)
        {
            while (condition != null && !condition())
            {
                action();
                yield return null;
            }
        }

        private static IEnumerator ExecuteDelayCoroutine(Action action, float seconds, bool timeScale)
        {
            if (timeScale)
            {
                yield return new WaitForSeconds(seconds);
            }
            else
            {
                yield return new WaitForSecondsRealtime(seconds);
            }

            action();
        }

        private static IEnumerator ExecuteNextFrameCoroutine(Action action, int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                yield return null;
                action();
            }
        }

        private static IEnumerator ExecuteEndOfFrameCoroutine(Action action)
        {
            yield return new WaitForEndOfFrame();
            action();
        }

        private static IEnumerator ExecuteAfterFramesCoroutine(Action action, int frames)
        {
            var count = 0;
            while (count < frames)
            {
                count++;
                yield return null;
            }

            action();
        }

        private static IEnumerator CoCoroutine(Func<IEnumerator> coroutine)
        {
            yield return coroutine;
        }

        private static IEnumerator ToFixedCoroutine(IEnumerator enumerator)
        {
            var parentsStack = new Stack<IEnumerator>();
            var currentEnumerator = enumerator;
            parentsStack.Push(currentEnumerator);
            while (parentsStack.Count > 0)
            {
                currentEnumerator = parentsStack.Pop();
                while (currentEnumerator.MoveNext())
                {
                    if (currentEnumerator.Current is IEnumerator subEnumerator)
                    {
                        parentsStack.Push(currentEnumerator);
                        currentEnumerator = subEnumerator;
                    }
                    else
                    {
                        if (currentEnumerator.Current is bool check && check) continue;
                        yield return currentEnumerator.Current;
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}