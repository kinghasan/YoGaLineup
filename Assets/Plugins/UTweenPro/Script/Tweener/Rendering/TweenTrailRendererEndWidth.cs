using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Trail Renderer End Width", "Rendering")]
    [Serializable]
    public class TweenTrailRendererEndWidth : TweenValueFloat<TrailRenderer>
    {
        public override float Value
        {
            get => Target.endWidth;
            set => Target.endWidth = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenTrailRendererEndWidth EndWidth(TrailRenderer trailRenderer, float to, float duration)
        {
            var tweener = Play<TweenTrailRendererEndWidth, TrailRenderer, float>(trailRenderer, to, duration);
            return tweener;
        }

        public static TweenTrailRendererEndWidth EndWidth(TrailRenderer trailRenderer, float from, float to, float duration)
        {
            var tweener = Play<TweenTrailRendererEndWidth, TrailRenderer, float>(trailRenderer, from, to, duration);
            return tweener;
        }
    }

    public static partial class TrailRendererExtension
    {
        public static TweenTrailRendererEndWidth TweenEndWidth(this TrailRenderer trailRenderer, float to, float duration)
        {
            var tweener = UTween.EndWidth(trailRenderer, to, duration);
            return tweener;
        }

        public static TweenTrailRendererEndWidth TweenEndWidth(this TrailRenderer trailRenderer, float from, float to, float duration)
        {
            var tweener = UTween.EndWidth(trailRenderer, from, to, duration);
            return tweener;
        }
    }

    #endregion
}