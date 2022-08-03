using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public abstract partial class TweenTrigger : MonoBehaviour
    {
        public TargetAnimationData Target = new TargetAnimationData();
        public TriggerActionType Action;

        public virtual void Awake()
        {
            
        }

        public virtual void OnTrigger()
        {
            if (Target.Data == null) return;
            switch (Action)
            {
                case TriggerActionType.Play:
                    Target.Data.Play();
                    break;
                case TriggerActionType.PlayBackward:
                    Target.Data.PlayBackward();
                    break;
                case TriggerActionType.Pause:
                    Target.Data.Pause();
                    break;
                case TriggerActionType.Resume:
                    Target.Data.Resume();
                    break;
                case TriggerActionType.Stop:
                    Target.Data.Stop();
                    break;
            }
        }

        public virtual void Reset()
        {
            Target.Reset();
            Target.Component = GetComponentInChildren<UTweenAnimation>();
            Action = TriggerActionType.Play;
        }
    }

#if UNITY_EDITOR
    public abstract partial class TweenTrigger : MonoBehaviour
    {
        [NonSerialized] public SerializedObject SerializedObject;

        [TweenerProperty, NonSerialized] public SerializedProperty TargetProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty ActionProperty;

        public virtual void InitEditor()
        {
            TweenerPropertyAttribute.CacheProperty(this, SerializedObject);
            Target.InitEditor(TargetProperty);
        }

        public virtual void DrawTrigger()
        {
            DrawInfo();
            DrawBody();
        }

        public virtual void DrawInfo()
        {
            Target.DrawAnimationData();
            EditorGUILayout.PropertyField(ActionProperty);
        }

        public virtual void DrawBody()
        {

        }
    }

    [CustomEditor(typeof(TweenTrigger))]
    [CanEditMultipleObjects]
    public abstract partial class TweenTriggerEditor<T> : Editor
        where T : TweenTrigger
    {
        public virtual T Target => target as T;

        public virtual void OnEnable()
        {
            Target.SerializedObject = serializedObject;
            Target.InitEditor();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            Target.DrawTrigger();
            serializedObject.ApplyModifiedProperties();
        }
    }

#endif
}