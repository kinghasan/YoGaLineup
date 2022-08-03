#if UTWEEN_TEXTMESHPRO
using System;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("TMP Text Word Spacing", "TextMeshPro", UTweenTMP.IconPath)]
    [Serializable]
    public partial class TweenTMPTextWordSpacing : TweenValueFloat<TMP_Text>
    {
        public override float Value
        {
            get => Target.wordSpacing;
            set => Target.wordSpacing = value;
        }
    }

    #region Extension

    public static partial class UTweenTMP
    {
        public static TweenTMPTextWordSpacing WordSpacing(TMP_Text text, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextWordSpacing, TMP_Text, float>(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextWordSpacing WordSpacing(TMP_Text text, float from, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextWordSpacing, TMP_Text, float>(text, from, to, duration);
            return tweener;
        }
    }

    public static partial class TMP_TextExtension
    {
        public static TweenTMPTextWordSpacing TweenWordSpacing(this TMP_Text text, float to, float duration)
        {
            var tweener = UTweenTMP.WordSpacing(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextWordSpacing TweenWordSpacing(this TMP_Text text, float from, float to, float duration)
        {
            var tweener = UTweenTMP.WordSpacing(text, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
#endif