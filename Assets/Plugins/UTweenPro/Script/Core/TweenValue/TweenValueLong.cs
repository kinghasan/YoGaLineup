using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class TweenValueLong<TTarget> : Tweener<TTarget, long>
        where TTarget : UnityEngine.Object
    {
        public OnValueLongEvent OnUpdateEvent;

        public override void Sample(float factor)
        {
            var from = FromGetter();
            var to = ToGetter();
            var result = (long)Math.Round(from + (to - from) * factor);
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

    public abstract partial class TweenValueLong<TTarget> : Tweener<TTarget, long>
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
            if (OnUpdateEvent == null) OnUpdateEvent = new OnValueLongEvent();
            base.DrawEvent();
        }
    }

#endif
}