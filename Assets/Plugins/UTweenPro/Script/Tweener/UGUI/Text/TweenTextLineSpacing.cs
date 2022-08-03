using System;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Text Line Spacing", "UGUI Text")]
    [Serializable]
    public class TweenTextLineSpacing : TweenValueFloat<Text>
    {
        public override float Value
        {
            get => Target.lineSpacing;
            set => Target.lineSpacing = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenTextLineSpacing LineSpacing(Text text, float to, float duration)
        {
            var tweener = Play<TweenTextLineSpacing, Text, float>(text, to, duration);
            return tweener;
        }

        public static TweenTextLineSpacing LineSpacing(Text text, float from, float to, float duration)
        {
            var tweener = Play<TweenTextLineSpacing, Text, float>(text, from, to, duration);
            return tweener;
        }
    }

    public static partial class TextExtension
    {
        public static TweenTextLineSpacing TweenLineSpacing(this Text text, float to, float duration)
        {
            var tweener = UTween.LineSpacing(text, to, duration);
            return tweener;
        }

        public static TweenTextLineSpacing TweenLineSpacing(this Text text, float from, float to, float duration)
        {
            var tweener = UTween.LineSpacing(text, from, to, duration);
            return tweener;
        }
    }

    #endregion
}