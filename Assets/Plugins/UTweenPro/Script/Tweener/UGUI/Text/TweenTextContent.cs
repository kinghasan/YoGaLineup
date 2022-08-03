using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Text Content", "UGUI Text")]
    [Serializable]
    public partial class TweenTextContent : TweenValueString<Text>
    {
        public override bool ShowRichText => false;

        public override void PreSample()
        {
            base.PreSample();
            RichText = Target.supportRichText;
        }

        public override string Value
        {
            get => Target.text;
            set => Target.text = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenTextContent TextContent(Text text, string to, float duration, bool richText = true)
        {
            var tweener = Play<TweenTextContent, Text, string>(text, to, duration)
                .SetRichText(richText) as TweenTextContent;
            return tweener;
        }

        public static TweenTextContent TextContent(Text text, string from, string to, float duration, bool richText = true)
        {
            var tweener = Play<TweenTextContent, Text, string>(text, from, to, duration)
                .SetRichText(richText) as TweenTextContent;
            return tweener;
        }
    }

    public static partial class TextExtension
    {
        public static TweenTextContent TweenTextContent(this Text text, string to, float duration, bool richText = true)
        {
            var tweener = UTween.TextContent(text, to, duration, richText);
            return tweener;
        }

        public static TweenTextContent TweenTextContent(this Text text, string from, string to, float duration, bool richText = true)
        {
            var tweener = UTween.TextContent(text, from, to, duration, richText);
            return tweener;
        }
    }

    #endregion
}