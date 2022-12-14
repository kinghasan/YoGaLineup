using System;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Text Per-Char Rotation", "UGUI Text")]
    [Serializable]
    public partial class TweenPerCharRotation : TweenValueVector3<Text>, ITextCharacterModifier
    {
        public TextPerCharEffectData EffectData = new TextPerCharEffectData();
        public CharacterSpaceMode CharacterSpace;

        public Text GetTarget => Target;
        public override bool SupportIndependentAxis => false;
        public override bool SupportSetCurrentValue => false;
        public override Vector3 Value { get; set; }

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
            var rotation = Vector3.LerpUnclamped(from, to, factor);

            if (CharacterSpace == CharacterSpaceMode.Character)
            {
                var center = Vector3.zero;
                for (var i = 0; i < vertices.Length; i++)
                {
                    center += vertices[i].position;
                }

                center /= vertices.Length;
                for (var i = 0; i < vertices.Length; i++)
                {
                    vertices[i].position = vertices[i].position.Rotate(center, rotation);
                }
            }
            else if (CharacterSpace == CharacterSpaceMode.Text)
            {
                for (var i = 0; i < vertices.Length; i++)
                {
                    vertices[i].position = vertices[i].position.Rotate(Vector3.zero, rotation);
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
            EffectData.Remove(Data, Target, this);
        }

        public override void Reset()
        {
            base.Reset();
            EffectData.Reset();
            CharacterSpace = CharacterSpaceMode.Character;
        }
    }

#if UNITY_EDITOR

    public partial class TweenPerCharRotation : TweenValueVector3<Text>, ITextCharacterModifier
    {
        [TweenerProperty, NonSerialized] public SerializedProperty CharacterSpaceProperty;

        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            EffectData.InitEditor(this, tweenerProperty);
        }

        public override void DrawBody()
        {
            EffectData.DrawCharacterModifier();
        }

        public override void DrawAppend()
        {
            base.DrawAppend();
            GUIUtil.DrawToolbarEnum(CharacterSpaceProperty, nameof(Space), typeof(CharacterSpaceMode));
        }
    }

#endif

}