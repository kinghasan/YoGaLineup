using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Horizontal Layout Group Spacing", "UGUI Layout")]
    [Serializable]
    public class TweenHorizontalLayoutGroupSpacing : TweenValueFloat<HorizontalLayoutGroup>
    {
        public override float Value
        {
            get => Target.spacing;
            set => Target.spacing = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenHorizontalLayoutGroupSpacing Spacing(HorizontalLayoutGroup horizontalLayoutGroup, float to, float duration)
        {
            var tweener = Play<TweenHorizontalLayoutGroupSpacing, HorizontalLayoutGroup, float>(horizontalLayoutGroup, to, duration);
            return tweener;
        }

        public static TweenHorizontalLayoutGroupSpacing Spacing(HorizontalLayoutGroup horizontalLayoutGroup, float from, float to, float duration)
        {
            var tweener = Play<TweenHorizontalLayoutGroupSpacing, HorizontalLayoutGroup, float>(horizontalLayoutGroup, from, to, duration);
            return tweener;
        }
    }

    public static partial class HorizontalLayoutGroupExtension
    {
        public static TweenHorizontalLayoutGroupSpacing TweenSpacing(this HorizontalLayoutGroup horizontalLayoutGroup, float to, float duration)
        {
            var tweener = UTween.Spacing(horizontalLayoutGroup, to, duration);
            return tweener;
        }

        public static TweenHorizontalLayoutGroupSpacing TweenSpacing(this HorizontalLayoutGroup horizontalLayoutGroup, float from, float to, float duration)
        {
            var tweener = UTween.Spacing(horizontalLayoutGroup, from, to, duration);
            return tweener;
        }
    }

    #endregion
}