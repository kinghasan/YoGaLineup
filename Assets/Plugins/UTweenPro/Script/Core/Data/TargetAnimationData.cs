using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public partial class TargetAnimationData
    {
        public TargetAnimationMode Mode;
        public UTweenAnimation Component;
        public UTweenAnimationAsset Asset;

        public TweenData Data
        {
            get
            {
                if (Mode == TargetAnimationMode.Component) return Component?.Data;
                if (Mode == TargetAnimationMode.Asset) return Asset?.Data;
                return default;
            }
        }

        public virtual void Reset()
        {
            Mode = TargetAnimationMode.Component;
            Component = null;
            Asset = null;
        }
    }

#if UNITY_EDITOR

    public partial class TargetAnimationData
    {
        [NonSerialized] public SerializedProperty DataProperty;

        [TweenerProperty, NonSerialized] public SerializedProperty ModeProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty ComponentProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty AssetProperty;

        public void InitEditor(SerializedProperty dataProperty)
        {
            DataProperty = dataProperty;
            TweenerPropertyAttribute.CacheProperty(this, DataProperty);
        }

        public void DrawAnimationData()
        {
            using (GUIHorizontal.Create())
            {
                GUILayout.Label(DataProperty.displayName, EditorStyles.label, GUILayout.Width(EditorGUIUtility.labelWidth));
                if (Mode == TargetAnimationMode.Component)
                {
                    EditorGUILayout.ObjectField(ComponentProperty, GUIContent.none, GUILayout.MinWidth(0));
                }
                else if (Mode == TargetAnimationMode.Asset)
                {
                    EditorGUILayout.ObjectField(AssetProperty, GUIContent.none, GUILayout.MinWidth(0));
                }

                GUIUtil.DrawSelectEnumButton(ModeProperty, typeof(TargetAnimationMode));
            }
        }
    }

#endif
}
