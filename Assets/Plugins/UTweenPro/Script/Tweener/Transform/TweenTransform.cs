using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Transform", "Transform")]
    [Serializable]
    public class TweenTransform : TweenValueTransform<Transform>
    {
        public override Transform Value
        {
            get => Target;
            set => Target = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenTransform Transform(Transform transform, Transform from, Transform to, float duration)
        {
            var tweener = Play<TweenTransform, Transform, Transform>(transform, from, to, duration);
            return tweener;
        }
    }

    public static partial class TransformExtension
    {
        public static TweenTransform TweenTransform(this Transform transform, Transform from, Transform to, float duration)
        {
            var tweener = UTween.Transform(transform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}