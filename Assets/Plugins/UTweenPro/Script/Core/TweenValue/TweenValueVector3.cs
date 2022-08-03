using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class TweenValueVector3<TTarget> : Tweener<TTarget, Vector3>
        where TTarget : UnityEngine.Object
    {
        public override bool SupportIndependentAxis => true;
        public override int AxisCount => 3;

        public override void Sample(float factor)
        {
            var from = FromGetter();
            var to = ToGetter();
            Vector3 result;
            var temp = Vector3.LerpUnclamped(from, to, factor);
            if (EnableAxis)
            {
                result = ValueGetter();
                if (AxisX) result.x = temp.x;
                if (AxisY) result.y = temp.y;
                if (AxisZ) result.z = temp.z;
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
            From = Vector3.zero;
            To = Vector3.one;
        }
    }

#if UNITY_EDITOR

    public abstract partial class TweenValueVector3<TTarget> : Tweener<TTarget, Vector3>
        where TTarget : UnityEngine.Object
    {
        public override void DrawFromToValue()
        {
            GUIUtil.DrawVector3Property(FromProperty, nameof(From),
                AxisXName, AxisYName, AxisZName,
                AxisX, AxisY, AxisZ);
            GUIUtil.DrawVector3Property(ToProperty, nameof(To),
                AxisXName, AxisYName, AxisZName,
                AxisX, AxisY, AxisZ);
        }
    }
#endif
}
