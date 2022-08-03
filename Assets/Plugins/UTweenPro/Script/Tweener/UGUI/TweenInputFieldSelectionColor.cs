using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [Tweener("Input Field Selection Color ", "UGUI")]
    [Serializable]
    public class TweenInputFieldSelectionColor : TweenValueColor<InputField>
    {
        public override Color Value
        {
            get => Target.selectionColor;
            set => Target.selectionColor = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenInputFieldSelectionColor SelectionColor(InputField image, Color to, float duration)
        {
            var tweener = Play<TweenInputFieldSelectionColor, InputField, Color>(image, to, duration);
            return tweener;
        }

        public static TweenInputFieldSelectionColor SelectionColor(InputField image, Color from, Color to, float duration)
        {
            var tweener = Play<TweenInputFieldSelectionColor, InputField, Color>(image, from, to, duration);
            return tweener;
        }

        public static TweenInputFieldSelectionColor SelectionColor(InputField image, Gradient gradient, float duration)
        {
            var tweener = Play<TweenInputFieldSelectionColor, InputField>(image, duration)
                .SetColorMode(ColorMode.Gradient)
                .SetGradient(gradient) as TweenInputFieldSelectionColor;
            return tweener;
        }
    }

    public static partial class InputFieldExtension
    {
        public static TweenInputFieldSelectionColor TweenSelectionColor(this InputField image, Color to, float duration)
        {
            var tweener = UTween.SelectionColor(image, to, duration);
            return tweener;
        }

        public static TweenInputFieldSelectionColor TweenSelectionColor(this InputField image, Color from, Color to, float duration)
        {
            var tweener = UTween.SelectionColor(image, from, to, duration);
            return tweener;
        }

        public static TweenInputFieldSelectionColor TweenSelectionColor(this InputField image, Gradient gradient, float duration)
        {
            var tweener = UTween.SelectionColor(image, gradient, duration);
            return tweener;
        }
    }

    #endregion
}