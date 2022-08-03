using System;
using UnityEngine;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    public enum UIEventType
    {
        None = 0,
        Enter = 1,
        Leave = 2,
        Hover = 3,  // Enter - Exit
        Down = 4,
        Up = 5,
        Click = 6,
        DoubleClick = 7
    }

    [AddComponentMenu("UTween Pro/Trigger/UI Event Trigger")]
    [Serializable]
    public partial class UIEventTrigger : TweenTrigger, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public UIEventType Type;

        #region UI Event

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Type != UIEventType.Click) return;
            OnTrigger();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Type != UIEventType.Down) return;
            OnTrigger();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (Type == UIEventType.Enter || Type == UIEventType.Hover) OnTrigger();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (Type == UIEventType.Leave) OnTrigger();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (Type != UIEventType.Up) return;
            OnTrigger();
        } 

        #endregion
    }

#if UNITY_EDITOR

    public partial class UIEventTrigger
    {
        [TweenerProperty, NonSerialized] public SerializedProperty TypeProperty;

        public override void DrawBody()
        {
            EditorGUILayout.PropertyField(TypeProperty);
        }
    }


   [CustomEditor(typeof(UIEventTrigger))]
    [CanEditMultipleObjects]
    public partial class UIEventTriggerEditor : TweenTriggerEditor<UIEventTrigger>
    {

    }

#endif
}