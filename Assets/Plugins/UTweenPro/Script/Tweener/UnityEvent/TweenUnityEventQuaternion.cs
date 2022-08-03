using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Unity Event Quaternion", "Unity Event", "BuildSettings.Editor")]
    [Serializable]
    public partial class TweenUnityEventQuaternion : TweenValueQuaternion<Object>
    {
        public override bool SupportTarget => false;

        public OnValueQuaternionEvent Event = new OnValueQuaternionEvent();

        public override Quaternion Value
        {
            get => _value;
            set
            {
                _value = value;
                Event.Invoke(_value);
            }
        }

        private Quaternion _value;

        public override void Reset()
        {
            base.Reset();
            Event.RemoveAllListeners();
        }
    }

#if UNITY_EDITOR

    public partial class TweenUnityEventQuaternion : TweenValueQuaternion<Object>
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