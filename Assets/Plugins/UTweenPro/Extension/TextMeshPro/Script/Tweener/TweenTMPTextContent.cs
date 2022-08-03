#if UTWEEN_TEXTMESHPRO
using System;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("TMP Text Content", "TextMeshPro", UTweenTMP.IconPath)]
    [Serializable]
    public partial class TweenTMPTextContent : TweenValueString<TMP_Text>
    {
        public override bool ShowRichText => false;

        public override void PreSample()
        {
            base.PreSample();
            RichText = Target.richText;
        }

        public override string Value
        {
            get => Target.text;
            set => Target.text = value;
        }
    }

    #region Extension

    public static partial class UTweenTMP
    {
        public static TweenTMPTextContent TextContent(TMP_Text text, string to, float duration, bool richText = true)
        {
            var tweener = UTween.Play<TweenTMPTextContent, TMP_Text, string>(text, to, duration)
                .SetRichText(richText) as TweenTMPTextContent;
            return tweener;
        }

        public static TweenTMPTextContent TextContent(TMP_Text text, string from, string to, float duration, bool richText = true)
        {
            var tweener = UTween.Play<TweenTMPTextContent, TMP_Text, string>(text, from, to, duration)
                .SetRichText(richText) as TweenTMPTextContent;
            return tweener;
        }
    }

    public static partial class TMP_TextExtension
    {
        public static TweenTMPTextContent TweenTextContent(this TMP_Text text, string to, float duration, bool richText = true)
        {
            var tweener = UTweenTMP.TextContent(text, to, duration, richText);
            return tweener;
        }

        public static TweenTMPTextContent TweenTextContent(this TMP_Text text, string from, string to, float duration, bool richText = true)
        {
            var tweener = UTweenTMP.TextContent(text, from, to, duration, richText);
            return tweener;
        }
    }

    #endregion
}
#endif