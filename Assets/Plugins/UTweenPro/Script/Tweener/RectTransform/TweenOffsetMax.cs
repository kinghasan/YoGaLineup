using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rect Transform Offset Max", "RectTransform")]
    [Serializable]
    public class TweenOffsetMax : TweenValueVector2<RectTransform>
    {
        public override Vector2 Value
        {
            get => Target.offsetMax;
            set => Target.offsetMax = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenOffsetMax OffsetMax(RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = Play<TweenOffsetMax, RectTransform, Vector2>(rectTransform, to, duration);
            return tweener;
        }

        public static TweenOffsetMax OffsetMax(RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenOffsetMax, RectTransform, Vector2>(rectTransform, from, to, duration);
            return tweener;
        }
    }

    public static partial class RectTransformExtension
    {
        public static TweenOffsetMax TweenOffsetMax(this RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = UTween.OffsetMax(rectTransform, to, duration);
            return tweener;
        }

        public static TweenOffsetMax TweenOffsetMax(this RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.OffsetMax(rectTransform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
