using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class Tweener
    {
        public bool Active;
        public float Duration;
        public float Delay;
        public bool HoldStart;
        public bool HoldEnd;
        public int Ease;
        public float Strength;
        public AnimationCurve Curve;
        public SpaceMode Space;

        [SerializeField] internal bool FoldOut = true;
        [SerializeField] internal DurationMode DurationMode = DurationMode.DurationDelay;

        public virtual bool SupportTarget => true;
        public virtual bool SupportFromTo => true;
        public virtual bool SupportSpace => false;
        public virtual bool SupportSpeedBased => false;
        public virtual bool SupportSetCurrentValue => true;
        public virtual bool SupportOnUpdate => true;
        public virtual bool SupportIndependentAxis => false;
        public virtual bool OneTimeTweener => false;
        public virtual int AxisCount => 1;

        [NonSerialized] public float Factor;

        public bool IsPrepared { get; set; }
        public bool SingleMode => Data != null && Data.SingleMode;
        public float TotalDuration => Delay + Duration;

        // TODO.. 用于确保子动画能结束在最终状态的临时解决方案，待替换实现
        internal bool IsCurrentLoopFinished;

        #region Cache

        public float DurationFrom
        {
            get => Delay;
            set
            {
                if (value < 0) Delay = 0;
                else Delay = value;
            }
        }

        public float DurationTo
        {
            get => Delay + Duration;
            set
            {
                if (value > Delay)
                {
                    Duration = value - Delay;
                }
                else
                {
                    if (value < 0)
                    {
                        Delay = 0;
                    }
                    else
                    {
                        Delay = value;
                    }

                    Duration = 0;
                }
            }
        }


        public float DurationFromNormalized
        {
            get => DurationFrom / Data.Duration;
            set => DurationFrom = Data.Duration * value;
        }

        public float DurationToNormalized
        {
            get => DurationTo / Data.Duration;
            set => DurationTo = Data.Duration * value;
        }

        public EaseFunction CacheEaseFunction
        {
            get
            {
                if (_cacheEaseFunction == null || _cacheEaseFunction.Type != Ease)
                {
                    _cacheEaseFunction = EaseType.FunctionDic[Ease];
                }

                return _cacheEaseFunction;
            }
        }

        private EaseFunction _cacheEaseFunction;

        public TweenData Data { get; internal set; }
        public bool IsCustomCurve => Ease < 0;

        #endregion

        public virtual void PreSample()
        {
            IsPrepared = true;
            IsCurrentLoopFinished = false;
        }

        public virtual void StopSample()
        {

        }

        public virtual float GetSpeedBasedDuration()
        {
            return Duration;
        }

        public virtual float GetFactor(float normalizedDuration)
        {
            var currentDuration = Data.Duration * normalizedDuration;
            var delta = float.NaN;
            if (!Data.SingleMode)
            {
                if (currentDuration < Delay)
                {
                    delta = 0f;
                    if (!IsCurrentLoopFinished && !Data.Forward)
                    {
                        IsCurrentLoopFinished = true;
                    }
                    else if (!HoldStart)
                    {
                        Factor = delta;
                        return float.NaN;
                    }
                }

                if (currentDuration > Delay + Duration)
                {
                    delta = 1f;
                    if (!IsCurrentLoopFinished && Data.Forward)
                    {
                        IsCurrentLoopFinished = true;
                    }
                    else if (!HoldStart)
                    {
                        Factor = delta;
                        return float.NaN;
                    }
                }
            }

            if (float.IsNaN(delta))
            {
                delta = (currentDuration - Delay) / Duration;
            }
            
            var factor = IsCustomCurve ? Curve.Evaluate(delta) : CacheEaseFunction.Ease(0f, 1f, delta, Strength);
            Factor = factor;
            return factor;
        }

        public abstract void Sample(float factor);

        // Editor Only
        public virtual void SetDirty()
        {

        }

        // Editor Only
        public virtual void RecordObject()
        {

        }

        // Editor Only
        public virtual void RestoreObject()
        {

        }

        public virtual void OnAdded()
        {

        }

        public virtual void OnRemoved()
        {

        }

        public virtual void Reset()
        {
            ResetCallback();
            IsPrepared = false;
            Active = true;
            Duration = 1f;
            Delay = 0f;
            HoldStart = false;
            HoldEnd = false;
            Ease = EaseType.Linear;
            Strength = 1f;
            Curve = new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));
            Space = SpaceMode.World;
#if UNITY_EDITOR
            // ShowEvent = false;
            if (Data != null)
            {
                Duration = Data.Duration;
            }
#endif
        }

        public virtual void ResetCallback()
        {

        }

        public virtual void ReverseFromTo()
        {

        }

        public virtual void OnDrawGizmos()
        {

        }
    }
}
