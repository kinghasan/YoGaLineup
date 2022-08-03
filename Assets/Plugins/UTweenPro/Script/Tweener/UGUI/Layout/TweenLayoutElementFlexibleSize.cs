using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Layout Element Flexible Size", "UGUI Layout")]
    [Serializable]
    public class TweenLayoutElementFlexibleSize : TweenValueVector2<LayoutElement>
    {
        public override Vector2 Value
        {
            get => new Vector2(Target.flexibleWidth, Target.flexibleHeight);
            set
            {
                Target.flexibleWidth = value.x;
                Target.flexibleHeight = value.y;
            }
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenLayoutElementFlexibleSize FlexibleSize(LayoutElement layoutElement, Vector2 to, float duration)
        {
            var tweener = Play<TweenLayoutElementFlexibleSize, LayoutElement, Vector2>(layoutElement, to, duration);
            return tweener;
        }

        public static TweenLayoutElementFlexibleSize FlexibleSize(LayoutElement layoutElement, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenLayoutElementFlexibleSize, LayoutElement, Vector2>(layoutElement, from, to, duration);
            return tweener;
        }
    }

    public static partial class LayoutElementExtension
    {
        public static TweenLayoutElementFlexibleSize TweenFlexibleSize(this LayoutElement layoutElement, Vector2 to, float duration)
        {
            var tweener = UTween.FlexibleSize(layoutElement, to, duration);
            return tweener;
        }

        public static TweenLayoutElementFlexibleSize TweenFlexibleSize(this LayoutElement layoutElement, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.FlexibleSize(layoutElement, from, to, duration);
            return tweener;
        }
    }

    #endregion
}