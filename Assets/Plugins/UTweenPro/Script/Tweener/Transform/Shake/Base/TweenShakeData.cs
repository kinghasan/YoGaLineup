using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public partial class TweenShakeData
    {
        public ShakeMode Mode;
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
        public int Count;
        public AnimationCurve Curve;

        public void Reset()
        {
            Mode = ShakeMode.Random;
            Position = Vector3.one;
            Rotation = Vector3.zero;
            Scale = Vector3.zero;
            Count = 5;
            var defaultSlope = 5f;
            Curve = new AnimationCurve(
                new Keyframe(0f, 0f, defaultSlope, defaultSlope), 
                new Keyframe(0.25f, 1f, 0f, 0f), 
                new Keyframe(0.5f, 0f, -defaultSlope, -defaultSlope),
                new Keyframe(0.75f, -1f, 0f, 0f),
                new Keyframe(1f, 0f, defaultSlope, defaultSlope));
        }
    }

#if UNITY_EDITOR

    public partial class TweenShakeData
    {
        [NonSerialized] public TweenShake Tweener;
        [NonSerialized] public SerializedProperty DataProperty;

        [TweenerProperty, NonSerialized] public SerializedProperty ModeProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty PositionProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty RotationProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty ScaleProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty CountProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty CurveProperty;

        public void InitEditor(TweenShake tweener, SerializedProperty dataProperty)
        {
            Tweener = tweener;
            DataProperty = dataProperty;
            TweenerPropertyAttribute.CacheProperty(this, DataProperty);
        }

        public void DrawShakeData()
        {
            GUIUtil.DrawToolbarEnum(ModeProperty, nameof(Mode), typeof(ShakeMode));
            var labelWidth = Tweener.EnableAxis ? EditorGUIUtility.labelWidth - EditorStyle.CharacterWidth : EditorGUIUtility.labelWidth;
            using (GUILabelWidthArea.Create(labelWidth))
            {
                using (GUIHorizontal.Create())
                {
                    if (Tweener.EnableAxis)
                    {
                        Tweener.AxisX = GUILayout.Toggle(Tweener.AxisX, "", GUILayout.Width(EditorStyle.CharacterWidth));
                    }
                    
                    using (GUIEnableArea.Create(Tweener.AxisX))
                    {
                        EditorGUILayout.PropertyField(PositionProperty, new GUIContent("Pos"));
                    }
                }

                using (GUIHorizontal.Create())
                {
                    if (Tweener.EnableAxis)
                    {
                        Tweener.AxisY = GUILayout.Toggle(Tweener.AxisY, "", GUILayout.Width(EditorStyle.CharacterWidth));
                    }

                    using (GUIEnableArea.Create(Tweener.AxisY))
                    {
                        EditorGUILayout.PropertyField(RotationProperty, new GUIContent("Rot"));
                    }
                }

                using (GUIHorizontal.Create())
                {
                    if (Tweener.EnableAxis)
                    {
                        Tweener.AxisZ = GUILayout.Toggle(Tweener.AxisZ, "", GUILayout.Width(EditorStyle.CharacterWidth));
                    }

                    using (GUIEnableArea.Create(Tweener.AxisZ))
                    {
                        EditorGUILayout.PropertyField(ScaleProperty, new GUIContent("Scale"));
                    }
                }
            }

            using (GUIHorizontal.Create())
            {
                EditorGUILayout.PropertyField(CountProperty);
                EditorGUILayout.PropertyField(CurveProperty, new GUIContent("Shake"));
            }
        }
    }

#endif
}