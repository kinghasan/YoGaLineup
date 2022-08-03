using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Line Renderer Start Width", "Rendering")]
    [Serializable]
    public class TweenLineRendererStartWidth : TweenValueFloat<LineRenderer>
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
        public static TweenLineRendererStartWidth StartWidth(LineRenderer lineRenderer, float to, float duration)
        {
            var tweener = Play<TweenLineRendererStartWidth, LineRenderer, float>(lineRenderer, to, duration);
            return tweener;
        }

        public static TweenLineRendererStartWidth StartWidth(LineRenderer lineRenderer, float from, float to, float duration)
        {
            var tweener = Play<TweenLineRendererStartWidth, LineRenderer, float>(lineRenderer, from, to, duration);
            return tweener;
        }
    }

    public static partial class LineRendererExtension
    {
        public static TweenLineRendererStartWidth TweenStartWidth(this LineRenderer lineRenderer, float to, float duration)
        {
            var tweener = UTween.StartWidth(lineRenderer, to, duration);
            return tweener;
        }

        public static TweenLineRendererStartWidth TweenStartWidth(this LineRenderer lineRenderer, float from, float to, float duration)
        {
            var tweener = UTween.StartWidth(lineRenderer, from, to, duration);
            return tweener;
        }
    }

    #endregion
}