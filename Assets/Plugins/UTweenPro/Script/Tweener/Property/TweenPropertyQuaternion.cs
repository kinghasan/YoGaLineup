using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Aya.TweenPro
{
    [Tweener("Property Quaternion", "Property", "cs Script Icon")]
    [Serializable]
    public partial class TweenPropertyQuaternion : TweenValueQuaternion<Component>
    {
        public TweenPropertyData PropertyData = new TweenPropertyData();

        public override Quaternion Value
        {
            get => PropertyData.GetValue<Quaternion>(Target);
            set => PropertyData.SetValue(Target, value);
        }

        public override void Reset()
        {
            base.Reset();
            PropertyData.Reset();
        }
    }

#if UNITY_EDITOR

    public partial class TweenPropertyQuaternion : TweenValueQuaternion<Component>
    {
        public override void InitEditor(int index, TweenData data, SerializedProperty tweenerProperty)
        {
            base.InitEditor(index, data, tweenerProperty);
            PropertyData.InitEditor(this, tweenerProperty);
        }

        public override void DrawTarget()
        {
            base.DrawTarget();
            PropertyData.DrawPropertyData<Quaternion>();
        }
    }

#endif

    #region Extension

    public partial class TweenPropertyQuaternion : TweenValueQuaternion<Component>
    {
        public TweenPropertyQuaternion SetProperty(string property)
        {
            PropertyData.Property = property;
            return this;
        }
    }

    public static partial class UTween
    {
        public static TweenPropertyQuaternion Property(Component component, string propertyName, Quaternion to, float duration)
        {
            var tweener = Create<TweenPropertyQuaternion>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetCurrent2From()
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyQuaternion;
            return tweener;
        }

        public static TweenPropertyQuaternion Property(Component component, string propertyName, Quaternion from, Quaternion to, float duration)
        {
            var tweener = Create<TweenPropertyQuaternion>()
                .SetTarget(component)
                .SetProperty(propertyName)
                .SetFrom(from)
                .SetTo(to)
                .SetDuration(duration)
                .Play() as TweenPropertyQuaternion;
            return tweener;
        }
    }

    public static partial class ComponentExtension
    {
        public static TweenPropertyQuaternion TweenProperty(this Component component, string propertyName, Quaternion to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, to, duration);
            return tweener;
        }

        public static TweenPropertyQuaternion TweenProperty(this Component component, string propertyName, Quaternion from, Quaternion to, float duration)
        {
            var tweener = UTween.Property(component, propertyName, from, to, duration);
            return tweener;
        }
    }

    #endregion
}
