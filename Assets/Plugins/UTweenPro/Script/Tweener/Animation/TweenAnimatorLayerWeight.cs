using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Animator Layer Weight", "Animation")]
    [Serializable]
    public partial class TweenAnimatorLayerWeight : TweenValueFloat<Animator>
    {
        public int Layer;

        public override float Value
        {
            get => Target.GetLayerWeight(Layer);
            set => Target.SetLayerWeight(Layer, value);
        }

        public override void Reset()
        {
            base.Reset();
            Layer = 0;
        }
    }

#if UNITY_EDITOR

    public partial class TweenAnimatorLayerWeight : TweenValueFloat<Animator>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty LayerProperty;

        public override void DrawTarget()
        {
            base.DrawTarget();

            if (Target == null)
            {
                return;
            }

            GUIMenu.SelectAnimatorLayerMenu(Target, nameof(Layer), LayerProperty);
        }
    }

#endif

    #region Extension

    public partial class TweenAnimatorLayerWeight : TweenValueFloat<Animator>
    {
        public TweenAnimatorLayerWeight SetLayerIndex(int layerIndex)
        {
            Layer = layerIndex;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenAnimatorLayerWeight LayerWeight(Animator animator, int layerIndex, float to, float duration)
        {
            var tweener = Play<TweenAnimatorLayerWeight, Animator, float>(animator, to, duration)
                .SetLayerIndex(layerIndex);
            return tweener;
        }

        public static TweenAnimatorLayerWeight LayerWeight(Animator animator, int layerIndex, float from, float to, float duration)
        {
            var tweener = Play<TweenAnimatorLayerWeight, Animator, float>(animator, from, to, duration)
                .SetLayerIndex(layerIndex);
            return tweener;
        }
    }

    public static partial class AnimatorExtension
    {
        public static TweenAnimatorLayerWeight TweenLayerWeight(this Animator animator, int layerIndex, float to, float duration)
        {
            var tweener = UTween.LayerWeight(animator, layerIndex, to, duration);
            return tweener;
        }

        public static TweenAnimatorLayerWeight TweenLayerWeight(this Animator animator, int layerIndex, float from, float to, float duration)
        {
            var tweener = UTween.LayerWeight(animator, layerIndex, from, to, duration);
            return tweener;
        }
    }

    #endregion
}