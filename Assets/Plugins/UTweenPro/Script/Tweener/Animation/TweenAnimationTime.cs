using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Animation Time", "Animation")]
    [Serializable]
    public partial class TweenAnimationTime : TweenValueFloat<Animation>
    {
        public string Clip;

        public override float Value
        {
            get
            {
                var state = Target[Clip];
                if (state == null) return default;
                return state.time;
            }
            set
            {
                var state = Target[Clip];
                if (state == null) return;
                var clip = state.clip;
                clip.SampleAnimation(Target.gameObject, value);
            }
        }

        public override void Reset()
        {
            base.Reset();
            Clip = null;
        }
    }

#if UNITY_EDITOR

    public partial class TweenAnimationTime : TweenValueFloat<Animation>
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

    public partial class TweenAnimationTime : TweenValueFloat<Animation>
    {
        public TweenAnimationTime SetClip(string clipName)
        {
            Clip = clipName;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenAnimationTime Time(Animation animation, string clipName, float to, float duration)
        {
            var tweener = Play<TweenAnimationTime, Animation, float>(animation, to, duration)
                .SetClip(clipName);
            return tweener;
        }

        public static TweenAnimationTime Time(Animation animation, string clipName, float from, float to, float duration)
        {
            var tweener = Play<TweenAnimationTime, Animation, float>(animation, from, to, duration)
                .SetClip(clipName);
            return tweener;
        }
    }

    public static partial class AnimationExtension
    {
        public static TweenAnimationTime TweenTime(this Animation animation, string clipName, float to, float duration)
        {
            var tweener = UTween.Time(animation, clipName, to, duration);
            return tweener;
        }

        public static TweenAnimationTime TweenTime(this Animation animation, string clipName, float from, float to, float duration)
        {
            var tweener = UTween.Time(animation, clipName, from, to, duration);
            return tweener;
        }
    }

    #endregion

}