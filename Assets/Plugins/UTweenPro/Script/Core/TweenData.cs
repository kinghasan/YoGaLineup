using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public partial class TweenData
    {
        public string Identifier;
        public float Duration;
        public float Delay;
        public bool Backward;

        public PlayMode PlayMode;
        public int PlayCount;
        public AutoPlayMode AutoPlay;
        public UpdateMode UpdateMode;
        public float Interval;
        public float Interval2;
        public TimeMode TimeMode;
        public float SelfScale;
        public PreSampleMode PreSampleMode;
        public bool AutoKill;
        public bool SpeedBased;

        [SerializeReference] 
        public List<Tweener> TweenerList = new List<Tweener>();

        public OnPlayEvent OnPlay = new OnPlayEvent();
        public OnStartEvent OnStart = new OnStartEvent();
        public OnLoopStartEvent OnLoopStart = new OnLoopStartEvent();
        public OnLoopEndEvent OnLoopEnd = new OnLoopEndEvent();
        public OnUpdateEvent OnUpdate = new OnUpdateEvent();
        public OnPauseEvent OnPause = new OnPauseEvent();
        public OnResumeEvent OnResume = new OnResumeEvent();
        public OnStopEvent OnStop = new OnStopEvent();
        public OnCompleteEvent OnComplete = new OnCompleteEvent();

        [SerializeField] internal bool FoldOut = true;
        [SerializeField] internal bool FoldOutEvent = false;
        [SerializeField] internal bool EnableIdentifier = false;
        [SerializeField] internal EventType EventType = EventType.OnPlay;

        // [NonSerialized] public List<Tweener> PlayingTweenerList = new List<Tweener>();

        #region State Property

        public Tweener Tweener => TweenerList.Count > 0 ? TweenerList[0] : default;
        public Tweener FirstTweener => Tweener;
        public Tweener LastTweener => TweenerList.Count > 0 ? TweenerList[TweenerList.Count - 1] : default;
        public bool IsSubAnimation { get; internal set; }
        public PlayState State { get; internal set; }
        public bool IsInitialized { get; internal set; }
        public bool Forward { get; internal set; }
        public bool StartForward { get; internal set; }
        public int LoopCounter { get; internal set; }
        public int FrameCounter { get; internal set; }
        public bool IsDelaying { get; internal set; }
        public bool IsPlaying => State == PlayState.Playing;
        public bool IsInterval { get; internal set; }
        public bool IsCompleted => State == PlayState.Completed;
        public bool IsInProgress => State == PlayState.Playing || State == PlayState.Paused;
        public float CurrentInterval { get; internal set; }
        public bool SingleMode => TweenerList.Count == 1;
        public float DelayTimer { get; internal set; }
        public float PlayTimer { get; internal set; }
        public float IntervalTimer { get; internal set; }
        public float RuntimeDuration { get; internal set; }

        public float Progress
        {
            get => PlayTimer;
            internal set => PlayTimer = value;
        }

        public float NormalizedProgress
        {
            get
            {
                if (Application.isPlaying) return RuntimeNormalizedProgress;
#if UNITY_EDITOR
                if (!Application.isPlaying) return EditorNormalizedProgress;
#endif
                return default;
            }
            internal set
            {
                if (Application.isPlaying) RuntimeNormalizedProgress = value;
#if UNITY_EDITOR
                if (!Application.isPlaying) EditorNormalizedProgress = value;
#endif
            }
        }

        public float RuntimeNormalizedProgress { get; internal set; }
        public UTweenAnimation TweenAnimation { get; internal set; }
        public TweenControlMode ControlMode { get; internal set; }

        #endregion

        #region Mono Behaviour

        public virtual void Awake()
        {
            if (PreSampleMode == PreSampleMode.Awake)
            {
                PreSample(); 
                Sample(0f);
            }

            if (AutoPlay == AutoPlayMode.Awake) Play();
        }

        public virtual void OnEnable()
        {
            if (PreSampleMode == PreSampleMode.Enable)
            {
                PreSample();
                Sample(0f);
            }

            if (AutoPlay == AutoPlayMode.Enable) Play();
        }

        public virtual void Start()
        {
            if (PreSampleMode == PreSampleMode.Start)
            {
                PreSample();
                Sample(0f);
            }

            if (AutoPlay == AutoPlayMode.Start) Play();
        }

        public virtual void OnDisable()
        {
            if (IsPlaying)
            {
                Stop();
            }
        }

        #endregion

        #region Play / Pasue / Resume / Stop

        public TweenData Play(bool forward = true)
        {
            if (State == PlayState.Playing) return this;
            if (State != PlayState.Paused)
            {
                IsInitialized = false;
                StartForward = forward && !Backward;
                foreach (var tweener in TweenerList)
                {
                    tweener.IsPrepared = false;
                }

                if (Application.isPlaying)
                {
                    UTweenManager.Ins.AddTweenData(this);
                }
                else
                {
#if UNITY_EDITOR
                    TweenAnimation.PreviewStart();
#endif
                }
            }

            State = PlayState.Playing;
            return this;
        }

        public TweenData Pause()
        {
            if (State != PlayState.Playing) return this;
            State = PlayState.Paused;
            OnPause.Invoke();
            return this;
        }

        public TweenData Resume()
        {
            if (State != PlayState.Paused) return this;
            State = PlayState.Playing;
            OnResume.Invoke();
            return this;
        }

        public TweenData Stop()
        {
            if (State != PlayState.Completed)
            {
                State = PlayState.Stopped;
            }

            RuntimeNormalizedProgress = 0f;
#if UNITY_EDITOR
            EditorNormalizedProgress = 0f;
#endif
            OnStop.Invoke();

            if (Application.isPlaying)
            {
                UTweenManager.Ins?.RemoveTweenData(this);
            }
            else
            {
#if UNITY_EDITOR
                TweenAnimation.PreviewEnd();
#endif
            }

            return this;
        }

        public TweenData PlayForward()
        {
            Play(true);
            return this;
        }

        public TweenData PlayBackward()
        {
            Play(false);
            return this;
        }

        #endregion

        #region Add / Remove Tweener

        public void AddTweener(Tweener tweener)
        {
            if (TweenerList.Contains(tweener)) return;
            if (tweener.Data != null)
            {
                tweener.Data.RemoveTweener(tweener);
                tweener.Data = null;
            }

            TweenerList.Add(tweener);
            tweener.Data = this;
        }

        public void RemoveTweener(Tweener tweener)
        {
            if (!TweenerList.Contains(tweener)) return;
            TweenerList.Remove(tweener);
            tweener.Data = null;
        }

        #endregion

        #region Initialize / Update / Sample

        internal void Initialize(bool isPreview = false)
        {
            FrameCounter = 0;
            PlayTimer = 0f;
            Forward = StartForward;
            IsDelaying = false;
            DelayTimer = 0f;
            IsInterval = false;
            IntervalTimer = 0f;
            LoopCounter = 0;

            if (SingleMode && SpeedBased)
            {
                RuntimeDuration = Tweener.GetSpeedBasedDuration();
            }
            else
            {
                RuntimeDuration = Duration;
            }

            PreSample();

            if (!isPreview)
            {
                if (Delay > 0f)
                {
                    IsDelaying = true;
                }

                State = PlayState.Playing;
            }
            
            IsInitialized = true;
        }

        internal void UpdateInternal(float scaledDeltaTime, float unscaledDeltaTime, float smoothDeltaTime)
        {
            var deltaTime = 0f;
            if (TimeMode == TimeMode.Normal) deltaTime = scaledDeltaTime;
            else if (TimeMode == TimeMode.UnScaled) deltaTime = unscaledDeltaTime;
            else if (TimeMode == TimeMode.Smooth) deltaTime = smoothDeltaTime;
            deltaTime *= SelfScale;
            Update(deltaTime);
        }

        public void Update(float deltaTime)
        {
            if (!IsInitialized)
            {
                Initialize();
            }

            if (FrameCounter == 0)
            {
                OnPlay.Invoke();
                if (PlayMode != PlayMode.Once)
                {
                    OnLoopStart.Invoke();
                }
            }

            if (IsDelaying)
            {
                DelayTimer += deltaTime;
                PlayTimer = 0f;
                if (DelayTimer >= Delay)
                {
                    IsDelaying = false;
                }

                return;
            }
            
            if (IsInterval)
            {
                IntervalTimer += deltaTime;
                if (IntervalTimer >= CurrentInterval)
                {
                    IsInterval = false;
                    OnLoopStart.Invoke();
                }

                return;
            }
            
            if (State == PlayState.Playing)
            {
                if (FrameCounter == 0)
                {
                    OnStart.Invoke();
                }

                PlayTimer += deltaTime;
                FrameCounter++;
            }

            if (PlayTimer < RuntimeDuration)
            {
                RuntimeNormalizedProgress = Forward ? PlayTimer / RuntimeDuration : (RuntimeDuration - PlayTimer) / RuntimeDuration;
                Sample(RuntimeNormalizedProgress);
                // TODO.. 3 ms
                OnUpdate.Invoke();
            }
            else
            {
                foreach (var tweener in TweenerList)
                {
                    tweener.IsCurrentLoopFinished = false;
                }

                if (PlayMode == PlayMode.Once)
                {
                    RuntimeNormalizedProgress = 1f;
                    Sample(RuntimeNormalizedProgress);
                    Complete();
                    Stop();
                }
                else if (PlayMode == PlayMode.Loop)
                {
                    LoopCounter++;
                    PlayTimer = 0f;
                    RuntimeNormalizedProgress = 1f;
                    Sample(RuntimeNormalizedProgress);
                    if (LoopCounter >= PlayCount && PlayCount > 0)
                    {
                        OnLoopEnd.Invoke();
                        Complete();
                        Stop();
                    }
                    else
                    {
                        if (Interval > 0)
                        {
                            IsInterval = true;
                            IntervalTimer = 0f;
                            CurrentInterval = Interval;
                        }
                        else
                        {
                            OnLoopStart.Invoke();
                            foreach (var tweener in TweenerList)
                            {
                                tweener.IsCurrentLoopFinished = false;
                            }
                        }
                    }
                }
                else if (PlayMode == PlayMode.PingPong)
                {
                    PlayTimer = 0f;
                    RuntimeNormalizedProgress = Forward ? 1f : 0f;
                    Sample(RuntimeNormalizedProgress);
                    Forward = !Forward;
                    if (Forward == StartForward) LoopCounter++;
                    if (LoopCounter >= PlayCount && PlayCount > 0)
                    {
                        OnLoopEnd.Invoke();
                        Complete();
                        Stop();
                    }
                    else
                    {
                        CurrentInterval = Forward == StartForward ? Interval : Interval2;
                        if (CurrentInterval > 0)
                        {
                            IsInterval = true;
                            IntervalTimer = 0f;
                        }
                        else
                        {
                            OnLoopStart.Invoke();
                            foreach (var tweener in TweenerList)
                            {
                                tweener.IsCurrentLoopFinished = false;
                            }
                        }
                    }
                }
            }
        }

        public void Sample(float normalizedDuration)
        {
            try
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    if (Mode == TweenEditorMode.Component && !PreviewSampled)
                    {
                        PreviewSampled = true;
                        RecordObject();
                    }

                    if (!IsPlaying)
                    {
                        PreSample();
                    }
                }
#endif

                foreach (var tweener in TweenerList)
                {
                    if (tweener.Data == null) tweener.Data = this;
                    if (!tweener.Active) continue;
                    // TODO.. 1 ms
                    var factor = tweener.GetFactor(normalizedDuration);
                    if (float.IsNaN(factor)) continue;
                    // TODO.. 7 ms
                    tweener.Sample(factor);
                }

#if UNITY_EDITOR
                SetDirty();
#endif
            }
            catch (Exception exception)
            {
                UTweenCallback.OnException(exception);
            }
        }

        internal void PreSample()
        {
            foreach (var tweener in TweenerList)
            {
                tweener.Data = this;
                if (!tweener.Active) continue;
                tweener.PreSample();
            }
        }

        internal void StopSample()
        {
            foreach (var tweener in TweenerList)
            {
                tweener.StopSample();
            }
        }

        internal void Complete()
        {
            State = PlayState.Completed;
            OnComplete.Invoke();
        }

        #endregion

        #region Record / Restore / SetDirty

        // Editor Only
        public void SetDirty()
        {
            if (Application.isPlaying) return;
            foreach (var tweener in TweenerList)
            {
                tweener.SetDirty();
            }
        }

        // Editor Only
        public void RecordObject()
        {
            if (Application.isPlaying) return;
            foreach (var tweener in TweenerList)
            {
                try
                {
                    tweener.RecordObject();
                }
                catch
                {
                    //
                }
            }
        }

        // Editor Only
        public void RestoreObject()
        {
            if (Application.isPlaying) return;
            foreach (var tweener in TweenerList)
            {
                try
                {
                    tweener.RestoreObject();
                }
                catch
                {
                    //
                }
            }
        }

        #endregion

        #region Reset / DeSpawn

        public void Reset()
        {
            IsInitialized = false;
            Duration = 1f;
            Delay = 0f;
            Backward = false;
            PlayMode = PlayMode.Once;
            PlayCount = 1;
            AutoPlay = AutoPlayMode.None;
            UpdateMode = UpdateMode.Update;
            Interval = 0f;
            Interval2 = 0f;
            TimeMode = TimeMode.Normal;
            SelfScale = 1f;
            PreSampleMode = PreSampleMode.Enable;
            AutoKill = false;
            SpeedBased = false;

            foreach (var tweener in TweenerList)
            {
                tweener.Reset();
            }

            ResetCallback();
        }

        public virtual void ResetCallback()
        {
            OnPlay.Reset();
            OnLoopStart.Reset();
            OnLoopEnd.Reset();
            OnUpdate.Reset();
            OnPause.Reset();
            OnResume.Reset();
            OnStop.Reset();
            OnComplete.Reset();
        }

        internal void DeSpawn()
        {
            StopSample();

            if (ControlMode == TweenControlMode.Component)
            {
                if (Application.isPlaying && AutoKill) Object.Destroy(TweenAnimation.gameObject);
                return;
            }

            foreach (var tweener in TweenerList)
            {
                Pool.DeSpawn(tweener);
            }

            TweenerList.Clear();
            Pool.DeSpawn(this);
        }

        #endregion

#if UNITY_EDITOR

        public void OnDrawGizmos()
        {
            foreach (var tweener in TweenerList)
            {
                if (!tweener.Active || !tweener.FoldOut) continue;
                tweener.OnDrawGizmos();
            }
        }

#endif
    }
}
