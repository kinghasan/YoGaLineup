using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class Tweener<TTarget> : Tweener
        where TTarget : UnityEngine.Object
    {
        public TTarget Target;

        public override void SetDirty()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && Target != null)
            {
                EditorUtility.SetDirty(Target);
            }
#endif
        }

        public override void Reset()
        {
            base.Reset();
            Target = null;
        }
    }
}