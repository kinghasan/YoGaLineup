using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Outline Alpha", "UGUI Effect")]
    [Serializable]
    public class TweenOutlineAlpha : TweenValueFloat<Outline>
    {
        public override float Value
        {
            get => Target.effectColor.a;
            set
            {
                var color = Target.effectColor;
                color.a = value;
                Target.effectColor = color;
            }
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenOutlineAlpha Alpha(Outline outline, float to, float duration)
        {
            var tweener = Play<TweenOutlineAlpha, Outline, float>(outline, to, duration);
            return tweener;
        }

        public static TweenOutlineAlpha Alpha(Outline outline, float from, float to, float duration)
        {
            var tweener = Play<TweenOutlineAlpha, Outline, float>(outline, from, to, duration);
            return tweener;
        }
    }

    public static partial class OutlineExtension
    {
        public static TweenOutlineAlpha TweenAlpha(this Outline outline, float to, float duration)
        {
            var tweener = UTween.Alpha(outline, to, duration);
            return tweener;
        }

        public static TweenOutlineAlpha TweenAlpha(this Outline outline, float from, float to, float duration)
        {
            var tweener = UTween.Alpha(outline, from, to, duration);
            return tweener;
        }
    }

    #endregion
}