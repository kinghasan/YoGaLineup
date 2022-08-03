using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Text Per-Char Alpha", "UGUI Text")]
    [Serializable]
    public partial class TweenPerCharAlpha : TweenValueFloat<Text>, ITextCharacterModifier
    {
        public TextPerCharEffectData EffectData = new TextPerCharEffectData();

        public Text GetTarget => Target;
        public override bool SupportIndependentAxis => false;
        public override bool SupportSetCurrentValue => false;
        public override float Value { get; set; }

        public override void PreSample()
        {
            base.PreSample();
            EffectData.Cache(Data, Target, this);
        }

        public override void Sample(float factor)
        {
        }

        public void Modify(int characterIndex, ref UIVertex[] vertices)
        {
            var (index, progress) = EffectData.GetIndexAndProgress(characterIndex);
            var from = FromGetter();
            var to = ToGetter();
            var factor = EffectData.GetFactor(progress, Factor);
            var alpha = Mathf.LerpUnclamped(from, to, factor);
            for (var i = 0; i < vertices.Length; i++)
            {
                Color color = vertices[i].color;
                color.a = alpha;
                vertices[i].color = color;
            }
        }

        public override void SetDirty()
        {
            base.SetDirty();
            EffectData.SetDirty();
        }

        public override void OnRemoved()
        {
            base.OnRemoved();
            EffectData.Remove(Data, Target, this);
        }

        public override void Reset()
        {
            base.Reset();
            EffectData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenPerCharAlpha : TweenValueFloat<Text>, ITextCharacterModifier
    {
        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            EffectData.InitEditor(this, tweenerProperty);
        }

        public override void DrawBody()
        {
            EffectData.DrawCharacterModifier();
        }
    }

#endif

}