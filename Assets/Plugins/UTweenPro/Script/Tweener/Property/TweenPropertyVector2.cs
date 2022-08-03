using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Property Vector2", "Property", "cs Script Icon")]
    [Serializable]
    public partial class TweenPropertyVector2 : TweenValueVector2<Component>
    {
        public TweenPropertyData PropertyData = new TweenPropertyData();

        public override Vector2 Value
        {
            get => PropertyData.GetValue<Vector2>(Target);
            set => PropertyData.SetValue(Target, value);
        }

        public override void Reset()
        {
            base.Reset();
            PropertyData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenPropertyVector2 : TweenValueVector2<Component>
    {
        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            PropertyData.InitEditor(this, tweenerProperty);
        }

        public override void DrawTarget()
        {
            base.DrawTarget();
            PropertyData.DrawPropertyData<Vector2>();
        }
    }

#endif

    #region Extension

    public partial class TweenPropertyVector2 : TweenValueVector2<Component>
    {
        public TweenPropertyVector2 SetProperty(string property)
        {
            PropertyData.Property = property;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenPropertyVector2 Property(Component component, string propertyName, Vector2 to, float duration)
        {
            var tweener = Create<TweenPropertyVector2>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyVector2;
            return tweener;
        }

        public static TweenPropertyVector2 Property(Component component, string propertyName, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Create<TweenPropertyVector2>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyVector2;
            return tweener;
        }
    }

    public static partial class ComponentExtension
    {
        public static TweenPropertyVector2 TweenProperty(this Component component, string propertyName, Vector2 to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, to, duration);
            return tweener;
        }

        public static TweenPropertyVector2 TweenProperty(this Component component, string propertyName, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
