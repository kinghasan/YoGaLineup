using System;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class TweenValueString<TTarget> : Tweener<TTarget, string>
        where TTarget : UnityEngine.Object
    {
        public bool RichText;
        public virtual bool ShowRichText => true;

        protected StringBuilder StringBuilder => StringLerpUtil.StringBuilder;

        public string Evaluate(string from, string to, float factor)
        {
            StringBuilder.Clear();
            factor = Mathf.Clamp01(factor);
            var fromText = StringLerpUtil.Lerp(from, 1f - factor, RichText);
            var toText = StringLerpUtil.Lerp(to, factor, RichText);
            StringBuilder.Append(toText);
            StringBuilder.Append(fromText);
            var result = StringBuilder.ToString();
            return result;
        }

        public override void Sample(float factor)
        {
            var from = FromGetter();
            var to = ToGetter();
            var result = Evaluate(from, to, factor);
            ValueSetter(result);
            OnUpdate?.Invoke(result);
        }

        public override void Reset()
        {
            base.Reset();
            From = null;
            To = null;
            RichText = true;
        }
    }

#if UNITY_EDITOR

    public abstract partial class TweenValueString<TTarget> : Tweener<TTarget, string>
        where TTarget : UnityEngine.Object
    {
        [TweenerProperty, NonSerialized] public SerializedProperty RichTextProperty;

        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
        }

        public override void DrawAppend()
        {
            base.DrawAppend();

            if (ShowRichText)
            {
                GUIUtil.DrawToggleButton(RichTextProperty);
            }
        }
    }

#endif

    #region Extension

    public abstract partial class TweenValueString<TTarget> : Tweener<TTarget, string>
        where TTarget : UnityEngine.Object
    {
        public TweenValueString<TTarget> SetRichText(bool richText)
        {
            RichText = richText;
            return this;
        }
    }

    #endregion
}
