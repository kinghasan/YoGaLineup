using System;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Text Format Number", "UGUI Text")]
    [Serializable]
    public partial class TweenTextFormatNumber : TweenValueFloat<Text>
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

    public partial class TweenTextFormatNumber : TweenValueFloat<Text>
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

    public partial class TweenTextFormatNumber : TweenValueFloat<Text>
    {
        public TweenTextFormatNumber SetFormat(string format)
        {
            Format = format;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenTextFormatNumber FormatNumber(Text text, string format, float to, float duration)
        {
            var tweener = Play<TweenTextFormatNumber, Text, float>(text, to, duration)
                .SetFormat(format);
            return tweener;
        }

        public static TweenTextFormatNumber FormatNumber(Text text, string format, float from, float to, float duration)
        {
            var tweener = Play<TweenTextFormatNumber, Text, float>(text, from, to, duration)
                .SetFormat(format);
            return tweener;
        }
    }

    public static partial class TextExtension
    {
        public static TweenTextFormatNumber TweenFormatNumber(this Text text, string format, float to, float duration)
        {
            var tweener = UTween.FormatNumber(text, format, to, duration);
            return tweener;
        }

        public static TweenTextFormatNumber TweenFormatNumber(this Text text, string format, float from, float to, float duration)
        {
            var tweener = UTween.FormatNumber(text, format, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
