using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Unity Event Float", "Unity Event", "BuildSettings.Editor")]
    [Serializable]
    public partial class TweenUnityEventFloat : TweenValueFloat<Object>
    {
        public override bool SupportTarget => false;

        public OnValueFloatEvent Event = new OnValueFloatEvent();

        public override float Value
        {
            get => _value;
            set
            {
                _value = value;
                Event.Invoke(_value);
            }
        }

        private float _value;

        public override void Reset()
        {
            base.Reset();
            Event.RemoveAllListeners();
        }
    }

#if UNITY_EDITOR

    public partial class TweenUnityEventFloat : TweenValueFloat<Object>
    {
        [TweenerProperty, NonSerialized] public SerializedProperty EventProperty;

        public override void DrawFromToValue()
        {
            base.DrawTarget();
            EditorGUILayout.PropertyField(EventProperty);
        }
    }

#endif
}