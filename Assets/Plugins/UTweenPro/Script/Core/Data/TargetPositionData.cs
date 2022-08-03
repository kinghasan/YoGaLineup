using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public partial class TargetPositionData
    {
        public TargetPositionMode Mode;
        public Transform Transform;
        public Vector3 Position;

        public Vector3 GetPosition()
        {
            if (Mode == TargetPositionMode.Transform)
            {
                if (Transform == null) return default;
                return Transform.position;
            }

            if (Mode == TargetPositionMode.Position) return Position;
            return default;
        }

        public static implicit operator TargetPositionData(Vector3 value)
        {
            var positionData = new TargetPositionData {Position = value};
            return positionData;
        }

        public static implicit operator Vector3(TargetPositionData data)
        {
            return data.GetPosition();
        }

        public void Reset()
        {
            Mode = TargetPositionMode.Transform;
            Transform = null;
            Position = Vector3.zero;
        }
    }

#if UNITY_EDITOR

    public partial class TargetPositionData
    {
        [NonSerialized] public SerializedProperty DataProperty;

        [TweenerProperty, NonSerialized] public SerializedProperty ModeProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty TransformProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty PositionProperty;

        public void InitEditor(SerializedProperty dataProperty)
        {
            DataProperty = dataProperty;
            TweenerPropertyAttribute.CacheProperty(this, DataProperty);
        }

        public void DrawTargetPosition()
        {
            using (GUIHorizontal.Create())
            {
                GUILayout.Label(DataProperty.displayName, EditorStyles.label, GUILayout.Width(EditorGUIUtility.labelWidth));

                if (Mode == TargetPositionMode.Transform)
                {
                    using (GUIErrorColorArea.Create(Transform == null))
                    {
                        EditorGUILayout.ObjectField(TransformProperty, GUIContent.none);
                    }
                }
                else if (Mode == TargetPositionMode.Position)
                {
                    PositionProperty.vector3Value = EditorGUILayout.Vector3Field(GUIContent.none, PositionProperty.vector3Value);
                }

                GUIUtil.DrawSelectEnumButton(ModeProperty, typeof(TargetPositionMode));
            }
        }
    }

#endif
}
