using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Trail Renderer Start Width", "Rendering")]
    [Serializable]
    public class TweenTrailRendererStartWidth : TweenValueFloat<TrailRenderer>
    {
        public override float Value
        {
            get => Target.startWidth;
            set => Target.startWidth = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenTrailRendererStartWidth StartWidth(TrailRenderer trailRenderer, float to, float duration)
        {
            var tweener = Play<TweenTrailRendererStartWidth, TrailRenderer, float>(trailRenderer, to, duration);
            return tweener;
        }

        public static TweenTrailRendererStartWidth StartWidth(TrailRenderer trailRenderer, float from, float to, float duration)
        {
            var tweener = Play<TweenTrailRendererStartWidth, TrailRenderer, float>(trailRenderer, from, to, duration);
            return tweener;
        }
    }

    public static partial class TrailRendererExtension
    {
        public static TweenTrailRendererStartWidth TweenStartWidth(this TrailRenderer trailRenderer, float to, float duration)
        {
            var tweener = UTween.StartWidth(trailRenderer, to, duration);
            return tweener;
        }

        public static TweenTrailRendererStartWidth TweenStartWidth(this TrailRenderer trailRenderer, float from, float to, float duration)
        {
            var tweener = UTween.StartWidth(trailRenderer, from, to, duration);
            return tweener;
        }
    }

    #endregion
}