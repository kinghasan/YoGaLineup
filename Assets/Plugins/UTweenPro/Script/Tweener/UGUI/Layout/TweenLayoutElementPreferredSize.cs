using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Layout Element Preferred Size", "UGUI Layout")]
    [Serializable]
    public class TweenLayoutElementPreferredSize : TweenValueVector2<LayoutElement>
    {
        public override Vector2 Value
        {
            get => new Vector2(Target.preferredWidth, Target.preferredHeight);
            set
            {
                Target.preferredWidth = value.x;
                Target.preferredHeight = value.y;
            }
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenLayoutElementPreferredSize PreferredSize(LayoutElement layoutElement, Vector2 to, float duration)
        {
            var tweener = Play<TweenLayoutElementPreferredSize, LayoutElement, Vector2>(layoutElement, to, duration);
            return tweener;
        }

        public static TweenLayoutElementPreferredSize PreferredSize(LayoutElement layoutElement, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenLayoutElementPreferredSize, LayoutElement, Vector2>(layoutElement, from, to, duration);
            return tweener;
        }
    }

    public static partial class LayoutElementExtension
    {
        public static TweenLayoutElementPreferredSize TweenPreferredSize(this LayoutElement layoutElement, Vector2 to, float duration)
        {
            var tweener = UTween.PreferredSize(layoutElement, to, duration);
            return tweener;
        }

        public static TweenLayoutElementPreferredSize TweenPreferredSize(this LayoutElement layoutElement, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.PreferredSize(layoutElement, from, to, duration);
            return tweener;
        }
    }

    #endregion
}