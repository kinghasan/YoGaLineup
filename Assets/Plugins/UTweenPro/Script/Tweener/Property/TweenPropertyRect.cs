using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Property Rect", "Property", "cs Script Icon")]
    [Serializable]
    public partial class TweenPropertyRect : TweenValueRect<Component>
    {
        public TweenPropertyData PropertyData = new TweenPropertyData();

        public override Rect Value
        {
            get => PropertyData.GetValue<Rect>(Target);
            set => PropertyData.SetValue(Target, value);
        }

        public override void Reset()
        {
            base.Reset();
            PropertyData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenPropertyRect : TweenValueRect<Component>
    {
        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            PropertyData.InitEditor(this, tweenerProperty);
        }

        public override void DrawTarget()
        {
            base.DrawTarget();
            PropertyData.DrawPropertyData<Rect>();
        }
    }

#endif

    #region Extension

    public partial class TweenPropertyRect : TweenValueRect<Component>
    {
        public TweenPropertyRect SetProperty(string property)
        {
            PropertyData.Property = property;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenPropertyRect Property(Component component, string propertyName, Rect to, float duration)
        {
            var tweener = Create<TweenPropertyRect>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyRect;
            return tweener;
        }

        public static TweenPropertyRect Property(Component component, string propertyName, Rect from, Rect to, float duration)
        {
            var tweener = Create<TweenPropertyRect>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyRect;
            return tweener;
        }
    }

    public static partial class ComponentExtension
    {
        public static TweenPropertyRect TweenProperty(this Component component, string propertyName, Rect to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, to, duration);
            return tweener;
        }

        public static TweenPropertyRect TweenProperty(this Component component, string propertyName, Rect from, Rect to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
