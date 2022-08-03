using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Unity Event Rect", "Unity Event", "BuildSettings.Editor")]
    [Serializable]
    public partial class TweenUnityEventRect : TweenValueRect<Object>
    {
        public override bool SupportTarget => false;

        public OnValueRectEvent Event = new OnValueRectEvent();

        public override Rect Value
        {
            get => _value;
            set
            {
                _value = value;
                Event.Invoke(_value);
            }
        }

        private Rect _value;

        public override void Reset()
        {
            base.Reset();
            Event.RemoveAllListeners();
        }
    }

#if UNITY_EDITOR

    public partial class TweenUnityEventRect : TweenValueRect<Object>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty EventProperty;

        public override void DrawFromToValue()
        {
            EditorGUILayout.PropertyField(EventProperty);
            base.DrawFromToValue();
        }
    }

#endif
}