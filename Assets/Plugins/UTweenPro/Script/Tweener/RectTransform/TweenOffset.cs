using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rect Transform Offset", "RectTransform")]
    [Serializable]
    public partial class TweenOffset : TweenValueVector4<RectTransform>
    {
        public override Vector4 Value
        {
            get
            {
                var min = Target.offsetMin;
                var max = Target.offsetMax;
                var anchor = new Vector4(min.x, min.y, max.x, max.y);
                return anchor;
            }
            set
            {
                Target.offsetMin = new Vector2(value.x, value.y);
                Target.offsetMax = new Vector2(value.z, value.w);
            }
        }
    }

#if UNITY_EDITOR

    public partial class TweenOffset : TweenValueVector4<RectTransform>
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
        public static TweenOffset Offset(RectTransform rectTransform, Vector4 to, float duration)
        {
            var tweener = Play<TweenOffset, RectTransform, Vector4>(rectTransform, to, duration);
            return tweener;
        }

        public static TweenOffset Offset(RectTransform rectTransform, Vector4 from, Vector4 to, float duration)
        {
            var tweener = Play<TweenOffset, RectTransform, Vector4>(rectTransform, from, to, duration);
            return tweener;
        }
    }

    public static partial class RectTransformExtension
    {
        public static TweenOffset TweenOffset(this RectTransform rectTransform, Vector4 to, float duration)
        {
            var tweener = UTween.Offset(rectTransform, to, duration);
            return tweener;
        }

        public static TweenOffset TweenOffset(this RectTransform rectTransform, Vector4 from, Vector4 to, float duration)
        {
            var tweener = UTween.Offset(rectTransform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
