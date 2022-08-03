using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Material Float", "Material", "Material Icon")]
    [Serializable]
    public partial class TweenMaterialPropertyFloat : TweenValueFloat<Renderer>
    {
        public TweenMaterialData MaterialData = new TweenMaterialData();

        public override float Value
        {
            get
            {
                MaterialData.Cache(Target);
                return MaterialData.GetFloat();
            }
            set
            {
                MaterialData.Cache(Target);
                MaterialData.SetFloat(value);
            }
        }

        public override void Reset()
        {
            base.Reset();
            MaterialData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenMaterialPropertyFloat : TweenValueFloat<Renderer>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty MaterialDataProperty;

        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            MaterialData.InitEditor(this, MaterialDataProperty);
        }

        public override void DrawTarget()
        {
            base.DrawTarget();
            MaterialData.DrawMaterialProperty(ShaderUtil.ShaderPropertyType.Float);
        }
    }

#endif

    #region Extension

    public partial class TweenMaterialPropertyFloat : TweenValueFloat<Renderer>
    {
        public TweenMaterialPropertyFloat SetMaterialMode(TweenMaterialMode materialMode)
        {
            MaterialData.Mode = materialMode;
            return this;
        }

        public TweenMaterialPropertyFloat SetMaterialIndex(int materialIndex)
        {
            MaterialData.Index = materialIndex;
            return this;
        }

        public TweenMaterialPropertyFloat SetPropertyName(string propertyName)
        {
            MaterialData.Property = propertyName;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenMaterialPropertyFloat Float(Renderer renderer, string propertyName, float to, float duration)
        {
            var tweener = Play<TweenMaterialPropertyFloat, Renderer, float>(renderer, to, duration)
                .SetMaterialIndex(0)
                .SetPropertyName(propertyName);
            return tweener;
        }

        public static TweenMaterialPropertyFloat Float(Renderer renderer, int materialIndex, string propertyName, float to, float duration)
        {
            var tweener = Play<TweenMaterialPropertyFloat, Renderer, float>(renderer, to, duration)
                .SetMaterialIndex(materialIndex)
                .SetPropertyName(propertyName);
            return tweener;
        }

        public static TweenMaterialPropertyFloat Float(Renderer renderer, string propertyName, float from, float to, float duration)
        {
            var tweener = Play<TweenMaterialPropertyFloat, Renderer, float>(renderer, from, to, duration)
                .SetMaterialIndex(0)
                .SetPropertyName(propertyName);
            return tweener;
        }

        public static TweenMaterialPropertyFloat Float(Renderer renderer, int materialIndex, string propertyName, float from, float to, float duration)
        {
            var tweener = Play<TweenMaterialPropertyFloat, Renderer, float>(renderer, from, to, duration)
                .SetMaterialIndex(materialIndex)
                .SetPropertyName(propertyName);
            return tweener;
        }
    }

    public static partial class RendererExtension
    {
        public static TweenMaterialPropertyFloat TweenFloat(this Renderer renderer, string propertyName, float to, float duration)
        {
            var tweener = UTween.Float(renderer, propertyName, to, duration);
            return tweener;
        }

        public static TweenMaterialPropertyFloat TweenFloat(this Renderer renderer, int materialIndex, string propertyName, float to, float duration)
        {
            var tweener = UTween.Float(renderer, materialIndex, propertyName, to, duration);
            return tweener;
        }

        public static TweenMaterialPropertyFloat TweenFloat(this Renderer renderer, string propertyName, float from, float to, float duration)
        {
            var tweener = UTween.Float(renderer, propertyName, from, to, duration);
            return tweener;
        }

        public static TweenMaterialPropertyFloat TweenFloat(this Renderer renderer, int materialIndex, string propertyName, float from, float to, float duration)
        {
            var tweener = UTween.Float(renderer, materialIndex, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
