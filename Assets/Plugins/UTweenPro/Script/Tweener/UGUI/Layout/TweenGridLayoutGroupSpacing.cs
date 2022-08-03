using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Grid Layout Group Spacing", "UGUI Layout")]
    [Serializable]
    public class TweenGridLayoutGroupSpacing : TweenValueVector2<GridLayoutGroup>
    {
        public override Vector2 Value
        {
            get => Target.spacing;
            set => Target.spacing = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenGridLayoutGroupSpacing Spacing(GridLayoutGroup gridLayoutGroup, Vector2 to, float duration)
        {
            var tweener = Play<TweenGridLayoutGroupSpacing, GridLayoutGroup, Vector2>(gridLayoutGroup, to, duration);
            return tweener;
        }

        public static TweenGridLayoutGroupSpacing Spacing(GridLayoutGroup gridLayoutGroup, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenGridLayoutGroupSpacing, GridLayoutGroup, Vector2>(gridLayoutGroup, from, to, duration);
            return tweener;
        }
    }

    public static partial class GridLayoutGroupExtension
    {
        public static TweenGridLayoutGroupSpacing TweenSpacing(this GridLayoutGroup gridLayoutGroup, Vector2 to, float duration)
        {
            var tweener = UTween.Spacing(gridLayoutGroup, to, duration);
            return tweener;
        }

        public static TweenGridLayoutGroupSpacing TweenSpacing(this GridLayoutGroup gridLayoutGroup, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.Spacing(gridLayoutGroup, from, to, duration);
            return tweener;
        }
    }

    #endregion
}