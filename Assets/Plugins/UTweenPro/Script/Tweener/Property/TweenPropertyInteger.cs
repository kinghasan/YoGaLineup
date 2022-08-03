using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Property Integer", "Property", "cs Script Icon")]
    [Serializable]
    public partial class TweenPropertyInteger : TweenValueInteger<Component>
    {
        public TweenPropertyData PropertyData = new TweenPropertyData();

        public override int Value
        {
            get => PropertyData.GetValue<int>(Target);
            set => PropertyData.SetValue(Target, value);
        }

        public override void Reset()
        {
            base.Reset();
            PropertyData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenPropertyInteger : TweenValueInteger<Component>
    {
        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            PropertyData.InitEditor(this, tweenerProperty);
        }

        public override void DrawTarget()
        {
            base.DrawTarget();
            PropertyData.DrawPropertyData<int>();
        }
    }

#endif

    #region Extension

    public partial class TweenPropertyInteger : TweenValueInteger<Component>
    {
        public TweenPropertyInteger SetProperty(string property)
        {
            PropertyData.Property = property;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenPropertyInteger Property(Component component, string propertyName, int to, float duration)
        {
            var tweener = Create<TweenPropertyInteger>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyInteger;
            return tweener;
        }

        public static TweenPropertyInteger Property(Component component, string propertyName, int from, int to, float duration)
        {
            var tweener = Create<TweenPropertyInteger>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyInteger;
            return tweener;
        }
    }

    public static partial class ComponentExtension
    {
        public static TweenPropertyInteger TweenProperty(this Component component, string propertyName, int to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, to, duration);
            return tweener;
        }

        public static TweenPropertyInteger TweenProperty(this Component component, string propertyName, int from, int to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion

}
