using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [CreateAssetMenu(menuName = "UTween Pro/UTween Animation Asset", fileName = "UTweenAnimation")]
    public partial class UTweenAnimationAsset : ScriptableObject
    {
        public TweenData Data = new TweenData();

        public virtual void Reset()
        {
            Data.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class UTweenAnimationAsset
    {
        [ContextMenu("Expand All Tweener")]
        public void ExpandAllTweener()
        {
            Undo.RegisterCompleteObjectUndo(this, "Expand All Tweener");
            foreach (var tweener in Data.TweenerList)
            {
                tweener.FoldOut = true;
                tweener.SerializedObject.ApplyModifiedProperties();
            }
        }

        [ContextMenu("Narrow All Tweener")]
        public void NarrowAllTweener()
        {
            Undo.RegisterCompleteObjectUndo(this, "Narrow All Tweener");
            foreach (var tweener in Data.TweenerList)
            {
                tweener.FoldOut = false;
                tweener.SerializedObject.ApplyModifiedProperties();
            }
        }

        [ContextMenu("Active All Tweener")]
        public void ActiveAllTweener()
        {
            Undo.RegisterCompleteObjectUndo(this, "Active All Tweener");
            foreach (var tweener in Data.TweenerList)
            {
                tweener.Active = true;
                tweener.SerializedObject.ApplyModifiedProperties();
            }
        }

        [ContextMenu("DeActive All Tweener")]
        public void DeActiveAllTweener()
        {
            Undo.RegisterCompleteObjectUndo(this, "DeActive All Tweener");
            foreach (var tweener in Data.TweenerList)
            {
                tweener.Active = false;
                tweener.SerializedObject.ApplyModifiedProperties();
            }
        }
    }

    [CustomEditor(typeof(UTweenAnimationAsset))]
    [CanEditMultipleObjects]
    public class UTweenAnimationAssetEditor : Editor
    {
        public virtual UTweenAnimationAsset Target => target as UTweenAnimationAsset;
        public UTweenAnimationAsset TweenAnimationAsset => Target;
        public TweenData Data => Target.Data;
        public List<Tweener> TweenerList => Data.TweenerList;

        [NonSerialized] public SerializedProperty TweenParamProperty;
        [NonSerialized] public SerializedProperty TweenerListProperty;

        public virtual void OnEnable()
        {
            InitEditor();
        }

        public virtual void OnDisable()
        {
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            Data.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
        }

        public virtual void InitEditor()
        {
            TweenParamProperty = serializedObject.FindProperty(nameof(Data));
            TweenerListProperty = TweenParamProperty.FindPropertyRelative(nameof(Data.TweenerList));

            Data.InitEditor(TweenEditorMode.ScriptableObject, this);
        }
    }

#endif
}
