using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class TweenValueVector2<TTarget> : Tweener<TTarget, Vector2>
        where TTarget : UnityEngine.Object
    {
        public override bool SupportIndependentAxis => true;
        public override int AxisCount => 2;

        public override void Sample(float factor)
        {
            var from = FromGetter();
            var to = ToGetter();
            Vector2 result;
            var temp = Vector2.LerpUnclamped(from, to, factor);
            if (EnableAxis)
            {
                result = ValueGetter();
                if (AxisX) result.x = temp.x;
                if (AxisY) result.y = temp.y;
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
            From = Vector2.zero;
            To = Vector2.one;
        }
    }

#if UNITY_EDITOR

    public abstract partial class TweenValueVector2<TTarget> : Tweener<TTarget, Vector2>
        where TTarget : UnityEngine.Object
    {
        public override void DrawFromToValue()
        {
            using (GUIVertical.Create())
            {
                GUIUtil.DrawVector2Property(FromProperty, nameof(From), AxisXName, AxisYName, AxisX, AxisY);
                GUIUtil.DrawVector2Property(ToProperty, nameof(To), AxisXName, AxisYName, AxisX, AxisY);
            }
        }
    }
#endif
}
