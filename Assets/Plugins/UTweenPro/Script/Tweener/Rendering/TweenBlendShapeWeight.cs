using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Blend Shape Weight", "Rendering")]
    [Serializable]
    public partial class TweenBlendShapeWeight : TweenValueFloat<SkinnedMeshRenderer>
    {
        public int BlendShapeIndex;

        public override float Value
        {
            get => Target.GetBlendShapeWeight(BlendShapeIndex);
            set => Target.SetBlendShapeWeight(BlendShapeIndex, value);
        }

        public override void Reset()
        {
            base.Reset();
            From = 0;
            To = 100;
            BlendShapeIndex = -1;
        }
    }

#if UNITY_EDITOR

    public partial class TweenBlendShapeWeight : TweenValueFloat<SkinnedMeshRenderer>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty BlendShapeIndexProperty;

        public override void DrawTarget()
        {
            base.DrawTarget();
            if (Data.Mode == TweenEditorMode.Component)
            {
                if (Target == null)
                {
                    if (BlendShapeIndexProperty.intValue >= 0) BlendShapeIndexProperty.intValue = -1;
                    return;
                }

                GUIMenu.SelectBlendShapeMenu(Target, "Shape", BlendShapeIndexProperty);
            }
            else
            {
                using (GUIHorizontal.Create())
                {
                    EditorGUILayout.PropertyField(BlendShapeIndexProperty, new GUIContent("Shape"));
                }
            }
        }
    }

#endif

    #region Extension

    public partial class TweenBlendShapeWeight : TweenValueFloat<SkinnedMeshRenderer>
    {
        public TweenBlendShapeWeight SetBlendShapeIndex(int blendShapeIndex)
        {
            BlendShapeIndex = blendShapeIndex;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenBlendShapeWeight BlendShapeWeight(SkinnedMeshRenderer renderer, int blendShapeIndex, float to, float duration)
        {
            var tweener = Play<TweenBlendShapeWeight, SkinnedMeshRenderer, float>(renderer, to, duration)
                .SetBlendShapeIndex(blendShapeIndex);
            return tweener;
        }

        public static TweenBlendShapeWeight BlendShapeWeight(SkinnedMeshRenderer renderer, int blendShapeIndex, float from, float to, float duration)
        {
            var tweener = Play<TweenBlendShapeWeight, SkinnedMeshRenderer, float>(renderer, from, to, duration)
                .SetBlendShapeIndex(blendShapeIndex);
            return tweener;
        }
    }

    public static partial class SkinnedMeshRendererExtension
    {
        public static TweenBlendShapeWeight TweenBlendShapeWeight(this SkinnedMeshRenderer renderer, int blendShapeIndex, float to, float duration)
        {
            var tweener = UTween.BlendShapeWeight(renderer, blendShapeIndex, to, duration);
            return tweener;
        }

        public static TweenBlendShapeWeight TweenBlendShapeWeight(this SkinnedMeshRenderer renderer, int blendShapeIndex, float from, float to, float duration)
        {
            var tweener = UTween.BlendShapeWeight(renderer, blendShapeIndex, from, to, duration);
            return tweener;
        }
    }

    #endregion
}