using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Animator Parameter Bool", "Animation")]
    [Serializable]
    public partial class TweenAnimatorParameterBool : TweenValueBoolean<Animator>
    {
        public string Parameter;

        public override bool Value
        {
            get
            {
                if (string.IsNullOrEmpty(Parameter)) return default;
                return Target.GetBool(Parameter);
            }
            set
            {
                if (string.IsNullOrEmpty(Parameter)) return;
                Target.SetBool(Parameter, value);
            }
        }

        public override void Reset()
        {
            base.Reset();
            Parameter = null;
        }
    }

#if UNITY_EDITOR

    public partial class TweenAnimatorParameterBool : TweenValueBoolean<Animator>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty ParameterProperty;

        public override void DrawTarget()
        {
            base.DrawTarget();
            GUIMenu.SelectAnimatorParameterMenu(Target, nameof(Parameter), ParameterProperty, AnimatorControllerParameterType.Bool);
        }
    }

#endif

    #region Extension

    public partial class TweenAnimatorParameterBool : TweenValueBoolean<Animator>
    {
        public TweenAnimatorParameterBool SetParameter(string parameter)
        {
            Parameter = parameter;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenAnimatorParameterBool AnimatorParameter(Animator animator, string parameter, bool to, float duration)
        {
            var tweener = Play<TweenAnimatorParameterBool, Animator, bool>(animator, to, duration)
                .SetParameter(parameter);
            return tweener;
        }

        public static TweenAnimatorParameterBool AnimatorParameter(Animator animator, string parameter, bool from, bool to, float duration)
        {
            var tweener = Play<TweenAnimatorParameterBool, Animator, bool>(animator, from, to, duration)
                .SetParameter(parameter);
            return tweener;
        }
    }

    public static partial class AnimatorExtension
    {
        public static TweenAnimatorParameterBool TweenParameter(this Animator animator, string parameter, bool to, float duration)
        {
            var tweener = UTween.AnimatorParameter(animator, parameter, to, duration);
            return tweener;
        }

        public static TweenAnimatorParameterBool TweenParameter(this Animator animator, string parameter, bool from, bool to, float duration)
        {
            var tweener = UTween.AnimatorParameter(animator, parameter, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
