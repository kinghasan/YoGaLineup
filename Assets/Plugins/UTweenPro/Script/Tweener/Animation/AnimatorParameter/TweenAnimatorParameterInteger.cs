using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Animator Parameter Integer", "Animation")]
    [Serializable]
    public partial class TweenAnimatorParameterInteger : TweenValueInteger<Animator>
    {
        public string Parameter;

        public override int Value
        {
            get
            {
                if (string.IsNullOrEmpty(Parameter)) return default;
                return Target.GetInteger(Parameter);
            }
            set
            {
                if (string.IsNullOrEmpty(Parameter)) return;
                Target.SetInteger(Parameter, value);
            }
        }

        public override void Reset()
        {
            base.Reset();
            Parameter = null;
        }
    }

#if UNITY_EDITOR

    public partial class TweenAnimatorParameterInteger : TweenValueInteger<Animator>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty ParameterProperty;

        public override void DrawTarget()
        {
            base.DrawTarget();
            GUIMenu.SelectAnimatorParameterMenu(Target, nameof(Parameter), ParameterProperty, AnimatorControllerParameterType.Int);
        }
    }

#endif

    #region Extension

    public partial class TweenAnimatorParameterInteger : TweenValueInteger<Animator>
    {
        public TweenAnimatorParameterInteger SetParameter(string parameter)
        {
            Parameter = parameter;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenAnimatorParameterInteger AnimatorParameter(Animator animator, string parameter, int to, float duration)
        {
            var tweener = Play<TweenAnimatorParameterInteger, Animator, int>(animator, to, duration)
                .SetParameter(parameter);
            return tweener;
        }

        public static TweenAnimatorParameterInteger AnimatorParameter(Animator animator, string parameter, int from, int to, float duration)
        {
            var tweener = Play<TweenAnimatorParameterInteger, Animator, int>(animator, from, to, duration)
                .SetParameter(parameter);
            return tweener;
        }
    }

    public static partial class AnimatorExtension
    {
        public static TweenAnimatorParameterInteger TweenParameter(this Animator animator, string parameter, int to, float duration)
        {
            var tweener = UTween.AnimatorParameter(animator, parameter, to, duration);
            return tweener;
        }

        public static TweenAnimatorParameterInteger TweenParameter(this Animator animator, string parameter, int from, int to, float duration)
        {
            var tweener = UTween.AnimatorParameter(animator, parameter, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
