using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class TweenValueBoolean<TTarget> : Tweener<TTarget, bool>
        where TTarget : UnityEngine.Object
    {
        public override void Sample(float factor)
        {
            var from = FromGetter() ? 1f : 0f;
            var to = ToGetter() ? 1f : 0f;
            var result = Mathf.LerpUnclamped(from, to, factor) > 0.5f;
            ValueSetter(result);
            OnUpdate?.Invoke(result);
        }

        public override void Reset()
        {
            base.Reset();
            From = false;
            To = true;
        }
    }

#if UNITY_EDITOR

    public abstract partial class TweenValueBoolean<TTarget> : Tweener<TTarget, bool>
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
    }

#endif
}
