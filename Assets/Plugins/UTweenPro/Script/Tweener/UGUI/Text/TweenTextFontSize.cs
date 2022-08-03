using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Text Font Size", "UGUI Text")]
    [Serializable]
    public class TweenTextFontSize : TweenValueInteger<Text>
    {
        public override int Value
        {
            get => Target.fontSize;
            set => Target.fontSize = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenTextFontSize FontSize(Text text, int to, float duration)
        {
            var tweener = Play<TweenTextFontSize, Text, int>(text, to, duration);
            return tweener;
        }

        public static TweenTextFontSize FontSize(Text text, int from, int to, float duration)
        {
            var tweener = Play<TweenTextFontSize, Text, int>(text, from, to, duration);
            return tweener;
        }
    }

    public static partial class TextExtension
    {
        public static TweenTextFontSize TweenFontSize(this Text text, int to, float duration)
        {
            var tweener = UTween.FontSize(text, to, duration);
            return tweener;
        }

        public static TweenTextFontSize TweenFontSize(this Text text, int from, int to, float duration)
        {
            var tweener = UTween.FontSize(text, from, to, duration);
            return tweener;
        }
    }

    #endregion
}