using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Text Per-Char Color", "UGUI Text")]
    [Serializable]
    public partial class TweenPerCharColor : TweenValueColor<Text>, ITextCharacterModifier
    {
        public TextPerCharEffectData EffectData = new TextPerCharEffectData();
        public ColorOverlayMode Overlay;

        public Text GetTarget => Target;
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

        public void Modify(int characterIndex, ref UIVertex[] vertices)
        {
            var (index, progress) = EffectData.GetIndexAndProgress(characterIndex);
            var from = FromGetter();
            var to = ToGetter();
            var factor = EffectData.GetFactor(progress, Factor);
            var color = ColorMode == ColorMode.FromTo ? Color.Lerp(from, to, factor) : Gradient.Evaluate(factor);
            for (var i = 0; i < vertices.Length; i++)
            {
                if (Overlay == ColorOverlayMode.Multiply)
                {
                    vertices[i].color *= color;
                }
                else if (Overlay == ColorOverlayMode.Add)
                {
                    vertices[i].color += color;
                }
                else if (Overlay == ColorOverlayMode.Minus)
                {
                    vertices[i].color -= color;
                }
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
            Overlay = ColorOverlayMode.Multiply;
            EffectData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenPerCharColor : TweenValueColor<Text>, ITextCharacterModifier
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