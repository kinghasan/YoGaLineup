using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Property String", "Property", "cs Script Icon")]
    [Serializable]
    public partial class TweenPropertyString : TweenValueString<Component>
    {
        public TweenPropertyData PropertyData = new TweenPropertyData();

        public override string Value
        {
            get => PropertyData.GetValue<string>(Target);
            set => PropertyData.SetValue(Target, value);
        }

        public override void Reset()
        {
            base.Reset();
            PropertyData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenPropertyString : TweenValueString<Component>
    {
        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            PropertyData.InitEditor(this, tweenerProperty);
        }

        public override void DrawTarget()
        {
            base.DrawTarget();
            PropertyData.DrawPropertyData<string>();
        }
    }

#endif

    #region Extension

    public partial class TweenPropertyString : TweenValueString<Component>
    {
        public TweenPropertyString SetProperty(string property)
        {
            PropertyData.Property = property;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenPropertyString Property(Component component, string propertyName, string to, float duration)
        {
            var tweener = Create<TweenPropertyString>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyString;
            return tweener;
        }

        public static TweenPropertyString Property(Component component, string propertyName, string from, string to, float duration)
        {
            var tweener = Create<TweenPropertyString>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyString;
            return tweener;
        }
    }

    public static partial class ComponentExtension
    {
        public static TweenPropertyString TweenProperty(this Component component, string propertyName, string to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, to, duration);
            return tweener;
        }

        public static TweenPropertyString TweenProperty(this Component component, string propertyName, string from, string to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
