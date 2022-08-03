using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Layout Element Min Size", "UGUI Layout")]
    [Serializable]
    public class TweenLayoutElementMinSize : TweenValueVector2<LayoutElement>
    {
        public override Vector2 Value
        {
            get => new Vector2(Target.minWidth, Target.minHeight);
            set
            {
                Target.minWidth = value.x;
                Target.minHeight = value.y;
            }
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenLayoutElementMinSize MinSize(LayoutElement layoutElement, Vector2 to, float duration)
        {
            var tweener = Play<TweenLayoutElementMinSize, LayoutElement, Vector2>(layoutElement, to, duration);
            return tweener;
        }

        public static TweenLayoutElementMinSize MinSize(LayoutElement layoutElement, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenLayoutElementMinSize, LayoutElement, Vector2>(layoutElement, from, to, duration);
            return tweener;
        }
    }

    public static partial class LayoutElementExtension
    {
        public static TweenLayoutElementMinSize TweenMinSize(this LayoutElement layoutElement, Vector2 to, float duration)
        {
            var tweener = UTween.MinSize(layoutElement, to, duration);
            return tweener;
        }

        public static TweenLayoutElementMinSize TweenMinSize(this LayoutElement layoutElement, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.MinSize(layoutElement, from, to, duration);
            return tweener;
        }
    }

    #endregion
}