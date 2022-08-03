using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public partial class TweenMaterialData
    {
        public TweenMaterialMode Mode;
        public int Index;
        public string Property;

        [NonSerialized] public Renderer Renderer;
        [NonSerialized] public MaterialPropertyBlock Block;

        public void Cache(Renderer renderer)
        {
            if (Renderer == renderer) return;
            Renderer = renderer;
            if (Mode != TweenMaterialMode.Property) return;
            if (Block == null) Block = new MaterialPropertyBlock();
            if (Index >= 0) Renderer.GetPropertyBlock(Block, Index);
        }

        #region Color

        public void SetColor(Color value)
        {
            if (Renderer == null) return;
            if (Index < 0) return;
            switch (Mode)
            {
                case TweenMaterialMode.Property:
                    Block.SetColor(Property, value);
                    Renderer.SetPropertyBlock(Block, Index);
                    break;
                case TweenMaterialMode.Instance:
                    Renderer.materials[Index].SetColor(Property, value);
                    break;
                case TweenMaterialMode.Shared:
                    Renderer.sharedMaterials[Index].SetColor(Property, value);
                    break;
            }
        }

        public Color GetColor()
        {
            if (Renderer == null) return default;
            if (Index < 0) return default;
            switch (Mode)
            {
                case TweenMaterialMode.Property:
                    // return Block.GetColor(Property);
                case TweenMaterialMode.Instance:
                    return Renderer.materials[Index].GetColor(Property);
                case TweenMaterialMode.Shared:
                    return Renderer.sharedMaterials[Index].GetColor(Property);
            }

            return default;
        }

        #endregion

        #region Float

        public void SetFloat(float value)
        {
            if (Renderer == null) return;
            if (Index < 0) return;
            switch (Mode)
            {
                case TweenMaterialMode.Property:
                    Block.SetFloat(Property, value);
                    Renderer.SetPropertyBlock(Block, Index);
                    break;
                case TweenMaterialMode.Instance:
                    Renderer.materials[Index].SetFloat(Property, value);
                    break;
                case TweenMaterialMode.Shared:
                    Renderer.sharedMaterials[Index].SetFloat(Property, value);
                    break;
            }
        }

        public float GetFloat()
        {
            if (Renderer == null) return default;
            if (Index < 0) return default;
            switch (Mode)
            {
                case TweenMaterialMode.Property:
                    // return Block.GetFloat(Property);
                case TweenMaterialMode.Instance:
                    return Renderer.materials[Index].GetFloat(Property);
                case TweenMaterialMode.Shared:
                    return Renderer.sharedMaterials[Index].GetFloat(Property);
            }

            return default;
        }

        #endregion

        #region Vector

        public void SetVector(Vector4 value)
        {
            if (Renderer == null) return;
            if (Index < 0) return;
            switch (Mode)
            {
                case TweenMaterialMode.Property:
                    Block.SetVector(Property, value);
                    Renderer.SetPropertyBlock(Block, Index);
                    break;
                case TweenMaterialMode.Instance:
                    Renderer.materials[Index].SetVector(Property, value);
                    break;
                case TweenMaterialMode.Shared:
                    Renderer.sharedMaterials[Index].SetVector(Property, value);
                    break;
            }
        }

        public Vector4 GetVector()
        {
            if (Renderer == null) return default;
            if (Index < 0) return default;
            switch (Mode)
            {
                case TweenMaterialMode.Property:
                    // return Block.GetVector(Property);
                case TweenMaterialMode.Instance:
                    return Renderer.materials[Index].GetVector(Property);
                case TweenMaterialMode.Shared:
                    return Renderer.sharedMaterials[Index].GetVector(Property);
            }

            return default;
        }

        #endregion

        public void Reset()
        {
            Mode = TweenMaterialMode.Property;
            Index = -1;
            Property = "";
            if (Block != null) Block.Clear();
        }
    }

#if UNITY_EDITOR

    public partial class TweenMaterialData
    {
        [NonSerialized] public Tweener<Renderer> Tweener;
        [NonSerialized] public SerializedProperty DataProperty;

        [TweenerProperty, NonSerialized] public SerializedProperty ModeProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty IndexProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty PropertyProperty;

        public void InitEditor(Tweener<Renderer> tweener, SerializedProperty dataProperty)
        {
            Tweener = tweener;
            DataProperty = dataProperty;
            TweenerPropertyAttribute.CacheProperty(this, DataProperty);
        }

        public void DrawMaterialProperty(ShaderUtil.ShaderPropertyType propertyType)
        {
            GUIUtil.DrawToolbarEnum(ModeProperty, typeof(TweenMaterialMode));
            if (Tweener.Data.Mode == TweenEditorMode.Component)
            {
                if (Tweener.Target == null)
                {
                    if (IndexProperty.intValue >= 0) IndexProperty.intValue = -1;
                    if (!string.IsNullOrEmpty(PropertyProperty.stringValue)) PropertyProperty.stringValue = "";
                    return;
                }

                GUIMenu.SelectMaterialMenu(Tweener.Target, "Material", IndexProperty);
                GUIMenu.SelectMaterialShaderMenu(Tweener.Target, nameof(Property), IndexProperty.intValue, PropertyProperty, propertyType);
            }
            else
            {
                using (GUIHorizontal.Create())
                {
                    EditorGUILayout.PropertyField(PropertyProperty, new GUIContent("Material"));
                    EditorGUILayout.PropertyField(PropertyProperty, new GUIContent(nameof(Property)));
                }
            }
        }
    }

#endif
}