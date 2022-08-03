#if UTWEEN_TEXTMESHPRO
using System;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("TMP Text Character Spacing", "TextMeshPro", UTweenTMP.IconPath)]
    [Serializable]
    public partial class TweenTMPTextCharacterSpacing : TweenValueFloat<TMP_Text>
    {
        public override float Value
        {
            get => Target.characterSpacing;
            set => Target.characterSpacing = value;
        }
    }

    #region Extension

    public static partial class UTweenTMP
    {
        public static TweenTMPTextCharacterSpacing CharacterSpacing(TMP_Text text, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextCharacterSpacing, TMP_Text, float>(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextCharacterSpacing CharacterSpacing(TMP_Text text, float from, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextCharacterSpacing, TMP_Text, float>(text, from, to, duration);
            return tweener;
        }
    }

    public static partial class TMP_TextExtension
    {
        public static TweenTMPTextCharacterSpacing TweenCharacterSpacing(this TMP_Text text, float to, float duration)
        {
            var tweener = UTweenTMP.CharacterSpacing(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextCharacterSpacing TweenCharacterSpacing(this TMP_Text text, float from, float to, float duration)
        {
            var tweener = UTweenTMP.CharacterSpacing(text, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
#endif