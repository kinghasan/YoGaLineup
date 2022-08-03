using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rect Transform Anchored Position 3D", "RectTransform")]
    [Serializable]
    public class TweenAnchoredPosition3D : TweenValueVector3<RectTransform>
    {
        public override Vector3 Value
        {
            get => Target.anchoredPosition3D;
            set => Target.anchoredPosition3D = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenAnchoredPosition3D AnchoredPosition3D(RectTransform rectTransform, Vector3 to, float duration)
        {
            var tweener = Play<TweenAnchoredPosition3D, RectTransform, Vector3>(rectTransform, to, duration);
            return tweener;
        }

        public static TweenAnchoredPosition3D AnchoredPosition3D(RectTransform rectTransform, Vector3 from, Vector3 to, float duration)
        {
            var tweener = Play<TweenAnchoredPosition3D, RectTransform, Vector3>(rectTransform, from, to, duration);
            return tweener;
        }
    }

    public static partial class RectTransformExtension
    {
        public static TweenAnchoredPosition3D TweenAnchoredPosition3D(this RectTransform rectTransform, Vector3 to, float duration)
        {
            var tweener = UTween.AnchoredPosition3D(rectTransform, to, duration);
            return tweener;
        }

        public static TweenAnchoredPosition3D TweenAnchoredPosition3D(this RectTransform rectTransform, Vector3 from, Vector3 to, float duration)
        {
            var tweener = UTween.AnchoredPosition3D(rectTransform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}