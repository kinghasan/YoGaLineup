using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [AddComponentMenu("UTween Pro/UTween Manager")]
    public partial class UTweenManager : MonoBehaviour
    {
        #region Singleton

        private static bool ApplicationIsQuitting = false;
        internal static UTweenManager Instance;

        public static UTweenManager Ins
        {
            get
            {
                if (Application.isPlaying && ApplicationIsQuitting)
                {
                    return null;
                }

                if (Instance != null) return Instance;
                Instance = FindObjectOfType<UTweenManager>();
                if (Instance != null) return Instance;
                var hideFlag = UTweenSetting.Ins.ShowManagerInHierarchy ? HideFlags.None : HideFlags.HideAndDontSave;
                var insName = nameof(UTween);
                if (!Application.isPlaying)
                {
                    insName += " (Editor)";
                }

                var obj = new GameObject
                {
                    name = insName,
                    hideFlags = hideFlag,
                };

                Instance = obj.AddComponent<UTweenManager>();

                return Instance;
            }
        }

        #endregion

        #region Cache

        internal HashSet<TweenData> AddList { get; set; } = new HashSet<TweenData>();
        internal HashSet<TweenData> RemoveList { get; set; } = new HashSet<TweenData>();
        internal HashSet<TweenData> PlayingList { get; set; } = new HashSet<TweenData>();

        internal Dictionary<UpdateMode, HashSet<TweenData>> UpdateListDic
        {
            get
            {
                if (_updateListDic == null)
                {
                    _updateListDic = new Dictionary<UpdateMode, HashSet<TweenData>>()
                    {
                        {UpdateMode.Update, UpdateList},
                        {UpdateMode.LateUpdate, LateUpdateList},
                        {UpdateMode.FixedUpdate, FixedUpdateList},
                        {UpdateMode.WaitForEndOfFrame, WaitForFixedUpdateList},
                        {UpdateMode.WaitForFixedUpdate, WaitForEndOfFrameList}
                    };
                }

                return _updateListDic;
            }
        }

        private Dictionary<UpdateMode, HashSet<TweenData>> _updateListDic;

        internal HashSet<TweenData> UpdateList { get; set; } = new HashSet<TweenData>();
        internal HashSet<TweenData> LateUpdateList { get; set; } = new HashSet<TweenData>();
        internal HashSet<TweenData> FixedUpdateList { get; set; } = new HashSet<TweenData>();
        internal HashSet<TweenData> WaitForFixedUpdateList { get; set; } = new HashSet<TweenData>();
        internal HashSet<TweenData> WaitForEndOfFrameList { get; set; } = new HashSet<TweenData>();

        #endregion

        #region MonoBehaviour

        protected void Awake()
        {
            if (!Application.isPlaying) return;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(WaitForFixedUpdate());
            StartCoroutine(WaitForEndOfFrame());
        }

        protected void OnEnable()
        {

        }

        protected void OnDisable()
        {

        }

        protected IEnumerator WaitForFixedUpdate()
        {
            var wait = new WaitForFixedUpdate();
            while (true)
            {
                yield return wait;
                if (WaitForFixedUpdateList.Count == 0) yield return null;
                var scaledDeltaTime = Time.fixedDeltaTime;
                var unscaledDeltaTime = Time.fixedUnscaledDeltaTime;
                var smoothDeltaTime = Time.fixedDeltaTime;
                UpdateImpl(WaitForFixedUpdateList, scaledDeltaTime, unscaledDeltaTime, smoothDeltaTime);
            }
        }

        protected IEnumerator WaitForEndOfFrame()
        {
            var wait = new WaitForEndOfFrame();
            while (true)
            {
                yield return wait;
                if (WaitForEndOfFrameList.Count == 0) yield return null;
                var scaledDeltaTime = Time.deltaTime;
                var unscaledDeltaTime = Time.unscaledDeltaTime;
                var smoothDeltaTime = Time.smoothDeltaTime;
                UpdateImpl(WaitForEndOfFrameList, scaledDeltaTime, unscaledDeltaTime, smoothDeltaTime);
            }
        }

        protected void Update()
        {
            if (UpdateList.Count == 0) return;
            var scaledDeltaTime = Time.deltaTime;
            var unscaledDeltaTime = Time.unscaledDeltaTime;
            var smoothDeltaTime = Time.smoothDeltaTime;
            UpdateImpl(UpdateList, scaledDeltaTime, unscaledDeltaTime, smoothDeltaTime);
            // TODO.. 13 - 15 ms
        }

        internal void PerformanceTest(Action action)
        {
            var start = Time.realtimeSinceStartup;
            action();
            var end = Time.realtimeSinceStartup;
            var total = end - start;
            var time = total * 1000;
            Debug.Log("Time : " + time);
        }

        protected void LateUpdate()
        {
            if (LateUpdateList.Count == 0) return;
            var scaledDeltaTime = Time.deltaTime;
            var unscaledDeltaTime = Time.unscaledDeltaTime;
            var smoothDeltaTime = Time.smoothDeltaTime;
            UpdateImpl(LateUpdateList, scaledDeltaTime, unscaledDeltaTime, smoothDeltaTime);
        }

        protected void FixedUpdate()
        {
            if (FixedUpdateList.Count == 0) return;
            var scaledDeltaTime = Time.fixedDeltaTime;
            var unscaledDeltaTime = Time.fixedUnscaledDeltaTime;
            var smoothDeltaTime = Time.smoothDeltaTime;
            UpdateImpl(FixedUpdateList, scaledDeltaTime, unscaledDeltaTime, smoothDeltaTime);
        }

        protected void OnDestroy()
        {
            if (Application.isPlaying)
            {
                ApplicationIsQuitting = true;
            }

            Instance = null;
        }

        #endregion

        #region Add / Remove TweenData

        public void AddTweenData(TweenData tweenData)
        {
            if (PlayingList.Contains(tweenData)) return;
            if (AddList.Contains(tweenData)) return;
            AddList.Add(tweenData);
        }

        public void RemoveTweenData(TweenData tweenData)
        {
            if (!PlayingList.Contains(tweenData)) return;
            if (RemoveList.Contains(tweenData)) return;
            RemoveList.Add(tweenData);
        }

        public void AddTweener(Tweener tweener)
        {
            AddTweenData(tweener.Data);
        }

        public void RemoveTweener(Tweener tweener)
        {
            RemoveTweenData(tweener.Data);
        }

        internal void SyncPlayingList()
        {
            if (RemoveList.Count > 0)
            {
                foreach (var tweenData in RemoveList)
                {
                    PlayingList.Remove(tweenData);
                    var updateList = UpdateListDic[tweenData.UpdateMode];
                    updateList.Remove(tweenData);
                    if (!AddList.Contains(tweenData))
                    {
                        tweenData.DeSpawn();
                    }
                }

                RemoveList.Clear();
            }

            if (AddList.Count > 0)
            {
                foreach (var tweenData in AddList)
                {
                    PlayingList.Add(tweenData);
                    var updateList = UpdateListDic[tweenData.UpdateMode];
                    updateList.Add(tweenData);
                }

                AddList.Clear();
            }
        }

        #endregion

        internal void UpdateImpl(HashSet<TweenData> updateList, float scaledDeltaTime, float unscaledDeltaTime, float smoothDeltaTime)
        {
            SyncPlayingList();
            foreach (var tweenData in updateList)
            {
                try
                {
                    tweenData.UpdateInternal(scaledDeltaTime, unscaledDeltaTime, smoothDeltaTime);
                }
                catch (Exception exception)
                {
                    UTweenCallback.OnException(exception);
                }
            }
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(UTweenManager))]
    public class UTweenManagerEditor : Editor
    {
        public virtual UTweenManager Target => target as UTweenManager;
        public UTweenManager TweenManager => Target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawPlayStats();
            DrawPoolStats();

            if (Application.isPlaying)
            {
                Repaint();
            }
        }

        public void DrawPlayStats()
        {
            GUILayout.Label($"Play:{Target.PlayingList.Count}");
        }

        public void DrawPoolStats()
        {
            using (GUIVertical.Create())
            {
                foreach (var kv in Pool.PoolListDic)
                {
                    var type = kv.Key;
                    var poolList = kv.Value;

                    GUILayout.Label(type.Name);
                    GUILayout.Label($"<color=green>{poolList.ActiveCount}</color>/<color=yellow>{poolList.DeActiveCount}</color>/<color=white>{poolList.Count}</color>", EditorStyle.RichLabel);
                }
            }
        }
    }

#endif

}
