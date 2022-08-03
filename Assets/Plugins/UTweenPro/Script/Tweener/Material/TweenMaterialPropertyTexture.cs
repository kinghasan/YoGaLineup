using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Material Texture", "Material", "Material Icon")]
    [Serializable]
    public partial class TweenMaterialPropertyTexture : TweenValueVector4<Renderer>
    {
        public TweenMaterialData MaterialData = new TweenMaterialData();

        public override Vector4 Value
        {
            get
            {
                MaterialData.Cache(Target);
                return MaterialData.GetVector();
            }
            set
            {
                MaterialData.Cache(Target);
                MaterialData.SetVector(value);
            }
        }

        public override void Reset()
        {
            base.Reset();
            MaterialData.Reset();
            From = new Vector4(1f, 1f, 0f, 0f);
            To = new Vector4(1f, 1f, 1f, 1f);
#if UNITY_EDITOR
            EnableAxis = true;
#endif
        }
    }

#if UNITY_EDITOR

    public partial class TweenMaterialPropertyTexture : TweenValueVector4<Renderer>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty MaterialDataProperty;

        public override string AxisXName => "TX";
        public override string AxisYName => "TY";
        public override string AxisZName => "OX";
        public override string AxisWName => "OY";

        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            MaterialData.InitEditor(this, MaterialDataProperty);
        }

        public override void DrawIndependentAxis()
        {
            EditorStyle.CharacterWidth *= 1.5f;
            base.DrawIndependentAxis();
            EditorStyle.CharacterWidth /= 1.5f;
        }

        public override void DrawFromToValue()
        {
            EditorStyle.CharacterWidth *= 1.5f;
            base.DrawFromToValue();
            EditorStyle.CharacterWidth /= 1.5f;
        }

        public override void DrawTarget()
        {
            base.DrawTarget();
            MaterialData.DrawMaterialProperty(ShaderUtil.ShaderPropertyType.TexEnv);
        }
    }

#endif

    #region Extension

    public partial class TweenMaterialPropertyTexture : TweenValueVector4<Renderer>
    {
        public TweenMaterialPropertyTexture SetMaterialMode(TweenMaterialMode materialMode)
        {
            MaterialData.Mode = materialMode;
            return this;
        }

        public TweenMaterialPropertyTexture SetMaterialIndex(int materialIndex)
        {
            MaterialData.Index = materialIndex;
            return this;
        }

        public TweenMaterialPropertyTexture SetPropertyName(string propertyName)
        {
            MaterialData.Property = propertyName;
            return this;
        }
    }

    #endregion
}
