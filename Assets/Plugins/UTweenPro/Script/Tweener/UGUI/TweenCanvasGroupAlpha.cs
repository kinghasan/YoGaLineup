using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Canvas Group Alpha", "UGUI")]
    [Serializable]
    public class TweenCanvasGroupAlpha : TweenValueFloat<CanvasGroup>
    {
        public override float Value
        {
            get => Target.alpha;
            set => Target.alpha = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenCanvasGroupAlpha Alpha(CanvasGroup canvasGroup, float to, float duration)
        {
            var tweener = Play<TweenCanvasGroupAlpha, CanvasGroup, float>(canvasGroup, to, duration);
            return tweener;
        }

        public static TweenCanvasGroupAlpha Alpha(CanvasGroup canvasGroup, float from, float to, float duration)
        {
            var tweener = Play<TweenCanvasGroupAlpha, CanvasGroup, float>(canvasGroup, from, to, duration);
            return tweener;
        }
    }

    public static partial class CanvasGroupExtension
    {
        public static TweenCanvasGroupAlpha TweenAlpha(this CanvasGroup canvasGroup, float to, float duration)
        {
            var tweener = UTween.Alpha(canvasGroup, to, duration);
            return tweener;
        }

        public static TweenCanvasGroupAlpha TweenAlpha(this CanvasGroup canvasGroup, float from, float to, float duration)
        {
            var tweener = UTween.Alpha(canvasGroup, from, to, duration);
            return tweener;
        }
    }

    #endregion
}