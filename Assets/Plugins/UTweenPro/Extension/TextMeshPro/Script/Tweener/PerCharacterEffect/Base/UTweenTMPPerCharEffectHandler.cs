#if UTWEEN_TEXTMESHPRO
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Aya.TweenPro
{
    [RequireComponent(typeof(TMP_Text))]
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    [AddComponentMenu("UTween Pro/UTween TMP Per-Char Effect Handler")]
    public class UTweenTMPPerCharEffectHandler : MonoBehaviour
    {
        [NonSerialized] public List<ITMPCharacterModifier> Modifiers = new List<ITMPCharacterModifier>();

        public TweenData TweenData { get; set; }

        public TMP_Text Text
        {
            get
            {
                if (_text == null) _text = GetComponent<TMP_Text>();
                return _text;
            }
        }

        private TMP_Text _text;

        public bool ChangeGeometry { get; set; }
        public bool ChangeColor { get; set; }

        public int Length
        {
            get
            {
                if (Text == null) return 0;
                if (Text.textInfo.meshInfo.Length == 0) return 0;
                var mesh = Text.textInfo.meshInfo[0];
                var vertices = mesh.vertices;
                if (vertices == null) return 0;
                var length = vertices.Length / 4;
                return length;
            }
        }

        [NonSerialized] public List<int> CharacterIndexList = new List<int>();

        public void SyncModifiers(TweenData tweenData)
        {
            TweenData = tweenData;
            Modifiers.Clear();
            foreach (var tweener in tweenData.TweenerList)
            {
                if (!tweener.Active) continue;
                if (tweener is ITMPCharacterModifier modifier)
                {
                    if (modifier.GetTarget != Text) continue;
                    Modifiers.Add(modifier);
                }
            }
        }

        public void Update()
        {
            if (Text == null) return;
            if (Length != CharacterIndexList.Count) return;

            Text.ForceMeshUpdate(true);
            ChangeGeometry = false;
            ChangeColor = false;

            var meshCount = Text.textInfo.meshInfo.Length;
            for (var meshIndex = 0; meshIndex < meshCount; meshIndex++)
            {
                var vectorLength = Text.textInfo.meshInfo[meshIndex].vertices.Length;
                var index = 0;
                for (var i = 0; i < vectorLength; i += 4)
                {
                    var progress = i * 1f / vectorLength;
                    try
                    {
                        var characterIndex = index;
                        foreach (var modifier in Modifiers)
                        {
                            if (modifier.ChangeGeometry)
                            {
                                modifier.ModifyGeometry(characterIndex, ref Text.textInfo.meshInfo[meshIndex].vertices, progress);
                                ChangeGeometry = true;
                            }

                            if (modifier.ChangeColor)
                            {
                                modifier.ModifyColor(characterIndex, ref Text.textInfo.meshInfo[meshIndex].colors32, progress);
                                ChangeColor = true;
                            }
                        }
                    }
                    catch
                    {
                        //
                    }

                    index++;
                }
            }

            if (ChangeGeometry)
            {
                for (var i = 0; i < Text.textInfo.meshInfo.Length; i++)
                {
                    Text.textInfo.meshInfo[i].mesh.vertices = Text.textInfo.meshInfo[i].vertices;
                    Text.UpdateGeometry(Text.textInfo.meshInfo[i].mesh, i);
                }

                ChangeGeometry = false;
            }

            if (ChangeColor)
            {
                Text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                ChangeColor = false;
            }
        }
    }
}

#endif