using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Unity Event Color", "Unity Event", "BuildSettings.Editor")]
    [Serializable]
    public partial class TweenUnityEventColor : TweenValueColor<Object>
    {
        public override bool SupportTarget => false;

        public OnValueColorEvent Event = new OnValueColorEvent();

        public override Color Value
        {
            get => _value;
            set
            {
                _value = value;
                Event.Invoke(_value);
            }
        }

        private Color _value;

        public override void Reset()
        {
            base.Reset();
            Event.RemoveAllListeners();
        }
    }

#if UNITY_EDITOR

    public partial class TweenUnityEventColor : TweenValueColor<Object>
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