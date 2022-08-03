using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Input Field Caret Color ", "UGUI")]
    [Serializable]
    public class TweenInputFieldCaretColor : TweenValueColor<InputField>
    {
        public override Color Value
        {
            get => Target.caretColor;
            set => Target.caretColor = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenInputFieldCaretColor CaretColor(InputField image, Color to, float duration)
        {
            var tweener = Play<TweenInputFieldCaretColor, InputField, Color>(image, to, duration);
            return tweener;
        }

        public static TweenInputFieldCaretColor CaretColor(InputField image, Color from, Color to, float duration)
        {
            var tweener = Play<TweenInputFieldCaretColor, InputField, Color>(image, from, to, duration);
            return tweener;
        }

        public static TweenInputFieldCaretColor CaretColor(InputField image, Gradient gradient, float duration)
        {
            var tweener = Play<TweenInputFieldCaretColor, InputField>(image, duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient) as TweenInputFieldCaretColor;
            return tweener;
        }
    }

    public static partial class InputFieldExtension
    {
        public static TweenInputFieldCaretColor TweenCaretColor(this InputField image, Color to, float duration)
        {
            var tweener = UTween.CaretColor(image, to, duration);
            return tweener;
        }

        public static TweenInputFieldCaretColor TweenCaretColor(this InputField image, Color from, Color to, float duration)
        {
            var tweener = UTween.CaretColor(image, from, to, duration);
            return tweener;
        }

        public static TweenInputFieldCaretColor TweenCaretColor(this InputField image, Gradient gradient, float duration)
        {
            var tweener = UTween.CaretColor(image, gradient, duration);
            return tweener;
        }
    }

    #endregion
}