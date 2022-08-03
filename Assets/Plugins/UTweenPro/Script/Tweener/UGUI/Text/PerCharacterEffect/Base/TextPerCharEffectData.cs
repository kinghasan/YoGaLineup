using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public partial class TextPerCharEffectData
    {
        public TextRangeMode RangeMode;
        public float RangeValue;
        public TextOrderMode OrderMode;
        public AnimationCurve Curve;

        [NonSerialized] public int Length;
        [NonSerialized] public float RuntimeRangeValue;
        [NonSerialized] public Text Text;
        [NonSerialized] public UTweenPerCharEffectHandler Effect;
        [NonSerialized] public List<int> CharacterIndexList = new List<int>();

        public bool NeedUpdateCharacterIndexList
        {
            get
            {
                if (OrderMode == TextOrderMode.Normal) return false;
                if (OrderMode == TextOrderMode.UniformRandom) return Effect.Length != Effect.CharacterIndexList.Count;
                if (OrderMode == TextOrderMode.SelfRandom) return Effect.Length != CharacterIndexList.Count;
                return false;
            }
        }

        public void Cache(TweenData data, Text text, ITextCharacterModifier modifier)
        {
            if (text == null) return;
            Text = text;
            CacheRuntimeRangeValue();
            if (Effect == null) Effect = text.GetComponent<UTweenPerCharEffectHandler>();
            if (Effect == null) Effect = text.gameObject.AddComponent<UTweenPerCharEffectHandler>();
            var length = Effect.Length;
            if (Length != length || Application.isPlaying)
            {
                Length = length;
                CacheRuntimeRangeValue();
                CacheOrderList();
            }

            Effect.SyncModifiers(data);
        }

        public void CacheRuntimeRangeValue()
        {
            if (Text == null) return;
            if (RangeMode == TextRangeMode.Length)
            {
                RuntimeRangeValue = Mathf.Clamp01(RangeValue / Length);
            }

            if (RangeMode == TextRangeMode.Percent)
            {
                RuntimeRangeValue = RangeValue;
            }
        }

        public void CacheOrderList()
        {
            Effect.CharacterIndexList.Clear();
            CharacterIndexList.Clear();
            for (var i = 0; i < Length; i++)
            {
                Effect.CharacterIndexList.Insert(Random.Range(0, Effect.CharacterIndexList.Count + 1), i);
            }

            if (OrderMode == TextOrderMode.SelfRandom)
            {
                for (var i = 0; i < Length; i++)
                {
                    CharacterIndexList.Insert(Random.Range(0, CharacterIndexList.Count + 1), i);
                }
            }
        }

        public (int, float) GetIndexAndProgress(int handlerCharacterIndex)
        {
            if (NeedUpdateCharacterIndexList)
            {
                CacheOrderList();
            }

            var index = -1;
            if (OrderMode == TextOrderMode.Normal) index = handlerCharacterIndex;
            else if (OrderMode == TextOrderMode.UniformRandom) index = Effect.CharacterIndexList[handlerCharacterIndex];
            else if(OrderMode == TextOrderMode.SelfRandom) index = CharacterIndexList[handlerCharacterIndex];
            var progress = index * 1f / (Length - 1);
            return (index, progress);
        }

        public void Remove(TweenData data, Text text, ITextCharacterModifier modifier)
        {
            if (text == null) return;
            if (Effect == null) Effect = text.GetComponent<UTweenPerCharEffectHandler>();
            if (Effect == null) return;
            Effect.SyncModifiers(data);
            if (Effect.Modifiers.Count == 0)
            {
                if (Application.isPlaying)
                {
                    UnityEngine.Object.Destroy(Effect);
                }
                else
                {
                    UnityEngine.Object.DestroyImmediate(Effect);
                }
            }
        }

        public float GetFactor(float progress, float normalizedTime)
        {
            var from = progress * (1f - RuntimeRangeValue);
            var to = from + RuntimeRangeValue;
            if (normalizedTime <= from) return Curve.Evaluate(0f);
            if (normalizedTime >= to) return Curve.Evaluate(1f);
            var factor = (normalizedTime - from) / RuntimeRangeValue;
            factor = Curve.Evaluate(factor);
            return factor;
        }

        public void SetDirty()
        {
            Effect?.SetDirty();
        }

        public void Reset()
        {
            RangeMode = TextRangeMode.Length;
            RangeValue = 1;
            OrderMode = TextOrderMode.Normal;
            Curve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));
        }
    }

#if UNITY_EDITOR

    public partial class TextPerCharEffectData
    {
        [NonSerialized] public Tweener Tweener;
        [NonSerialized] public SerializedProperty TweenerProperty;
        [NonSerialized] public SerializedProperty EffectDataProperty;

        [TweenerProperty, NonSerialized] public SerializedProperty RangeModeProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty RangeValueProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty OrderModeProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty CurveProperty;

        public void InitEditor(Tweener tweener, SerializedProperty tweenerProperty)
        {
            Tweener = tweener;
            TweenerProperty = tweenerProperty;
            EffectDataProperty = TweenerProperty.FindPropertyRelative("EffectData");

            TweenerPropertyAttribute.CacheProperty(this, EffectDataProperty);
        }

        public void DrawCharacterModifier()
        {
            using (GUIHorizontal.Create())
            {
                using (var check = GUICheckChangeArea.Create())
                {
                    EditorGUILayout.PropertyField(RangeModeProperty, new GUIContent("Range"));

                    if (RangeMode == TextRangeMode.Length)
                    {
                        RangeValueProperty.floatValue = EditorGUILayout.FloatField(nameof(TextRangeMode.Length), RangeValueProperty.floatValue);
                        RangeValueProperty.floatValue = (int)RangeValueProperty.floatValue;
                        if (RangeValueProperty.floatValue < 1f) RangeValueProperty.floatValue = 1f;
                    }
                    else if (RangeMode == TextRangeMode.Percent)
                    {
                        RangeValueProperty.floatValue = EditorGUILayout.FloatField(nameof(TextRangeMode.Percent), RangeValueProperty.floatValue);
                        RangeValueProperty.floatValue = Mathf.Clamp(RangeValueProperty.floatValue, 0.01f, 1f);
                    }

                    if (check.Changed)
                    {
                        CacheRuntimeRangeValue();
                    }
                }
            }

            using (GUIHorizontal.Create())
            {
                EditorGUILayout.PropertyField(OrderModeProperty, new GUIContent("Order"));
                EditorGUILayout.PropertyField(CurveProperty, new GUIContent(nameof(Effect)));
            }
        }
    }

#endif

}