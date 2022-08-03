using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Text Per-Char Glitch", "UGUI Text")]
    [Serializable]
    public partial class TweenPerCharGlitch : TweenValueVector3<Text>, ITextCharacterModifier
    {
        public TextPerCharEffectData EffectData = new TextPerCharEffectData();

        public Text GetTarget => Target;
        public override bool SupportIndependentAxis => false;
        public override bool SupportSetCurrentValue => false;
        public override Vector3 Value { get; set; }

        public override void PreSample()
        {
            base.PreSample();
            EffectData.Cache(((Tweener)this).Data, Target, this);
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
            var power = Vector3.LerpUnclamped(from, to, factor);
            var glitchX = Random.Range(-power.x, power.x);
            var glitchY = Random.Range(-power.y, power.y);
            var glitchZ = Random.Range(-power.z, power.z);
            var glitch = new Vector3(glitchX, glitchY, glitchZ);
            for (var i = 0; i < vertices.Length; i++)
            {
                vertices[i].position += glitch;
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
            EffectData.Remove(((Tweener)this).Data, Target, this);
        }

        public override void Reset()
        {
            base.Reset();
            EffectData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenPerCharGlitch : TweenValueVector3<Text>, ITextCharacterModifier
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