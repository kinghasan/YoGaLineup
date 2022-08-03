using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Animator Parameter Float", "Animation")]
    [Serializable]
    public partial class TweenAnimatorParameterFloat : TweenValueFloat<Animator>
    {
        public string Parameter;

        public override float Value
        {
            get
            {
                if (string.IsNullOrEmpty(Parameter)) return default;
                return Target.GetFloat(Parameter);
            }
            set
            {
                if (string.IsNullOrEmpty(Parameter)) return;
                Target.SetFloat(Parameter, value);
            }
        }

        public override void Reset()
        {
            base.Reset();
            Parameter = null;
        }
    }

#if UNITY_EDITOR

    public partial class TweenAnimatorParameterFloat : TweenValueFloat<Animator>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty ParameterProperty;

        public override void DrawTarget()
        {
            base.DrawTarget();
            GUIMenu.SelectAnimatorParameterMenu(Target, nameof(Parameter), ParameterProperty, AnimatorControllerParameterType.Float);
        }
    }

#endif

    #region Extension

    public partial class TweenAnimatorParameterFloat : TweenValueFloat<Animator>
    {
        public TweenAnimatorParameterFloat SetParameter(string parameter)
        {
            Parameter = parameter;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenAnimatorParameterFloat AnimatorParameter(Animator animator, string parameter, float to, float duration)
        {
            var tweener = Play<TweenAnimatorParameterFloat, Animator, float>(animator, to, duration)
                .SetParameter(parameter);
            return tweener;
        }

        public static TweenAnimatorParameterFloat AnimatorParameter(Animator animator, string parameter, float from, float to, float duration)
        {
            var tweener = Play<TweenAnimatorParameterFloat, Animator, float>(animator, from, to, duration)
                .SetParameter(parameter);
            return tweener;
        }
    }

    public static partial class AnimatorExtension
    {
        public static TweenAnimatorParameterFloat TweenParameter(this Animator animator, string parameter, float to, float duration)
        {
            var tweener = UTween.AnimatorParameter(animator, parameter, to, duration);
            return tweener;
        }

        public static TweenAnimatorParameterFloat TweenParameter(this Animator animator, string parameter, float from, float to, float duration)
        {
            var tweener = UTween.AnimatorParameter(animator, parameter, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
