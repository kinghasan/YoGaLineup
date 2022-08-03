#if UTWEEN_TEXTMESHPRO
using System;
using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("TMP Text Per-Char Color", "TextMeshPro", UTweenTMP.IconPath)]
    [Serializable]
    public partial class TweenTMPPerCharColor : TweenValueColor<TMP_Text>, ITMPCharacterModifier
    {
        public TMPPerCharEffectData EffectData = new TMPPerCharEffectData();
        public ColorOverlayMode Overlay;

        public TMP_Text GetTarget => Target;
        public bool ChangeGeometry => false;
        public bool ChangeColor => true;

        public override bool SupportIndependentAxis => false;
        public override bool SupportSetCurrentValue => false;
        public override Color Value { get; set; }

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
            var color = ColorMode == ColorMode.FromTo ? Color.Lerp(from, to, factor) : Gradient.Evaluate(factor);
            for (var i = startIndex; i < startIndex + 4; i++)
            {
                if (Overlay == ColorOverlayMode.Multiply)
                {
                    colors[i] *= color;
                }
                else if (Overlay == ColorOverlayMode.Add)
                {
                    colors[i] += color;
                }
                else if (Overlay == ColorOverlayMode.Minus)
                {
                    colors[i] -= color;
                }
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
            Overlay = ColorOverlayMode.Multiply;
            EffectData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenTMPPerCharColor : TweenValueColor<TMP_Text>, ITMPCharacterModifier
    {
        [TweenerProperty, NonSerialized] public SerializedProperty OverlayProperty;

        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            EffectData.InitEditor(this, tweenerProperty);
        }

        public override void DrawBody()
        {
            GUIUtil.DrawToolbarEnum(OverlayProperty, nameof(Overlay), typeof(ColorOverlayMode));
            EffectData.DrawCharacterModifier();
        }
    }

#endif

}
#endif