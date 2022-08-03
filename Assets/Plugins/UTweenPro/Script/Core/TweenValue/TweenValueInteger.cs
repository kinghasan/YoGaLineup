using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class TweenValueInteger<TTarget> : Tweener<TTarget, int>
        where TTarget : UnityEngine.Object
    {
        public OnValueIntegerEvent OnUpdateEvent;

        public override void Sample(float factor)
        {
            var from = FromGetter();
            var to = ToGetter();
            var result = Mathf.RoundToInt(Mathf.LerpUnclamped(from, to, factor));
            ValueSetter(result);
            OnUpdate?.Invoke(result);
            OnUpdateEvent?.Invoke(result);
        }

        public override void Reset()
        {
            base.Reset();
            From = 0;
            To = 1;
        }

        public override void ResetCallback()
        {
            base.ResetCallback();
            OnUpdateEvent = null;
        }
    }

#if UNITY_EDITOR

    public abstract partial class TweenValueInteger<TTarget> : Tweener<TTarget, int>
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
            if (OnUpdateEvent == null) OnUpdateEvent = new OnValueIntegerEvent();
            base.DrawEvent();
        }
    }

#endif
}
