using System;
using System.Reflection;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Serializable]
    public partial class TweenPropertyData
    {
        public string Property;

        public BindingFlags Flags => TypeCaches.DefaultBindingFlags;

        [NonSerialized] public object CacheTarget;
        [NonSerialized] public string CacheProperty;
        [NonSerialized] public PropertyInfo PropertyInfo;
        [NonSerialized] public FieldInfo FieldInfo;

        public void SetValue<TValue>(object target, TValue value)
        {
            if (target == null) return;
            CacheTargetInfo(target);
            PropertyInfo?.SetValue(target, value);
            FieldInfo?.SetValue(target, value);
        }

        public TValue GetValue<TValue>(object target)
        {
            if (target == null) return default;
            CacheTargetInfo(target);
            PropertyInfo?.GetValue(target);
            FieldInfo?.GetValue(target);
            return default;
        }

        internal void CacheTargetInfo(object target)
        {
            if (CacheTarget == target && CacheProperty == Property) return;
            CacheTarget = target;
            CacheProperty = Property;
            PropertyInfo = target.GetType().GetProperty(Property, Flags);
            FieldInfo = target.GetType().GetField(Property, Flags);
        }

        public void Reset()
        {
            Property = "";
        }
    }

#if UNITY_EDITOR

    public partial class TweenPropertyData
    {
        [NonSerialized] public Tweener<Component> Tweener;
        [NonSerialized] public SerializedProperty TweenerProperty;
        [NonSerialized] public SerializedProperty PropertyDataProperty;

        [TweenerProperty, NonSerialized] public SerializedProperty PropertyProperty;

        public void InitEditor(Tweener<Component> tweener, SerializedProperty tweenerProperty)
        {
            Tweener = tweener;
            TweenerProperty = tweenerProperty;
            PropertyDataProperty = TweenerProperty.FindPropertyRelative("PropertyData");

            TweenerPropertyAttribute.CacheProperty(this, PropertyDataProperty);
        }

        public void DrawPropertyData<TValue>()
        {
            if (Tweener.Data.Mode == TweenEditorMode.Component)
            {
                if (Tweener.Target != null)
                {
                    GUIMenu.DrawPropertyMenu<TValue>(Tweener.Target, nameof(Property), PropertyProperty);
                }
            }
            else
            {
                EditorGUILayout.PropertyField(PropertyProperty, new GUIContent(nameof(Property)));
            }
        }
    }

#endif
}
