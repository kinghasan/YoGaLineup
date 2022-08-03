using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Unity Event Boolean", "Unity Event", "BuildSettings.Editor")]
    [Serializable]
    public partial class TweenUnityEventBoolean : TweenValueBoolean<Object>
    {
        public override bool SupportTarget => false;

        public OnValueBooleanEvent Event = new OnValueBooleanEvent();

        public override bool Value
        {
            get => _value;
            set
            {
                _value = value;
                Event.Invoke(_value);
            }
        }

        private bool _value;

        public override void Reset()
        {
            base.Reset();
            Event.RemoveAllListeners();
        }
    }

#if UNITY_EDITOR

    public partial class TweenUnityEventBoolean : TweenValueBoolean<Object>
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