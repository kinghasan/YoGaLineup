using System.Collections.Generic;
using UnityEngine;

namespace Aya.TweenPro
{
    public abstract partial class TweenPathBase : TweenValueFloat<Transform>
    {
        public float Precision;
        public const float DefaultPrecision = 0.001f;

        public override bool SupportSetCurrentValue => false;
        public override bool SupportSpeedBased => true;

        public override float Value { get; set; }

        public Vector3 ValuePosition
        {
            get => Target.position;
            set => Target.position = value;
        }

        protected Vector3 StartPosition;

        protected bool IsCached = false;
        protected List<Vector3> CachePositionList;
        protected List<float> CacheDistanceList;
        protected float Length;

        public virtual void RefreshCache()
        {
            Length = 0f;
            CachePositionList = new List<Vector3>();
            if (Precision < 1e-6) Precision = DefaultPrecision;
            for (var factor = 0f; factor <= 1f; factor += Precision)
            {
                var point = GetPositionByFactor(factor);
                CachePositionList.Add(point);
            }

            CacheDistanceList = new List<float>();
            for (var i = 0; i < CachePositionList.Count - 1; i++)
            {
                var p1 = CachePositionList[i];
                var p2 = CachePositionList[i + 1];
                var distance = Vector3.Distance(p1, p2);
                CacheDistanceList.Add(distance);
                Length += distance;
            }

            IsCached = true;
        }

        public override float GetSpeedBasedDuration()
        {
            RefreshCache();
            var speed = Duration;
            var duration = Length / speed;
            return duration;
        }

        public override void Sample(float factor)
        {
            factor = Mathf.Clamp01(factor);
            ValuePosition = Data.SpeedBased ? GetPositionByFactorSpeedBased(factor) : GetPositionByFactor(factor);
        }

        public virtual Vector3 GetPositionByFactorSpeedBased(float factor)
        {
            if (!IsCached) RefreshCache();
            var distance = factor * Length;
            var disCount = 0f;
            for (var i = 0; i < CacheDistanceList.Count; i++)
            {
                var dis = CacheDistanceList[i];
                if (disCount + dis >= distance)
                {
                    var p1 = CachePositionList[i];
                    var p2 = CachePositionList[i + 1];
                    var diff = disCount + dis - distance;
                    var delta = diff / dis;
                    var pos = Vector3.Lerp(p1, p2, delta);
                    return pos;
                }

                disCount += dis;
            }

            return default;
        }

        public abstract Vector3 GetPositionByFactor(float factor);

        public override void Reset()
        {
            base.Reset();
            Precision = DefaultPrecision;
        }

        public override void RecordObject()
        {
            StartPosition = ValuePosition;
        }

        public override void RestoreObject()
        {
            ValuePosition = StartPosition;
        }

        public override void OnDrawGizmos()
        {
            if (Target == null) return;
            if (Precision < 1e-6) Precision = DefaultPrecision;
            Gizmos.color = Color.green;
            for (var factor = 0f; factor <= 1f - Precision; factor += Precision)
            {
                var p1 = GetPositionByFactor(factor);
                var p2 = GetPositionByFactor(factor + Precision);
                Gizmos.DrawLine(p1, p2);
            }
        }
    }

#if UNITY_EDITOR

    public abstract partial class TweenPathBase : TweenValueFloat<Transform>
    {
        public override void DrawFromToValue()
        {
            base.DrawFromToValue();
            From = Mathf.Clamp01(From);
            To = Mathf.Clamp01(To);
        }
    }

#endif

    #region Extensnion

    public static partial class TweenPathExtension
    {
        public static TTweener SetPrecision<TTweener>(this TTweener tweener, float precision) where TTweener : TweenPathBase
        {
            tweener.Precision = precision;
            return tweener;
        }
    }

    #endregion

}
