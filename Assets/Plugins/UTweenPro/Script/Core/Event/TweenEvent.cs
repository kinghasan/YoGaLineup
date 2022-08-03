using System;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public partial class TweenEvent
    {
        public UnityEvent Event;
        public Action Action;

        public void AddListener(Action action)
        {
            Action += action;
        }

        public void RemoveListener(Action action)
        {
            Action -= action;
        }

        public void Invoke()
        {
            Event?.Invoke();
            Action?.Invoke();
        }

        public void Reset()
        {
            Event = null;
            Action = null;
        }

        public void InitEvent()
        {
            Event = new UnityEvent();
            Action = delegate { };
        }
    }

#if UNITY_EDITOR

    public partial class TweenEvent
    {
        [TweenerProperty, NonSerialized] public SerializedProperty TweenDataProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty CallbackProperty;
        [TweenerProperty, NonSerialized] public SerializedProperty EventProperty;

        public void InitEditor(SerializedProperty dataProperty, string propertyName)
        {
            TweenDataProperty = dataProperty;
            CallbackProperty = TweenDataProperty.FindPropertyRelative(propertyName);
            EventProperty = CallbackProperty.FindPropertyRelative(nameof(Event));
        }

        public void DrawEvent(string eventName)
        {
            if (Event == null) InitEvent();
            EditorGUILayout.PropertyField(EventProperty, new GUIContent(eventName));
        }
    }

#endif

}
