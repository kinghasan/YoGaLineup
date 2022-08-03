using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class TweenValueColor<TTarget> : Tweener<TTarget, Color>
        where TTarget : UnityEngine.Object
    {
        public ColorMode ColorMode;
        public Gradient Gradient;

        public override bool SupportIndependentAxis => true;
        public override int AxisCount => 4;

        public override void Sample(float factor)
        {
            var from = FromGetter();
            var to = ToGetter();
            Color result;
            var temp = ColorMode == ColorMode.FromTo ? Color.LerpUnclamped(from, to, factor) : Gradient.Evaluate(factor);
            if (EnableAxis)
            {
                result = ValueGetter();
                if (AxisX) result.r = temp.r;
                if (AxisY) result.g = temp.g;
                if (AxisZ) result.b = temp.b;
                if (AxisW) result.a = temp.a;
            }
            else
            {
                result = temp;
            }

            ValueSetter(result);
            OnUpdate?.Invoke(result);
        }

        public override void Reset()
        {
            base.Reset();
            From = Color.black;
            To = Color.white;
            ColorMode = ColorMode.FromTo;
            Gradient = new Gradient();
            Gradient.SetKeys(new[] {new GradientColorKey(Color.black, 0f), new GradientColorKey(Color.white, 1f)}, new[] {new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f)});
        }

        public override void ReverseFromTo()
        {
            base.ReverseFromTo();
            Gradient.Reverse();
        }
    }

#if UNITY_EDITOR

    public abstract partial class TweenValueColor<TTarget> : Tweener<TTarget, Color>
        where TTarget : UnityEngine.Object
    {
        public override string AxisXName => "R";
        public override string AxisYName => "G";
        public override string AxisZName => "B";
        public override string AxisWName => "A";

        [TweenerProperty, NonSerialized] public SerializedProperty ColorModeProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty GradientProperty;

        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
        }

        public override void DrawFromToValue()
        {
            GUIUtil.DrawToolbarEnum(ColorModeProperty, "Mode", typeof(ColorMode));
            if (ColorMode == ColorMode.FromTo)
            {
                using (GUIHorizontal.Create())
                {
                    GUIUtil.DrawProperty(FromProperty);
                    GUIUtil.DrawProperty(ToProperty);
                }
            }

            if (ColorMode == ColorMode.Gradient)
            {
                using (GUIHorizontal.Create())
                {
                    using (GUILabelWidthArea.Create(EditorStyle.FromToValueLabelWidth))
                    {
                        EditorGUILayout.PropertyField(GradientProperty, new GUIContent(nameof(Color)));
                    }
                }
                
            }
        }
    }
#endif

    #region Extension

    public abstract partial class TweenValueColor<TTarget> : Tweener<TTarget, Color>
        where TTarget : UnityEngine.Object
    {
        public TweenValueColor<TTarget> SetColorMode(ColorMode colorMode)
        {
            ColorMode = colorMode;
            return this;
        }

        public TweenValueColor<TTarget> SetGradient(Gradient gradient)
        {
            Gradient = gradient;
            return this;
        }
    }

    #endregion
}
