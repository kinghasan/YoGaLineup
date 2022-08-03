#if UTWEEN_TEXTMESHPRO
using System;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("TMP Text Paragraph Spacing", "TextMeshPro", UTweenTMP.IconPath)]
    [Serializable]
    public partial class TweenTMPTextParagraphSpacing : TweenValueFloat<TMP_Text>
    {
        public override float Value
        {
            get => Target.paragraphSpacing;
            set => Target.paragraphSpacing = value;
        }
    }

    #region Extension

    public static partial class UTweenTMP
    {
        public static TweenTMPTextParagraphSpacing ParagraphSpacing(TMP_Text text, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextParagraphSpacing, TMP_Text, float>(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextParagraphSpacing ParagraphSpacing(TMP_Text text, float from, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextParagraphSpacing, TMP_Text, float>(text, from, to, duration);
            return tweener;
        }
    }

    public static partial class TMP_TextExtension
    {
        public static TweenTMPTextParagraphSpacing TweenParagraphSpacing(this TMP_Text text, float to, float duration)
        {
            var tweener = UTweenTMP.ParagraphSpacing(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextParagraphSpacing TweenParagraphSpacing(this TMP_Text text, float from, float to, float duration)
        {
            var tweener = UTweenTMP.ParagraphSpacing(text, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
#endif