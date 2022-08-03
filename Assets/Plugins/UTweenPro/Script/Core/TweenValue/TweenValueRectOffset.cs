using System;
using UnityEngine;
using RectOffset = UnityEngine.RectOffset;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class TweenValueRectOffset<TTarget> : Tweener<TTarget, RectOffset>
        where TTarget : UnityEngine.Object
    {
        public override bool SupportIndependentAxis => true;
        public override int AxisCount => 4;

        public override void Sample(float factor)
        {
            var from = FromGetter();
            var to = ToGetter();
            RectOffset result;
            var left = Mathf.RoundToInt(Mathf.LerpUnclamped(from.left, to.left, factor));
            var right = Mathf.RoundToInt(Mathf.LerpUnclamped(from.right, to.right, factor));
            var top = Mathf.RoundToInt(Mathf.LerpUnclamped(from.top, to.top, factor));
            var bottom = Mathf.RoundToInt(Mathf.LerpUnclamped(from.bottom, to.bottom, factor));
            if (EnableAxis)
            {
                result = ValueGetter();
                if (AxisX) result.left = left;
                if (AxisY) result.right = right;
                if (AxisZ) result.top = top;
                if (AxisW) result.bottom = bottom;
            }
            else
            {
                result = new RectOffset(left, right, top, bottom);
            }

            ValueSetter(result);
            OnUpdate?.Invoke(result);
        }

        public override void Reset()
        {
            base.Reset();
            From = new RectOffset();
            To = new RectOffset(1, 1, 1, 1);
        }
    }

#if UNITY_EDITOR

    public abstract partial class TweenValueRectOffset<TTarget> : Tweener<TTarget, RectOffset>
        where TTarget : UnityEngine.Object
    {
        public override string AxisXName => "L";
        public override string AxisYName => "R";
        public override string AxisZName => "T";
        public override string AxisWName => "B";

        [NonSerialized] public SerializedProperty FromLProperty;
        [NonSerialized] public SerializedProperty FromRProperty;
        [NonSerialized] public SerializedProperty FromTProperty;
        [NonSerialized] public SerializedProperty FromBProperty;

        [NonSerialized] public SerializedProperty ToLProperty;
        [NonSerialized] public SerializedProperty ToRProperty;
        [NonSerialized] public SerializedProperty ToTProperty;
        [NonSerialized] public SerializedProperty ToBProperty;

        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            FromLProperty = FromProperty.FindPropertyRelative("m_Left");
            FromRProperty = FromProperty.FindPropertyRelative("m_Right");
            FromTProperty = FromProperty.FindPropertyRelative("m_Top");
            FromBProperty = FromProperty.FindPropertyRelative("m_Bottom");

            ToLProperty = ToProperty.FindPropertyRelative("m_Left");
            ToRProperty = ToProperty.FindPropertyRelative("m_Right");
            ToTProperty = ToProperty.FindPropertyRelative("m_Top");
            ToBProperty = ToProperty.FindPropertyRelative("m_Bottom");
        }

        public override void DrawFromToValue()
        {
            using (GUIHorizontal.Create())
            {
                GUILayout.Label(nameof(From), EditorStyles.label, GUILayout.Width(EditorStyle.FromToValueLabelWidth));
                using (GUILabelWidthArea.Create(EditorStyle.CharacterWidth))
                {
                    FromLProperty.intValue = GUIUtil.DrawIntProperty(AxisXName, FromLProperty.intValue, AxisX);
                    FromRProperty.intValue = GUIUtil.DrawIntProperty(AxisYName, FromRProperty.intValue, AxisY);
                    FromTProperty.intValue = GUIUtil.DrawIntProperty(AxisZName, FromTProperty.intValue, AxisZ);
                    FromBProperty.intValue = GUIUtil.DrawIntProperty(AxisWName, FromBProperty.intValue, AxisW);
                }
            }

            using (GUIHorizontal.Create())
            {
                GUILayout.Label(nameof(To), EditorStyles.label, GUILayout.Width(EditorStyle.FromToValueLabelWidth));
                using (GUILabelWidthArea.Create(EditorStyle.CharacterWidth))
                {
                    ToLProperty.intValue = GUIUtil.DrawIntProperty(AxisXName, ToLProperty.intValue, AxisX);
                    ToRProperty.intValue = GUIUtil.DrawIntProperty(AxisYName, ToRProperty.intValue, AxisY);
                    ToTProperty.intValue = GUIUtil.DrawIntProperty(AxisZName, ToTProperty.intValue, AxisZ);
                    ToBProperty.intValue = GUIUtil.DrawIntProperty(AxisWName, ToBProperty.intValue, AxisW);
                }
            }
        }
    }
#endif
}
