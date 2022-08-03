using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Aya.TweenPro
{
    [RequireComponent(typeof(Text))]
    [DisallowMultipleComponent]
    [AddComponentMenu("UTween Pro/UTween Per-Char Effect Handler")]
    public partial class UTweenPerCharEffectHandler : BaseMeshEffect
    {
        [NonSerialized] public List<ITextCharacterModifier> Modifiers = new List<ITextCharacterModifier>();

        public Text Text
        {
            get
            {
                if (_target == null) _target = GetComponent<Text>();
                return _target;
            }
        }

        private Text _target;

        public int Length { get; set; }

        [NonSerialized] public List<int> CharacterIndexList = new List<int>();

        public void SyncModifiers(TweenData tween)
        {
            Modifiers.Clear();
            foreach (var tweener in tween.TweenerList)
            {
                if (!tweener.Active) continue;
                if (tweener is ITextCharacterModifier modifier)
                {
                    if (Text != modifier.GetTarget) continue;
                    Modifiers.Add(modifier);
                }
            }
        }

        public override void ModifyMesh(VertexHelper vh)
        {
            var vertexCount = vh.currentVertCount;
            if (!IsActive() || vertexCount == 0)
            {
                vh.Clear();
                return;
            } 

            var characterIndex = 0;
            var characterLength = vertexCount / 4;
            if (characterLength != Length)
            {
                Length = characterLength;
            }

            for (var i = 0; i < vertexCount; i += 4)
            {
                // var progress = i * 1f / vertexCount;
                var vertexes = new UIVertex[4];
                for (var j = 0; j < 4; j++)
                {
                    var uiVertex = new UIVertex();
                    vertexes[j] = uiVertex;
                    vh.PopulateUIVertex(ref vertexes[j], i + j);
                }

                try
                {
                    foreach (var modifier in Modifiers)
                    {
                        modifier.Modify(characterIndex, ref vertexes);
                    }
                }
                catch
                {
                    //
                }

                for (var j = 0; j < 4; j++)
                {
                    vh.SetUIVertex(vertexes[j], i + j);
                }

                characterIndex++;
            }
        }

        public void SetDirty()
        {
            graphic.SetAllDirty();
        }
    }
}
