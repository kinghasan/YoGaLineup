using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Unity Event Vector2", "Unity Event", "BuildSettings.Editor")]
    [Serializable]
    public partial class TweenUnityEventVector2 : TweenValueVector2<Object>
    {
        public override bool SupportTarget => false;

        public OnValueVector2Event Event = new OnValueVector2Event();

        public override Vector2 Value
        {
            get => _value;
            set
            {
                _value = value;
                Event.Invoke(_value);
            }
        }

        private Vector2 _value;

        public override void Reset()
        {
            base.Reset();
            Event.RemoveAllListeners();
        }
    }

#if UNITY_EDITOR

    public partial class TweenUnityEventVector2 : TweenValueVector2<Object>
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