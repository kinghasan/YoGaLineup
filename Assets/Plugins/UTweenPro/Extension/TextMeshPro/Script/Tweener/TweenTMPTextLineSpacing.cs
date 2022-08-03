#if UTWEEN_TEXTMESHPRO
using System;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("TMP Text Line Spacing", "TextMeshPro", UTweenTMP.IconPath)]
    [Serializable]
    public partial class TweenTMPTextLineSpacing : TweenValueFloat<TMP_Text>
    {
        public override float Value
        {
            get => Target.lineSpacing;
            set => Target.lineSpacing = value;
        }
    }

    #region Extension

    public static partial class UTweenTMP
    {
        public static TweenTMPTextLineSpacing LineSpacing(TMP_Text text, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextLineSpacing, TMP_Text, float>(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextLineSpacing LineSpacing(TMP_Text text, float from, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextLineSpacing, TMP_Text, float>(text, from, to, duration);
            return tweener;
        }
    }

    public static partial class TMP_TextExtension
    {
        public static TweenTMPTextLineSpacing TweenLineSpacing(this TMP_Text text, float to, float duration)
        {
            var tweener = UTweenTMP.LineSpacing(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextLineSpacing TweenLineSpacing(this TMP_Text text, float from, float to, float duration)
        {
            var tweener = UTweenTMP.LineSpacing(text, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
#endif