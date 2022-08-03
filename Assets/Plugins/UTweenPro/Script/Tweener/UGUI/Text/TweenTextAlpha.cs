using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Text Alpha", "UGUI Text")]
    [Serializable]
    public class TweenTextAlpha : TweenValueFloat<Text>
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
        public static TweenTextAlpha Alpha(Text text, float to, float duration)
        {
            var tweener = Play<TweenTextAlpha, Text, float>(text, to, duration);
            return tweener;
        }

        public static TweenTextAlpha Alpha(Text text, float from, float to, float duration)
        {
            var tweener = Play<TweenTextAlpha, Text, float>(text, from, to, duration);
            return tweener;
        }
    }

    public static partial class TextExtension
    {
        public static TweenTextAlpha TweenAlpha(this Text text, float to, float duration)
        {
            var tweener = UTween.Alpha(text, to, duration);
            return tweener;
        }

        public static TweenTextAlpha TweenAlpha(this Text text, float from, float to, float duration)
        {
            var tweener = UTween.Alpha(text, from, to, duration);
            return tweener;
        }
    }

    #endregion
}