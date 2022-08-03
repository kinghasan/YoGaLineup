using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class TweenValueTransform<TTarget> : Tweener<TTarget, Transform>
        where TTarget : UnityEngine.Object
    {
        public override bool SupportSpace => true;
        public override bool SupportOnUpdate => false;
        public override bool SupportIndependentAxis => true;
        public override int AxisCount => 3;

        public override void Sample(float factor)
        {
            var from = FromGetter();
            var to = ToGetter();
            var value = ValueGetter();
            if (Space == SpaceMode.World)
            {
                var result = (value.position, value.eulerAngles, value.localScale);
                if (AxisX)
                {
                    var position = Vector3.LerpUnclamped(from.position, to.position, factor);
                    result.position = position;
                }

                if (AxisY)
                {
                    var eulerAngles = Vector3.LerpUnclamped(from.eulerAngles, to.eulerAngles, factor);
                    result.eulerAngles = eulerAngles;
                }

                if (AxisZ)
                {
                    var localScale = Vector3.LerpUnclamped(from.localScale, to.localScale, factor);
                    result.localScale = localScale;
                }

                value.position = result.position;
                value.eulerAngles = result.eulerAngles;
                value.localScale = result.localScale;
            }

            if (Space == SpaceMode.Local)
            {
                var result = (value.localPosition, value.localEulerAngles, value.localScale);
                if (AxisX)
                {
                    var position = Vector3.Lerp(from.localPosition, to.localPosition, factor);
                    result.localPosition = position;
                }

                if (AxisY)
                {
                    var eulerAngles = Vector3.Lerp(from.localEulerAngles, to.localEulerAngles, factor);
                    result.localEulerAngles = eulerAngles;
                }

                if (AxisZ)
                {
                    var localScale = Vector3.Lerp(from.localScale, to.localScale, factor);
                    result.localScale = localScale;
                }

                value.localPosition = result.localPosition;
                value.localEulerAngles = result.localEulerAngles;
                value.localScale = result.localScale;
            }
        }

        public override void Reset()
        {
            base.Reset();
            From = null;
            To = null;
        }
    }

#if UNITY_EDITOR

    public abstract partial class TweenValueTransform<TTarget> : Tweener<TTarget, Transform>
        where TTarget : UnityEngine.Object
    {
        public override string AxisXName => "P";
        public override string AxisYName => "R";
        public override string AxisZName => "S";

        public override void DrawFromToValue()
        {
            using (GUIVertical.Create())
            {
                var transform = Data.Mode == TweenEditorMode.Component ? MonoBehaviour.transform : null;
                using (GUILabelWidthArea.Create(EditorStyle.FromToValueLabelWidth))
                {
                    using (GUIErrorColorArea.Create(From == null))
                    {
                        EditorGUILayout.PropertyField(FromProperty);
                        EditorGUILayout.PropertyField(ToProperty);
                    }
                }
            }
        }
    }
#endif
}
