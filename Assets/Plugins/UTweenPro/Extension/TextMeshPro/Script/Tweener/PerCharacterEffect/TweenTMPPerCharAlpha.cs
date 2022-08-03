#if UTWEEN_TEXTMESHPRO
using System;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("TMP Text Per-Char Alpha", "TextMeshPro", UTweenTMP.IconPath)]
    [Serializable]
    public partial class TweenTMPPerCharAlpha : TweenValueFloat<TMP_Text>, ITMPCharacterModifier
    {
        public TMPPerCharEffectData EffectData = new TMPPerCharEffectData();

        public TMP_Text GetTarget => Target;
        public bool ChangeGeometry => false;
        public bool ChangeColor => true;

        public override bool SupportIndependentAxis => false;
        public override bool SupportSetCurrentValue => false;
        public override float Value { get; set; }

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
        }

        public void ModifyColor(int characterIndex, ref Color32[] colors, float progress)
        {
            var startIndex = EffectData.GetStartIndex(characterIndex) * 4;
            var from = FromGetter();
            var to = ToGetter();
            var factor = EffectData.GetFactor(progress, Factor);
            var alpha = Mathf.LerpUnclamped(from, to, factor);
            for (var i = startIndex; i < startIndex + 4; i++)
            {
                Color color = colors[i];
                color.a = alpha;
                colors[i] = color;
            }
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

    public partial class TweenTMPPerCharAlpha : TweenValueFloat<TMP_Text>, ITMPCharacterModifier
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