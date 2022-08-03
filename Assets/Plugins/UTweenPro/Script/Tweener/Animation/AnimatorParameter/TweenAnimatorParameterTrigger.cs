using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Animator Parameter Trigger", "Animation")]
    [Serializable]
    public partial class TweenAnimatorParameterTrigger : TweenValueBoolean<Animator>
    {
        public string Parameter;

        public override bool Value
        {
            get => _trigger;
            set
            {
                if (string.IsNullOrEmpty(Parameter)) return;
                if (_trigger == value) return;
                _trigger = value;
                if (_trigger)
                {
                    Target.SetTrigger(Parameter);
                }
                else
                {
                    Target.ResetTrigger(Parameter);
                }
            }
        }

        private bool _trigger;

        public override void Reset()
        {
            base.Reset();
            Parameter = null;
        }
    }

#if UNITY_EDITOR

    public partial class TweenAnimatorParameterTrigger : TweenValueBoolean<Animator>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty ParameterProperty;

        public override void DrawTarget()
        {
            base.DrawTarget();
            GUIMenu.SelectAnimatorParameterMenu(Target, nameof(Parameter), ParameterProperty, AnimatorControllerParameterType.Trigger);
        }
    }

#endif

    #region Extension

    public partial class TweenAnimatorParameterTrigger : TweenValueBoolean<Animator>
    {
        public TweenAnimatorParameterTrigger SetParameter(string parameter)
        {
            Parameter = parameter;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenAnimatorParameterTrigger AnimatorTrigger(Animator animator, string parameter, bool to, float duration)
        {
            var tweener = Play<TweenAnimatorParameterTrigger, Animator, bool>(animator, to, duration)
                .SetParameter(parameter);
            return tweener;
        }

        public static TweenAnimatorParameterTrigger AnimatorTrigger(Animator animator, string parameter, bool from, bool to, float duration)
        {
            var tweener = Play<TweenAnimatorParameterTrigger, Animator, bool>(animator, from, to, duration)
                .SetParameter(parameter);
            return tweener;
        }
    }

    public static partial class AnimatorExtension
    {
        public static TweenAnimatorParameterTrigger TweenTrigger(this Animator animator, string parameter, bool to, float duration)
        {
            var tweener = UTween.AnimatorTrigger(animator, parameter, to, duration);
            return tweener;
        }

        public static TweenAnimatorParameterTrigger TweenTrigger(this Animator animator, string parameter, bool from, bool to, float duration)
        {
            var tweener = UTween.AnimatorTrigger(animator, parameter, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
