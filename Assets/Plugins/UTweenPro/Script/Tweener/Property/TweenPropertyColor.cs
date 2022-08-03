using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Property Color", "Property", "cs Script Icon")]
    [Serializable]
    public partial class TweenPropertyColor : TweenValueColor<Component>
    {
        public TweenPropertyData PropertyData = new TweenPropertyData();

        public override Color Value
        {
            get => PropertyData.GetValue<Color>(Target);
            set => PropertyData.SetValue(Target, value);
        }

        public override void Reset()
        {
            base.Reset();
            PropertyData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenPropertyColor : TweenValueColor<Component>
    {
        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            PropertyData.InitEditor(this, tweenerProperty);
        }

        public override void DrawTarget()
        {
            base.DrawTarget();
            PropertyData.DrawPropertyData<Color>();
        }
    }

#endif

    #region Extension

    public partial class TweenPropertyColor : TweenValueColor<Component>
    {
        public TweenPropertyColor SetProperty(string property)
        {
            PropertyData.Property = property;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenPropertyColor Property(Component component, string propertyName, Color to, float duration)
        {
            var tweener = Create<TweenPropertyColor>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyColor;
            return tweener;
        }

        public static TweenPropertyColor Property(Component component, string propertyName, Color from, Color to, float duration)
        {
            var tweener = Create<TweenPropertyColor>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyColor;
            return tweener;
        }
    }

    public static partial class ComponentExtension
    {
        public static TweenPropertyColor TweenProperty(this Component component, string propertyName, Color to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, to, duration);
            return tweener;
        }

        public static TweenPropertyColor TweenProperty(this Component component, string propertyName, Color from, Color to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
