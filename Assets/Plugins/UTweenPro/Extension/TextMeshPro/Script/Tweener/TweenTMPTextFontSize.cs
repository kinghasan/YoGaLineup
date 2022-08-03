#if UTWEEN_TEXTMESHPRO
using System;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("TMP Text Font Size", "TextMeshPro", UTweenTMP.IconPath)]
    [Serializable]
    public class TweenTMPTextFontSize : TweenValueFloat<TMP_Text>
    {
        public override float Value
        {
            get => Target.fontSize;
            set => Target.fontSize = value;
        }
    }

    #region Extension

    public static partial class UTweenTMP
    {
        public static TweenTMPTextFontSize FontSize(TMP_Text text, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextFontSize, TMP_Text, float>(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextFontSize FontSize(TMP_Text text, float from, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextFontSize, TMP_Text, float>(text, from, to, duration);
            return tweener;
        }
    }

    public static partial class TMP_TextExtension
    {
        public static TweenTMPTextFontSize TweenFontSize(this TMP_Text text, float to, float duration)
        {
            var tweener = UTweenTMP.FontSize(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextFontSize TweenFontSize(this TMP_Text text, float from, float to, float duration)
        {
            var tweener = UTweenTMP.FontSize(text, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
#endif