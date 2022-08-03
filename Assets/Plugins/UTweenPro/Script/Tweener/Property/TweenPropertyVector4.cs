using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Property Vector4", "Property", "cs Script Icon")]
    [Serializable]
    public partial class TweenPropertyVector4 : TweenValueVector4<Component>
    {
        public TweenPropertyData PropertyData = new TweenPropertyData();

        public override Vector4 Value
        {
            get => PropertyData.GetValue<Vector4>(Target);
            set => PropertyData.SetValue(Target, value);
        }

        public override void Reset()
        {
            base.Reset();
            PropertyData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenPropertyVector4 : TweenValueVector4<Component>
    {
        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            PropertyData.InitEditor(this, tweenerProperty);
        }

        public override void DrawTarget()
        {
            base.DrawTarget();
            PropertyData.DrawPropertyData<Vector4>();
        }
    }

#endif

    #region Extension

    public partial class TweenPropertyVector4 : TweenValueVector4<Component>
    {
        public TweenPropertyVector4 SetProperty(string property)
        {
            PropertyData.Property = property;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenPropertyVector4 Property(Component component, string propertyName, Vector4 to, float duration)
        {
            var tweener = Create<TweenPropertyVector4>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyVector4;
            return tweener;
        }

        public static TweenPropertyVector4 Property(Component component, string propertyName, Vector4 from, Vector4 to, float duration)
        {
            var tweener = Create<TweenPropertyVector4>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyVector4;
            return tweener;
        }
    }

    public static partial class ComponentExtension
    {
        public static TweenPropertyVector4 TweenProperty(this Component component, string propertyName, Vector4 to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, to, duration);
            return tweener;
        }

        public static TweenPropertyVector4 TweenProperty(this Component component, string propertyName, Vector4 from, Vector4 to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
