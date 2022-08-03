/////////////////////////////////////////////////////////////////////////////
//
//  Script   : UnityThread.cs
//  Info     : Unity线程，可以将分线程的任务以委托的形式附加到主线程上执行
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Warning  : 该类是依赖unity monobehaviour，所以执行不是立即的，与调用时机有关，最多会延迟一帧
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Async
{
    public class UnityThread : MonoBehaviour
    {
        #region Instance

        protected static UnityThread Instance;

        public static UnityThread Ins
        {
            get
            {
                if (Instance != null) return Instance;
                Instance = (UnityThread) FindObjectOfType(typeof(UnityThread));
                if (Instance != null) return Instance;
                var obj = new GameObject
                {
                    hideFlags = HideFlags.HideAndDontSave,
                    name = "UnityThread"
                };
                DontDestroyOnLoad(obj);
                Instance = obj.AddComponent<UnityThread>();
                return Instance;
            }
        }

        protected UnityThread()
        {
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        protected static void Init()
        {
            var ins = Ins;
        }

        #endregion

        #region Private

        protected static bool NoUpdate = true;
        protected static List<Action> UpdateQueue = new List<Action>();
        protected static List<Action> UpdateRunQueue = new List<Action>();
        protected static bool NoLateUpdate = true;
        protected static List<Action> LateUpdateQueue = new List<Action>();
        protected static List<Action> LateUpdateRunQueue = new List<Action>();
        protected static bool NoFixedUpdate = true;
        protected static List<Action> FixedUpdateQueue = new List<Action>();
        protected static List<Action> FixedUpdateRunQueue = new List<Action>();

        #endregion

        #region Public

        /// <summary>
        /// 附加到 Update，当满足条件时执行
        /// </summary>
        /// <param name="action">action</param>
        /// <param name="condition">condition</param>
        public static void ExecuteWhen(Action action, Func<bool> condition)
        {
            ExecuteCoroutine(ExecuteWhenCoroutine(action, condition));
        }

        /// <summary>
        /// 附加到 Update 执行
        /// </summary>
        /// <param name="action">action</param>
        public static void ExecuteUpdate(Action action)
        {
            lock (UpdateQueue)
            {
                UpdateQueue.Add(action);
                NoUpdate = false;
            }
        }

        /// <summary>
        /// 附加到 LateUpdate 执行
        /// </summary>
        /// <param name="action">action</param>
        public static void ExecuteLateUpdate(Action action)
        {
            lock (LateUpdateQueue)
            {
                LateUpdateQueue.Add(action);
                NoLateUpdate = false;
            }
        }

        /// <summary>
        /// 附加到 FixedUpdate 执行
        /// </summary>
        /// <param name="action">action</param>
        public static void ExecuteFixedUpdate(Action action)
        {
            lock (FixedUpdateQueue)
            {
                FixedUpdateQueue.Add(action);
                NoFixedUpdate = false;
            }
        }

        /// <summary>
        /// 在主线程上执行一个协程
        /// </summary>
        /// <param name="coroutine">协程</param>
        public static void ExecuteCoroutine(IEnumerator coroutine)
        {
            ExecuteUpdate(() => { Ins.StartCoroutine(coroutine); });
        }

        /// <summary>
        /// 在主线程上开启一个协程延时执行
        /// </summary>
        /// <param name="action">action</param>
        /// <param name="delayTime">延迟时间</param>
        /// <param name="timeScale">时间缩放</param>
        public static void ExecuteDelay(Action action, float delayTime, bool timeScale = true)
        {
            ExecuteCoroutine(ExecuteDelayCoroutine(action, delayTime, timeScale));
        }

        #endregion

        #region Update / LateUpdate / FixedUpdate

        private void Update()
        {
            if (NoUpdate) return;
            lock (UpdateQueue)
            {
                UpdateRunQueue.AddRange(UpdateQueue);
                UpdateQueue.Clear();
                NoUpdate = true;
                for (var i = 0; i < UpdateRunQueue.Count; i++)
                {
                    var action = UpdateRunQueue[i];
                    action?.Invoke();
                }

                UpdateRunQueue.Clear();
            }
        }

        private void LateUpdate()
        {
            if (NoLateUpdate) return;
            lock (LateUpdateQueue)
            {
                LateUpdateRunQueue.AddRange(LateUpdateQueue);
                LateUpdateQueue.Clear();
                NoLateUpdate = true;

                for (var i = 0; i < LateUpdateRunQueue.Count; i++)
                {
                    var action = LateUpdateRunQueue[i];
                    action?.Invoke();
                }

                LateUpdateRunQueue.Clear();
            }
        }

        private void FixedUpdate()
        {
            if (NoFixedUpdate) return;
            lock (FixedUpdateQueue)
            {
                FixedUpdateRunQueue.AddRange(FixedUpdateQueue);
                FixedUpdateQueue.Clear();
                NoFixedUpdate = true;
                for (var i = 0; i < FixedUpdateRunQueue.Count; i++)
                {
                    var action = FixedUpdateRunQueue[i];
                    action?.Invoke();
                }

                FixedUpdateRunQueue.Clear();
            }
        }

        #endregion

        #region Coroutine

        private static IEnumerator ExecuteDelayCoroutine(Action action, float delayTime, bool timeScale = true)
        {
            if (timeScale)
            {
                yield return new WaitForSeconds(delayTime);
            }
            else
            {
                yield return new WaitForSecondsRealtime(delayTime);
            }

            action();
        }

        private static IEnumerator ExecuteWhenCoroutine(Action action, Func<bool> condition)
        {
            while (condition != null && !condition())
            {
                yield return null;
            }

            action();
        }

        #endregion
    }
}

