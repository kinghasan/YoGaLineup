using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rect Transform Anchor Min", "RectTransform")]
    [Serializable]
    public class TweenAnchorMin : TweenValueVector2<RectTransform>
    {
        public override Vector2 Value
        {
            get => Target.anchorMin;
            set => Target.anchorMin = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenAnchorMin AnchorMin(RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = Play<TweenAnchorMin, RectTransform, Vector2>(rectTransform, to, duration);
            return tweener;
        }

        public static TweenAnchorMin AnchorMin(RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenAnchorMin, RectTransform, Vector2>(rectTransform, from, to, duration);
            return tweener;
        }
    }

    public static partial class RectTransformExtension
    {
        public static TweenAnchorMin TweenAnchorMin(this RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = UTween.AnchorMin(rectTransform, to, duration);
            return tweener;
        }

        public static TweenAnchorMin TweenAnchorMin(this RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.AnchorMin(rectTransform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
