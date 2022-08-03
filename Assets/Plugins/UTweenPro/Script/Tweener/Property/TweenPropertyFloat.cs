using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Property Float", "Property", "cs Script Icon")]
    [Serializable]
    public partial class TweenPropertyFloat : TweenValueFloat<Component>
    {
        public TweenPropertyData PropertyData = new TweenPropertyData();

        public override float Value
        {
            get => PropertyData.GetValue<float>(Target);
            set => PropertyData.SetValue(Target, value);
        }

        public override void Reset()
        {
            base.Reset();
            PropertyData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenPropertyFloat : TweenValueFloat<Component>
    {
        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            PropertyData.InitEditor(this, tweenerProperty);
        }

        public override void DrawTarget()
        {
            base.DrawTarget();
            PropertyData.DrawPropertyData<float>();
        }
    }

#endif

    #region Extension

    public partial class TweenPropertyFloat : TweenValueFloat<Component>
    {
        public TweenPropertyFloat SetProperty(string property)
        {
            PropertyData.Property = property;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenPropertyFloat Property(Component component, string propertyName, float to, float duration)
        {
            var tweener = Create<TweenPropertyFloat>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyFloat;
            return tweener;
        }

        public static TweenPropertyFloat Property(Component component, string propertyName, float from, float to, float duration)
        {
            var tweener = Create<TweenPropertyFloat>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyFloat;
            return tweener;
        }
    }

    public static partial class ComponentExtension
    {
        public static TweenPropertyFloat TweenProperty(this Component component, string propertyName, float to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, to, duration);
            return tweener;
        }

        public static TweenPropertyFloat TweenProperty(this Component component, string propertyName, float from, float to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}