using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class TweenValueDouble<TTarget> : Tweener<TTarget, double>
        where TTarget : UnityEngine.Object
    {
        public OnValueDoubleEvent OnUpdateEvent;

        public override void Sample(float factor)
        {
            var from = FromGetter();
            var to = ToGetter();
            var result = from + (to - from) * factor;
            ValueSetter(result);
            OnUpdate?.Invoke(result);
            OnUpdateEvent?.Invoke(result);
        }

        public override void Reset()
        {
            base.Reset();
            From = 0f;
            To = 1f;
        }

        public override void ResetCallback()
        {
            base.ResetCallback();
            OnUpdateEvent = null;
        }
    }

#if UNITY_EDITOR

    public abstract partial class TweenValueDouble<TTarget> : Tweener<TTarget, double>
        where TTarget : UnityEngine.Object
    {
        public override void DrawFromToValue()
        {
            using (GUIHorizontal.Create())
            {
                GUIUtil.DrawProperty(FromProperty);
                GUIUtil.DrawProperty(ToProperty);
            }
        }

        public override void DrawEvent()
        {
            if (OnUpdateEvent == null) OnUpdateEvent = new OnValueDoubleEvent();
            base.DrawEvent();
        }
    }

#endif
}
