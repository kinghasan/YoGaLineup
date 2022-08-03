#if UTWEEN_TEXTMESHPRO
using System;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("TMP Text Color", "TextMeshPro", UTweenTMP.IconPath)]
    [Serializable]
    public class TweenTMPTextColor : TweenValueColor<TMP_Text>
    {
        public override Color Value
        {
            get => Target.color;
            set => Target.color = value;
        }
    }

    #region Extension

    public static partial class UTweenTMP
    {
        public static TweenTMPTextColor Color(TMP_Text text, Color to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextColor, TMP_Text, Color>(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextColor Color(TMP_Text text, Color from, Color to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextColor, TMP_Text, Color>(text, from, to, duration);
            return tweener;
        }

        public static TweenTMPTextColor Color(TMP_Text text, Gradient gradient, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextColor, TMP_Text>(text, duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient) as TweenTMPTextColor;
            return tweener;
        }
    }

    public static partial class TMP_TextExtension
    {
        public static TweenTMPTextColor TweenColor(this TMP_Text text, Color to, float duration)
        {
            var tweener = UTweenTMP.Color(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextColor TweenColor(this TMP_Text text, Color from, Color to, float duration)
        {
            var tweener = UTweenTMP.Color(text, from, to, duration);
            return tweener;
        }

        public static TweenTMPTextColor TweenColor(this TMP_Text text, Gradient gradient, float duration)
        {
            var tweener = UTweenTMP.Color(text, gradient, duration);
            return tweener;
        }
    }

    #endregion
}
#endif
