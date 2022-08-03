using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rect Transform Height", "RectTransform")]
    [Serializable]
    public class TweenHeight : TweenValueFloat<RectTransform>
    {
        public override float Value
        {
            get => Target.sizeDelta.y;
            set => Target.sizeDelta = new Vector2(Target.sizeDelta.x, value);
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenHeight Height(RectTransform rectTransform, float to, float duration)
        {
            var tweener = Play<TweenHeight, RectTransform, float>(rectTransform, to, duration);
            return tweener;
        }

        public static TweenHeight Height(RectTransform rectTransform, float from, float to, float duration)
        {
            var tweener = Play<TweenHeight, RectTransform, float>(rectTransform, from, to, duration);
            return tweener;
        }
    }

    public static partial class RectTransformExtension
    {
        public static TweenHeight TweenHeight(this RectTransform rectTransform, float to, float duration)
        {
            var tweener = UTween.Height(rectTransform, to, duration);
            return tweener;
        }

        public static TweenHeight TweenHeight(this RectTransform rectTransform, float from, float to, float duration)
        {
            var tweener = UTween.Height(rectTransform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
