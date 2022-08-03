using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Grid Layout Group Cell Size", "UGUI Layout")]
    [Serializable]
    public class TweenGridLayoutGroupCellSize : TweenValueVector2<GridLayoutGroup>
    {
        public override Vector2 Value
        {
            get => Target.cellSize;
            set => Target.cellSize = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenGridLayoutGroupCellSize CellSize(GridLayoutGroup gridLayoutGroup, Vector2 to, float duration)
        {
            var tweener = Play<TweenGridLayoutGroupCellSize, GridLayoutGroup, Vector2>(gridLayoutGroup, to, duration);
            return tweener;
        }

        public static TweenGridLayoutGroupCellSize CellSize(GridLayoutGroup gridLayoutGroup, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenGridLayoutGroupCellSize, GridLayoutGroup, Vector2>(gridLayoutGroup, from, to, duration);
            return tweener;
        }
    }

    public static partial class GridLayoutGroupExtension
    {
        public static TweenGridLayoutGroupCellSize TweenCellSize(this GridLayoutGroup gridLayoutGroup, Vector2 to, float duration)
        {
            var tweener = UTween.CellSize(gridLayoutGroup, to, duration);
            return tweener;
        }

        public static TweenGridLayoutGroupCellSize CellSize(this GridLayoutGroup gridLayoutGroup, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.CellSize(gridLayoutGroup, from, to, duration);
            return tweener;
        }
    }

    #endregion
}