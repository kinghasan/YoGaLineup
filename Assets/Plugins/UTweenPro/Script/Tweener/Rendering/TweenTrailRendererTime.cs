using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Trail Renderer Time", "Rendering")]
    [Serializable]
    public class TweenTrailRendererTime : TweenValueFloat<TrailRenderer>
    {
        public override float Value
        {
            get => Target.time;
            set => Target.time = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenTrailRendererTime Time(TrailRenderer trailRenderer, float to, float duration)
        {
            var tweener = Play<TweenTrailRendererTime, TrailRenderer, float>(trailRenderer, to, duration);
            return tweener;
        }

        public static TweenTrailRendererTime Time(TrailRenderer trailRenderer, float from, float to, float duration)
        {
            var tweener = Play<TweenTrailRendererTime, TrailRenderer, float>(trailRenderer, from, to, duration);
            return tweener;
        }
    }

    public static partial class TrailRendererExtension
    {
        public static TweenTrailRendererTime TweenTime(this TrailRenderer trailRenderer, float to, float duration)
        {
            var tweener = UTween.Time(trailRenderer, to, duration);
            return tweener;
        }

        public static TweenTrailRendererTime TweenTime(this TrailRenderer trailRenderer, float from, float to, float duration)
        {
            var tweener = UTween.Time(trailRenderer, from, to, duration);
            return tweener;
        }
    }

    #endregion
}