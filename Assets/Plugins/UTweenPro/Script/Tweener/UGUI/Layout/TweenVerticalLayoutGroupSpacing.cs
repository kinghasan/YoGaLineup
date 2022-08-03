using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Vertical Layout Group Spacing", "UGUI Layout")]
    [Serializable]
    public class TweenVerticalLayoutGroupSpacing : TweenValueFloat<VerticalLayoutGroup>
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
        public static TweenVerticalLayoutGroupSpacing Spacing(VerticalLayoutGroup verticalLayoutGroup, float to, float duration)
        {
            var tweener = Play<TweenVerticalLayoutGroupSpacing, VerticalLayoutGroup, float>(verticalLayoutGroup, to, duration);
            return tweener;
        }

        public static TweenVerticalLayoutGroupSpacing Spacing(VerticalLayoutGroup verticalLayoutGroup, float from, float to, float duration)
        {
            var tweener = Play<TweenVerticalLayoutGroupSpacing, VerticalLayoutGroup, float>(verticalLayoutGroup, from, to, duration);
            return tweener;
        }
    }

    public static partial class VerticalLayoutGroupExtension
    {
        public static TweenVerticalLayoutGroupSpacing TweenSpacing(this VerticalLayoutGroup verticalLayoutGroup, float to, float duration)
        {
            var tweener = UTween.Spacing(verticalLayoutGroup, to, duration);
            return tweener;
        }

        public static TweenVerticalLayoutGroupSpacing TweenSpacing(this VerticalLayoutGroup verticalLayoutGroup, float from, float to, float duration)
        {
            var tweener = UTween.Spacing(verticalLayoutGroup, from, to, duration);
            return tweener;
        }
    }

    #endregion
}