using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class TweenValueRect<TTarget> : Tweener<TTarget, Rect>
        where TTarget : UnityEngine.Object
    {
        public OnValueRectEvent OnUpdateEvent;

        public override bool SupportIndependentAxis => true;
        public override int AxisCount => 4;

        public override void Sample(float factor)
        {
            var from = FromGetter();
            var to = ToGetter();
            Rect result;
            var pos = Vector2.LerpUnclamped(from.position, to.position, factor);
            var size = Vector2.LerpUnclamped(from.size, to.size, factor);
            if (EnableAxis)
            {
                result = ValueGetter();
                if (AxisX) result.x = pos.x;
                if (AxisY) result.y = pos.y;
                if (AxisZ) result.width = size.x;
                if (AxisW) result.height = size.y;
            }
            else
            {
                result = new Rect(pos, size);
            }

            ValueSetter(result);
            OnUpdate?.Invoke(result);
            OnUpdateEvent?.Invoke(result);
        }

        public override void Reset()
        {
            base.Reset();
            From = default;
            To = default;
        }

        public override void ResetCallback()
        {
            base.ResetCallback();
            OnUpdateEvent = null;
        }
    }

#if UNITY_EDITOR

    public abstract partial class TweenValueRect<TTarget> : Tweener<TTarget, Rect>
        where TTarget : UnityEngine.Object
    {
        public override string AxisZName => "W";
        public override string AxisWName => "H";

        public override void DrawFromToValue()
        {
            GUIUtil.DrawRectProperty(FromProperty, nameof(From),
                AxisXName, AxisYName, AxisZName, AxisWName,
                AxisX, AxisY, AxisZ, AxisW);
            GUIUtil.DrawRectProperty(ToProperty, nameof(To),
                AxisXName, AxisYName, AxisZName, AxisWName,
                AxisX, AxisY, AxisZ, AxisW);
        }

        public override void DrawEvent()
        {
            if (OnUpdateEvent == null) OnUpdateEvent = new OnValueRectEvent();
            base.DrawEvent();
        }
    }
#endif
}
