using System;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("TMP Text Format Number", "TextMeshPro", UTweenTMP.IconPath)]
    [Serializable]
    public partial class TweenTMPTextFormatNumber : TweenValueFloat<TMP_Text>
    {
        public string Format;
        public override bool SupportSetCurrentValue => false;

        public override float Value
        {
            get => _value;
            set
            {
                _value = value;
                var str = string.Format(Format, _value);
                Target.text = str;
            }
        }

        private float _value;

        public override void Reset()
        {
            base.Reset();
            Format = "{0:F2}";
        }
    }

#if UNITY_EDITOR

    public partial class TweenTMPTextFormatNumber : TweenValueFloat<TMP_Text>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty FormatProperty;

        public override void DrawFromToValue()
        {
            base.DrawFromToValue();
            EditorGUILayout.PropertyField(FormatProperty);
        }
    }

#endif

    #region Extension

    public partial class TweenTMPTextFormatNumber : TweenValueFloat<TMP_Text>
    {
        public TweenTMPTextFormatNumber SetFormat(string format)
        {
            Format = format;
            return this;
        }
    }

    public static partial class UTweenTMP
    {
        public static TweenTMPTextFormatNumber FormatNumber(TMP_Text text, string formatText, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextFormatNumber, TMP_Text, float>(text, to, duration)
                .SetFormat(formatText);
            return tweener;
        }

        public static TweenTMPTextFormatNumber FormatNumber(TMP_Text text, string formatText, float from, float to, float duration)
        {
            var tweener = UTween.Play<TweenTMPTextFormatNumber, TMP_Text, float>(text, from, to, duration)
                .SetFormat(formatText);
            return tweener;
        }
    }

    public static partial class TMP_TextExtension
    {
        public static TweenTMPTextFormatNumber TweenFormatNumber(this TMP_Text text, string formatText, float to, float duration)
        {
            var tweener = UTweenTMP.FormatNumber(text, formatText, to, duration);
            return tweener;
        }

        public static TweenTMPTextFormatNumber TweenFormatNumber(this TMP_Text text, string formatText, float from, float to, float duration)
        {
            var tweener = UTweenTMP.FormatNumber(text, formatText, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
