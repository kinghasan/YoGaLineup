using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rect Transform Width", "RectTransform")]
    [Serializable]
    public class TweenWidth : TweenValueFloat<RectTransform>
    {
        public override float Value
        {
            get => Target.sizeDelta.x;
            set => Target.sizeDelta = new Vector2(value, Target.sizeDelta.y);
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenWidth Width(RectTransform rectTransform, float to, float duration)
        {
            var tweener = Play<TweenWidth, RectTransform, float>(rectTransform, to, duration);
            return tweener;
        }

        public static TweenWidth Width(RectTransform rectTransform, float from, float to, float duration)
        {
            var tweener = Play<TweenWidth, RectTransform, float>(rectTransform, from, to, duration);
            return tweener;
        }
    }

    public static partial class RectTransformExtension
    {
        public static TweenWidth TweenWidth(this RectTransform rectTransform, float to, float duration)
        {
            var tweener = UTween.Width(rectTransform, to, duration);
            return tweener;
        }

        public static TweenWidth TweenWidth(this RectTransform rectTransform, float from, float to, float duration)
        {
            var tweener = UTween.Width(rectTransform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
