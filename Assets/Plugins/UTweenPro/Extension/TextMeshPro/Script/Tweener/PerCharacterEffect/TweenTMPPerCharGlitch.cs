#if UTWEEN_TEXTMESHPRO
using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("TMP Text Per-Char Glitch", "TextMeshPro", UTweenTMP.IconPath)]
    [Serializable]
    public partial class TweenTMPPerCharGlitch : TweenValueVector3<TMP_Text>, ITMPCharacterModifier
    {
        public TMPPerCharEffectData EffectData = new TMPPerCharEffectData();

        public TMP_Text GetTarget => Target;
        public bool ChangeGeometry => true;
        public bool ChangeColor => false;

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

        public void ModifyGeometry(int characterIndex, ref Vector3[] vertices, float progress)
        {
            var startIndex = EffectData.GetStartIndex(characterIndex) * 4;
            var from = FromGetter();
            var to = ToGetter();
            var factor = EffectData.GetFactor(progress, Factor);
            var power = Vector3.LerpUnclamped(from, to, factor);
            var glitchX = Random.Range(-power.x, power.x);
            var glitchY = Random.Range(-power.y, power.y);
            var glitchZ = Random.Range(-power.z, power.z);
            var glitch = new Vector3(glitchX, glitchY, glitchZ);
            for (var i = startIndex; i < startIndex + 4; i++)
            {
                vertices[i] += glitch;
            }
        }

        public void ModifyColor(int characterIndex, ref Color32[] colors, float progress)
        {
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

    public partial class TweenTMPPerCharGlitch : TweenValueVector3<TMP_Text>, ITMPCharacterModifier
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
#endif