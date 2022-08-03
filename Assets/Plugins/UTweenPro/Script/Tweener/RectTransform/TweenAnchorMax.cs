using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rect Transform Anchor Max", "RectTransform")]
    [Serializable]
    public class TweenAnchorMax : TweenValueVector2<RectTransform>
    {
        public override Vector2 Value
        {
            get => Target.anchorMax;
            set => Target.anchorMax = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenAnchorMax AnchorMax(RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = Play<TweenAnchorMax, RectTransform, Vector2>(rectTransform, to, duration);
            return tweener;
        }

        public static TweenAnchorMax AnchorMax(RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenAnchorMax, RectTransform, Vector2>(rectTransform, from, to, duration);
            return tweener;
        }
    }

    public static partial class RectTransformExtension
    {
        public static TweenAnchorMax TweenAnchorMax(this RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = UTween.AnchorMax(rectTransform, to, duration);
            return tweener;
        }

        public static TweenAnchorMax TweenAnchorMax(this RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.AnchorMax(rectTransform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
