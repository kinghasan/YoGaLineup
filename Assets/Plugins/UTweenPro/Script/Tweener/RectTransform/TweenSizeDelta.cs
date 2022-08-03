using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rect Transform Size Delta", "RectTransform")]
    [Serializable]
    public class TweenSizeDelta : TweenValueVector2<RectTransform>
    {
        public override Vector2 Value
        {
            get => Target.sizeDelta;
            set => Target.sizeDelta = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenSizeDelta SizeDelta(RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = Play<TweenSizeDelta, RectTransform, Vector2>(rectTransform, to, duration);
            return tweener;
        }

        public static TweenSizeDelta SizeDelta(RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenSizeDelta, RectTransform, Vector2>(rectTransform, from, to, duration);
            return tweener;
        }
    }

    public static partial class RectTransformExtension
    {
        public static TweenSizeDelta TweenSizeDelta(this RectTransform rectTransform, Vector2 to, float duration)
        {
            var tweener = UTween.SizeDelta(rectTransform, to, duration);
            return tweener;
        }

        public static TweenSizeDelta TweenSizeDelta(this RectTransform rectTransform, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.SizeDelta(rectTransform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
