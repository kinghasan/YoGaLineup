using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [ExecuteInEditMode]
    [AddComponentMenu("UTween Pro/UTween Animation")]
    public partial class UTweenAnimation : MonoBehaviour
    {
        public TweenData Data = new TweenData();

        public virtual void Awake()
        {
            Data.TweenAnimation = this;
            Data.ControlMode = TweenControlMode.Component;
            if (Application.isPlaying) Data.Awake();
        }

        public virtual void OnEnable()
        {
            if (Application.isPlaying) Data.OnEnable();
        }

        public virtual void Start()
        {
            if (Application.isPlaying) Data.Start();
        }

        public virtual void OnDisable()
        {
            if (Application.isPlaying) Data.OnDisable();
        }

        public virtual void Reset()
        {
            Data.Reset();
        }

#if UNITY_EDITOR
        public virtual void OnDrawGizmos()
        {
            if (Selection.activeObject != gameObject) return;
            Data.OnDrawGizmos();
        }
#endif
    }

#if UNITY_EDITOR

    public partial class UTweenAnimation
    {
        internal Action RefreshEditorAction { get; set; } = delegate { };

        #region Editor Preview

        internal double LastTimeSinceStartup = -1f;

        internal void PreviewStart()
        {
            LastTimeSinceStartup = -1f;
            EditorApplication.update += EditorUpdate;
        }

        internal void PreviewEnd()
        {
            EditorApplication.update -= EditorUpdate;
        }

        internal void EditorUpdate()
        {
            var currentTime = EditorApplication.timeSinceStartup;
            if (LastTimeSinceStartup < 0f)
            {
                LastTimeSinceStartup = currentTime;
            }

            var deltaTime = (float)(currentTime - LastTimeSinceStartup);
            LastTimeSinceStartup = currentTime;

            var smoothDeltaTime = deltaTime;
            var scaledDeltaTime = deltaTime;
            var unscaledDeltaTime = deltaTime;

            EditorUpdateImpl(scaledDeltaTime, unscaledDeltaTime, smoothDeltaTime);
        }

        internal void EditorUpdateImpl(float scaledDeltaTime, float unscaledDeltaTime, float smoothDeltaTime)
        {
            Data.UpdateInternal(scaledDeltaTime, unscaledDeltaTime, smoothDeltaTime);
        }

        #endregion

        #region Context Menu
       
        [ContextMenu("Expand All Tweener")]
        public void ExpandAllTweener()
        {
            Undo.RegisterCompleteObjectUndo(this, "Expand All Tweener");
            foreach (var tweener in Data.TweenerList)
            {
                tweener.FoldOut= true;
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

        [ContextMenu("Export Assets...", false, 10000)]
        public void ExportAssets()
        {
            var path = EditorUtility.SaveFilePanel("Export param to UTween Animation Asset", Application.dataPath, "UTween", "asset");
            path = path.Remove(0, path.IndexOf("Assets", StringComparison.Ordinal));
            var asset = ScriptableObject.CreateInstance<UTweenAnimationAsset>();
            asset.Data = Data;
            var saveAsset = Instantiate(asset);
            DestroyImmediate(asset);
            AssetDatabase.CreateAsset(saveAsset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [ContextMenu("Import Assets...", false, 10001)]
        public void ImportAssets()
        {
            var path = EditorUtility.OpenFilePanel("Select a UTween Animation Asset", Application.dataPath, "asset");
            path = path.Remove(0, path.IndexOf("Assets", StringComparison.Ordinal));
            var asset = (UTweenAnimationAsset)AssetDatabase.LoadAssetAtPath(path, typeof(UTweenAnimationAsset));
            if (asset == null) return;

            var loadAsset = Instantiate(asset);
            Undo.RegisterCompleteObjectUndo(this, "Import UTween Animation Asset");
            Data = loadAsset.Data;
            EditorUtility.SetDirty(gameObject);
            RefreshEditorAction();
        }

        #endregion

        #region Hierarctry Menu

        [MenuItem("GameObject/Create UTween Animation", false, 0)]
        public static void CreateUTweenAnimation(MenuCommand menuCommand)
        {
            var go = new GameObject(nameof(UTweenAnimation));
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            go.transform.localPosition = Vector3.zero;
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            go.AddComponent<UTweenAnimation>();
            Selection.activeObject = go;
        }

        #endregion
    }

    [CustomEditor(typeof(UTweenAnimation))]
    [CanEditMultipleObjects]
    public class UTweenAnimationEditor : Editor
    {
        public virtual UTweenAnimation Target => target as UTweenAnimation;
        public UTweenAnimation TweenAnimation => Target;
        public TweenData Data => Target.Data;
        public List<Tweener> TweenerList => Data.TweenerList;

        [NonSerialized] public SerializedProperty TweenParamProperty;
        [NonSerialized] public SerializedProperty TweenerListProperty;

        public virtual void OnEnable()
        {
            InitEditor();
            Target.RefreshEditorAction = InitEditor;
            Data.RecordObject();
        }

        public virtual void OnDisable()
        {
        }

        public virtual void OnDestroy()
        {
            if (Data.IsSubAnimation) return;
            if (Data.PreviewSampled)
            {
                Data.RestoreObject();
                if (Data.IsPlaying) Data.Stop();
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            Data.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();

            if (Data.IsInProgress)
            {
                Repaint();
            }
        }

        public virtual void InitEditor()
        {
            Data.EditorNormalizedProgress = 0f;
            Data.TweenAnimation = Target;

            TweenParamProperty = serializedObject.FindProperty(nameof(Data));
            TweenerListProperty = TweenParamProperty.FindPropertyRelative(nameof(Data.TweenerList));

            Data.InitEditor(TweenEditorMode.Component, this);
        }
    }

#endif
}
