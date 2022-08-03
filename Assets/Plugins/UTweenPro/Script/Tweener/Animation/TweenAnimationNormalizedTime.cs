using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Animation Normalized Time", "Animation")]
    [Serializable]
    public partial class TweenAnimationNormalizedTime : TweenValueFloat<Animation>
    {
        public string Clip;

        public override float Value
        {
            get
            {
                var state = Target[Clip];
                if (state == null) return default;
                var progress = state.time / state.length;
                return progress;
            }
            set
            {
                var state = Target[Clip];
                if (state == null) return;
                var clip = state.clip;
                var time = clip.length * value;
                clip.SampleAnimation(Target.gameObject, time);
            }
        }

        public override void Reset()
        {
            base.Reset();
            Clip = null;
        }
    }

#if UNITY_EDITOR

    public partial class TweenAnimationNormalizedTime : TweenValueFloat<Animation>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty ClipProperty;

        public override void DrawTarget()
        {
            base.DrawTarget();

            if (Target == null)
            {
                ClipProperty.stringValue = null;
                return;
            }

            GUIMenu.SelectAnimationClipMenu(Target, nameof(Clip), ClipProperty);
        }
    }

#endif

    #region Extension

    public partial class TweenAnimationNormalizedTime : TweenValueFloat<Animation>
    {
        public TweenAnimationNormalizedTime SetClip(string clipName)
        {
            Clip = clipName;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenAnimationNormalizedTime NormalizedTime(Animation animation, string clipName, float to, float duration)
        {
            var tweener = Play<TweenAnimationNormalizedTime, Animation, float>(animation, to, duration)
                .SetClip(clipName);
            return tweener;
        }

        public static TweenAnimationNormalizedTime NormalizedTime(Animation animation, string clipName, float from, float to, float duration)
        {
            var tweener = Play<TweenAnimationNormalizedTime, Animation, float>(animation, from, to, duration)
                .SetClip(clipName);
            return tweener;
        }
    }

    public static partial class AnimationExtension
    {
        public static TweenAnimationNormalizedTime TweenNormalizedTime(this Animation animation, string clipName, float to, float duration)
        {
            var tweener = UTween.NormalizedTime(animation, clipName, to, duration);
            return tweener;
        }

        public static TweenAnimationNormalizedTime TweenNormalizedTime(this Animation animation, string clipName, float from, float to, float duration)
        {
            var tweener = UTween.NormalizedTime(animation, clipName, from, to, duration);
            return tweener;
        }
    }

    #endregion

}