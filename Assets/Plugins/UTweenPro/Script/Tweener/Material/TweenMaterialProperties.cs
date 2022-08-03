using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Material Properties", "Material", "Material Icon")]
    [Serializable]
    public partial class TweenMaterialProperties : TweenValueFloat<Renderer>
    {
        public int MaterialIndex;
        public Material Start;
        public Material End;

        public override float Value
        {
            get => _value;
            set
            {
                _value = value;
                Target.materials[MaterialIndex].Lerp(Start, End, _value);
            }
        }

        private float _value;

        public override void Reset()
        {
            base.Reset();
            MaterialIndex = -1;
            Start = null;
            End = null;
        }
    }

#if UNITY_EDITOR

    public partial class TweenMaterialProperties : TweenValueFloat<Renderer>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty MaterialIndexProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty StartProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty EndProperty;

        public override void DrawTarget()
        {
            base.DrawTarget();

            if (Data.Mode == TweenEditorMode.Component)
            {
                if (Target == null)
                {
                    if (MaterialIndexProperty.intValue >= 0) MaterialIndexProperty.intValue = -1;
                    return;
                }

                using (GUIErrorColorArea.Create(MaterialIndex < 0))
                {
                    GUIMenu.SelectMaterialMenu(Target, "Material", MaterialIndexProperty);
                }
            }
            else
            {
                using (GUIHorizontal.Create())
                {
                    EditorGUILayout.PropertyField(MaterialIndexProperty, new GUIContent("Material"));
                }
            }

            using (GUIHorizontal.Create())
            {
                using (GUIErrorColorArea.Create(Start == null))
                {
                    EditorGUILayout.PropertyField(StartProperty);
                }

                using (GUIErrorColorArea.Create(End == null))
                {
                    EditorGUILayout.PropertyField(EndProperty);
                }
            }
        }
    }

#endif

    #region Extension

    public partial class TweenMaterialProperties : TweenValueFloat<Renderer>
    {
        public TweenMaterialProperties SetMaterialIndex(int materialIndex)
        {
            MaterialIndex = materialIndex;
            return this;
        }

        public TweenMaterialProperties SetStartMaterial(Material startMaterial)
        {
            Start = startMaterial;
            return this;
        }

        public TweenMaterialProperties SetEndMaterial(Material endMaterial)
        {
            End = endMaterial;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenMaterialProperties Properties(Renderer renderer, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = Properties(renderer, 0, 0f, 1f, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterialProperties Properties(Renderer renderer, int materialIndex, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = Properties(renderer, materialIndex, 0f, 1f, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterialProperties Properties(Renderer renderer, float to, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = Properties(renderer, 0, 0f, to, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterialProperties Properties(Renderer renderer, int materialIndex, float to, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = Properties(renderer, materialIndex, 0f, to, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterialProperties Properties(Renderer renderer, float from, float to, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = Properties(renderer, 0, from, to, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterialProperties Properties(Renderer renderer, int materialIndex, float from, float to, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = Create<TweenMaterialProperties>()
                .SetTarget(renderer)
                .SetMaterialIndex(materialIndex)
                .SetStartMaterial(startMaterial)
                .SetEndMaterial(endMaterial)
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenMaterialProperties;
            return tweener;
        }
    }

    public static partial class RendererExtension
    {
        public static TweenMaterialProperties TweenProperties(this Renderer renderer, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = UTween.Properties(renderer, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterialProperties TweenProperties(this Renderer renderer, int materialIndex, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = UTween.Properties(renderer, materialIndex, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterialProperties TweenProperties(this Renderer renderer, float to, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = UTween.Properties(renderer, to, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterialProperties TweenProperties(this Renderer renderer, int materialIndex, float to, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = UTween.Properties(renderer, materialIndex, to, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterialProperties TweenProperties(this Renderer renderer, float from, float to, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = UTween.Properties(renderer, from, to, startMaterial, endMaterial, duration);
            return tweener;
        }

        public static TweenMaterialProperties TweenProperties(this Renderer renderer, int materialIndex, float from, float to, Material startMaterial, Material endMaterial, float duration)
        {
            var tweener = UTween.Properties(renderer, materialIndex, from, to, startMaterial, endMaterial, duration);
            return tweener;
        }
    }

    #endregion

}
