using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Property Boolean", "Property", "cs Script Icon")]
    [Serializable]
    public partial class TweenPropertyBoolean : TweenValueBoolean<Component>
    {
        public TweenPropertyData PropertyData = new TweenPropertyData();

        public override bool Value
        {
            get => PropertyData.GetValue<bool>(Target);
            set => PropertyData.SetValue(Target, value);
        }

        public override void Reset()
        {
            base.Reset();
            PropertyData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenPropertyBoolean : TweenValueBoolean<Component>
    {
        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            PropertyData.InitEditor(this, tweenerProperty);
        }

        public override void DrawTarget()
        {
            base.DrawTarget();
            PropertyData.DrawPropertyData<bool>();
        }
    }

#endif

    #region Extension

    public partial class TweenPropertyBoolean : TweenValueBoolean<Component>
    {
        public TweenPropertyBoolean SetProperty(string property)
        {
            PropertyData.Property = property;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenPropertyBoolean Property(Component component, string propertyName, bool to, float duration)
        {
            var tweener = Create<TweenPropertyBoolean>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyBoolean;
            return tweener;
        }

        public static TweenPropertyBoolean Property(Component component, string propertyName, bool from, bool to, float duration)
        {
            var tweener = Create<TweenPropertyBoolean>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyBoolean;
            return tweener;
        }
    }

    public static partial class ComponentExtension
    {
        public static TweenPropertyBoolean TweenProperty(this Component component, string propertyName, bool to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, to, duration);
            return tweener;
        }

        public static TweenPropertyBoolean TweenProperty(this Component component, string propertyName, bool from, bool to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion

}
