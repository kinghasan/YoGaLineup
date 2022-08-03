using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Line Renderer End Width", "Rendering")]
    [Serializable]
    public class TweenLineRendererEndWidth : TweenValueFloat<LineRenderer>
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
        public static TweenLineRendererEndWidth EndWidth(LineRenderer lineRenderer, float to, float duration)
        {
            var tweener = Play<TweenLineRendererEndWidth, LineRenderer, float>(lineRenderer, to, duration);
            return tweener;
        }

        public static TweenLineRendererEndWidth EndWidth(LineRenderer lineRenderer, float from, float to, float duration)
        {
            var tweener = Play<TweenLineRendererEndWidth, LineRenderer, float>(lineRenderer, from, to, duration);
            return tweener;
        }
    }

    public static partial class LineRendererExtension
    {
        public static TweenLineRendererEndWidth TweenEndWidth(this LineRenderer lineRenderer, float to, float duration)
        {
            var tweener = UTween.EndWidth(lineRenderer, to, duration);
            return tweener;
        }

        public static TweenLineRendererEndWidth TweenEndWidth(this LineRenderer lineRenderer, float from, float to, float duration)
        {
            var tweener = UTween.EndWidth(lineRenderer, from, to, duration);
            return tweener;
        }
    }

    #endregion
}