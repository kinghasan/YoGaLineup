#if UTWEEN_TEXTMESHPRO
using System;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("TMP Text Alpha", "TextMeshPro", UTweenTMP.IconPath)]
    [Serializable]
    public class TweenTMPTextAlpha : TweenValueFloat<TMP_Text>
    {
        public override float Value
        {
            get => Target.color.a;
            set
            {
                var color = Target.color;
                color.a = value;
                Target.color = color;
            }
        }
    }

    #region Extension

    public static partial class UTweenTMP
    {
        public static TweenTMPTextAlpha Alpha(TMP_Text text, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextAlpha, TMP_Text, float>(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextAlpha Alpha(TMP_Text text, float from, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextAlpha, TMP_Text, float>(text, from, to, duration);
            return tweener;
        }
    }

    public static partial class TMP_TextExtension
    {
        public static TweenTMPTextAlpha TweenAlpha(this TMP_Text text, float to, float duration)
        {
            var tweener = UTweenTMP.Alpha(text, to, duration);
            return tweener;
        }

        public static TweenTMPTextAlpha TweenAlpha(this TMP_Text text, float from, float to, float duration)
        {
            var tweener = UTweenTMP.Alpha(text, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
#endif
