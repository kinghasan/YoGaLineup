using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class TweenValueVector4<TTarget> : Tweener<TTarget, Vector4>
        where TTarget : UnityEngine.Object
    {
        public OnValueVector4Event OnUpdateEvent;

        public override bool SupportIndependentAxis => true;
        public override int AxisCount => 4;

        public override void Sample(float factor)
        {
            var from = FromGetter();
            var to = ToGetter();
            Vector4 result;
            var temp = Vector4.LerpUnclamped(from, to, factor);
            if (EnableAxis)
            {
                result = ValueGetter();
                if (AxisX) result.x = temp.x;
                if (AxisY) result.y = temp.y;
                if (AxisZ) result.z = temp.z;
                if (AxisW) result.w = temp.w;
            }
            else
            {
                result = temp;
            }

            ValueSetter(result);
            OnUpdate?.Invoke(result);
            OnUpdateEvent?.Invoke(result);
        }

        public override void Reset()
        {
            base.Reset();
            From = Vector4.zero;
            To = Vector4.one;
        }

        public override void ResetCallback()
        {
            base.ResetCallback();
            OnUpdateEvent = null;
        }
    }

#if UNITY_EDITOR

    public abstract partial class TweenValueVector4<TTarget> : Tweener<TTarget, Vector4>
        where TTarget : UnityEngine.Object
    {
        public override void DrawFromToValue()
        {
            GUIUtil.DrawVector4Property(FromProperty, nameof(From),
                AxisXName, AxisYName, AxisZName, AxisWName,
                AxisX, AxisY, AxisZ, AxisW);
            GUIUtil.DrawVector4Property(ToProperty, nameof(To),
                AxisXName, AxisYName, AxisZName, AxisWName,
                AxisX, AxisY, AxisZ, AxisW);
        }

        public override void DrawEvent()
        {
            if (OnUpdateEvent == null) OnUpdateEvent = new OnValueVector4Event();
            base.DrawEvent();
        }
    }
#endif
}
