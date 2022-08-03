using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Material Alpha", "Material", "Material Icon")]
    [Serializable]
    public partial class TweenMaterialPropertyAlpha : TweenValueFloat<Renderer>
    {
        public TweenMaterialData MaterialData = new TweenMaterialData();

        public override float Value
        {
            get
            {
                MaterialData.Cache(Target);
                return MaterialData.GetColor().a;
            }
            set
            {
                MaterialData.Cache(Target);
                var color = MaterialData.GetColor();
                color.a = value;
                MaterialData.SetColor(color);
            }
        }

        public override void Reset()
        {
            base.Reset();
            MaterialData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenMaterialPropertyAlpha : TweenValueFloat<Renderer>
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
            MaterialData.DrawMaterialProperty(ShaderUtil.ShaderPropertyType.Color);
        }
    }

#endif

    #region Extension

    public partial class TweenMaterialPropertyAlpha : TweenValueFloat<Renderer>
    {
        public TweenMaterialPropertyAlpha SetMaterialMode(TweenMaterialMode materialMode)
        {
            MaterialData.Mode = materialMode;
            return this;
        }

        public TweenMaterialPropertyAlpha SetMaterialIndex(int materialIndex)
        {
            MaterialData.Index = materialIndex;
            return this;
        }

        public TweenMaterialPropertyAlpha SetPropertyName(string propertyName)
        {
            MaterialData.Property = propertyName;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenMaterialPropertyAlpha Alpha(Renderer renderer, string propertyName, float to, float duration)
        {
            var tweener = Play<TweenMaterialPropertyAlpha, Renderer, float>(renderer, to, duration)
                .SetMaterialIndex(0)
                .SetPropertyName(propertyName);
            return tweener;
        }

        public static TweenMaterialPropertyAlpha Alpha(Renderer renderer, int materialIndex, string propertyName, float to, float duration)
        {
            var tweener = Play<TweenMaterialPropertyAlpha, Renderer, float>(renderer, to, duration)
                .SetMaterialIndex(materialIndex)
                .SetPropertyName(propertyName);
            return tweener;
        }

        public static TweenMaterialPropertyAlpha Alpha(Renderer renderer, string propertyName, float from, float to, float duration)
        {
            var tweener = Play<TweenMaterialPropertyAlpha, Renderer, float>(renderer, from, to, duration)
                .SetMaterialIndex(0)
                .SetPropertyName(propertyName);
            return tweener;
        }

        public static TweenMaterialPropertyAlpha Alpha(Renderer renderer, int materialIndex, string propertyName, float from, float to, float duration)
        {
            var tweener = Play<TweenMaterialPropertyAlpha, Renderer, float>(renderer, from, to, duration)
                .SetMaterialIndex(materialIndex)
                .SetPropertyName(propertyName);
            return tweener;
        }
    }

    public static partial class RendererExtension
    {
        public static TweenMaterialPropertyAlpha TweenAlpha(this Renderer renderer, string propertyName, float to, float duration)
        {
            var tweener = UTween.Alpha(renderer, propertyName, to, duration);
            return tweener;
        }

        public static TweenMaterialPropertyAlpha TweenAlpha(this Renderer renderer, int materialIndex, string propertyName, float to, float duration)
        {
            var tweener = UTween.Alpha(renderer, materialIndex, propertyName, to, duration);
            return tweener;
        }

        public static TweenMaterialPropertyAlpha TweenAlpha(this Renderer renderer, string propertyName, float from, float to, float duration)
        {
            var tweener = UTween.Alpha(renderer, propertyName, from, to, duration);
            return tweener;
        }

        public static TweenMaterialPropertyAlpha TweenAlpha(this Renderer renderer, int materialIndex, string propertyName, float from, float to, float duration)
        {
            var tweener = UTween.Alpha(renderer, materialIndex, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
