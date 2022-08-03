using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rect Transform Anchor", "RectTransform")]
    [Serializable]
    public partial class TweenAnchor : TweenValueVector4<RectTransform>
    {
        public override Vector4 Value
        {
            get
            {
                var min = Target.anchorMin;
                var max = Target.anchorMax;
                var anchor = new Vector4(min.x, min.y, max.x, max.y);
                return anchor;
            }
            set
            {
                Target.anchorMin = new Vector2(value.x, value.y);
                Target.anchorMax = new Vector2(value.z, value.w);
            }
        }
    }

#if UNITY_EDITOR

    public partial class TweenAnchor : TweenValueVector4<RectTransform>
    {
        public override string AxisXName => "x";
        public override string AxisYName => "y";
        public override string AxisZName => "X";
        public override string AxisWName => "Y";
    }

#endif

    #region Extension

    public static partial class UTween
    {
        public static TweenAnchor Anchor(RectTransform rectTransform, Vector4 to, float duration)
        {
            var tweener = Play<TweenAnchor, RectTransform, Vector4>(rectTransform, to, duration);
            return tweener;
        }

        public static TweenAnchor Anchor(RectTransform rectTransform, Vector4 from, Vector4 to, float duration)
        {
            var tweener = Play<TweenAnchor, RectTransform, Vector4>(rectTransform, from, to, duration);
            return tweener;
        }
    }

    public static partial class RectTransformExtension
    {
        public static TweenAnchor TweenAnchor(this RectTransform rectTransform, Vector4 to, float duration)
        {
            var tweener = UTween.Anchor(rectTransform, to, duration);
            return tweener;
        }

        public static TweenAnchor TweenAnchor(this RectTransform rectTransform, Vector4 from, Vector4 to, float duration)
        {
            var tweener = UTween.Anchor(rectTransform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
