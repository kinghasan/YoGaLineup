using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Graphic Alpha", "UGUI", "GraphicRaycaster Icon")]
    [Serializable]
    public class TweenGraphicAlpha : TweenValueFloat<Graphic>
    {
        public override float Value
        {
            get => Target.color.a;
            set
            {
                var color = Target.color;
                color.a = value;
                Target.color = color;
            }
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenGraphicAlpha Alpha(Graphic graphic, float to, float duration)
        {
            var tweener = Play<TweenGraphicAlpha, Graphic, float>(graphic, to, duration);
            return tweener;
        }

        public static TweenGraphicAlpha Alpha(Graphic graphic, float from, float to, float duration)
        {
            var tweener = Play<TweenGraphicAlpha, Graphic, float>(graphic, from, to, duration);
            return tweener;
        }
    }

    public static partial class GraphicExtension
    {
        public static TweenGraphicAlpha TweenAlpha(this Graphic graphic, float to, float duration)
        {
            var tweener = UTween.Alpha(graphic, to, duration);
            return tweener;
        }

        public static TweenGraphicAlpha TweenAlpha(this Graphic graphic, float from, float to, float duration)
        {
            var tweener = UTween.Alpha(graphic, from, to, duration);
            return tweener;
        }
    }

    #endregion
}