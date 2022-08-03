using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Text Typer", "UGUI Text")]
    [Serializable]
    public partial class TweenTextTyper : TweenValueFloat<Text>
    {
        public bool RichText;

        public override float Value
        {
            get => _value;
            set
            {
                _value = Mathf.Clamp01(value);
                Target.text = StringLerpUtil.Lerp(RecordText, _value, RichText);
            }
        }

        private float _value;

        protected string RecordText;

        public override void RecordObject()
        {
            if (Target != null) RecordText = Target.text;
        }

        public override void RestoreObject()
        {
            if (Target != null) Target.text = RecordText;
        }

        public override void Reset()
        {
            base.Reset();
            From = 0f;
            To = 1f;
            RichText = true;
        }
    }

#if UNITY_EDITOR

    public partial class TweenTextTyper : TweenValueFloat<Text>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty RichTextProperty;
        
        public override void DrawFromToValue()
        {
            base.DrawFromToValue();
            From = Mathf.Clamp01(From);
            To = Mathf.Clamp01(To);
        }

        public override void DrawAppend()
        {
            base.DrawAppend();
            GUIUtil.DrawToggleButton(RichTextProperty);
        }
    }

#endif

    #region Extension

    public partial class TweenTextTyper : TweenValueFloat<Text>
    {
        public TweenTextTyper SetRichText(bool richText)
        {
            RichText = richText;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenTextTyper TextTyper(Text text, float to, float duration, bool richText = true)
        {
            var tweener = Play<TweenTextTyper, Text, float>(text, to, duration)
                .SetRichText(richText);
            return tweener;
        }

        public static TweenTextTyper TextTyper(Text text, float from, float to, float duration, bool richText = true)
        {
            var tweener = Play<TweenTextTyper, Text, float>(text, from, to, duration)
                .SetRichText(richText);
            return tweener;
        }
    }

    public static partial class TextExtension
    {
        public static TweenTextTyper TweenTextTyper(this Text text, float to, float duration, bool richText = true)
        {
            var tweener = UTween.TextTyper(text, to, duration, richText);
            return tweener;
        }

        public static TweenTextTyper TweenTextTyper(this Text text, float from, float to, float duration, bool richText = true)
        {
            var tweener = UTween.TextTyper(text, from, to, duration, richText);
            return tweener;
        }
    }

    #endregion
}