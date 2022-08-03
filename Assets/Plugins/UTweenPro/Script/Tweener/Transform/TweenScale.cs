using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Transform Scale", "Transform")]
    [Serializable]
    public class TweenScale : TweenValueVector3<Transform>
    {
        public override Vector3 Value
        {
            get => Target.localScale;
            set => Target.localScale = value;
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenScale Scale(Transform transform, float to, float duration)
        {
            var tweener = Play<TweenScale, Transform, Vector3>(transform, Vector3.one * to, duration)
                .SetCurrent2From() as TweenScale;
            return tweener;
        }

        public static TweenScale Scale(Transform transform, float from, float to, float duration)
        {
            var tweener = Play<TweenScale, Transform, Vector3>(transform, Vector3.one * from, Vector3.one * to, duration);
            return tweener;
        }

        public static TweenScale Scale(Transform transform, Vector3 to, float duration)
        {
            var tweener = Play<TweenScale, Transform, Vector3>(transform, to, duration)
                .SetCurrent2From() as TweenScale;
            return tweener;
        }

        public static TweenScale Scale(Transform transform, Vector3 from, Vector3 to, float duration)
        {
            var tweener = Play<TweenScale, Transform, Vector3>(transform, from, to, duration);
            return tweener;
        }
    }

    public static partial class TransformExtension
    {
        public static TweenScale TweenScale(this Transform transform, float to, float duration)
        {
            var tweener = UTween.Scale(transform, to, duration);
            return tweener;
        }

        public static TweenScale TweenScale(this Transform transform, float from, float to, float duration)
        {
            var tweener = UTween.Scale(transform, from, to, duration);
            return tweener;
        }

        public static TweenScale TweenScale(this Transform transform, Vector3 to, float duration)
        {
            var tweener = UTween.Scale(transform, to, duration);
            return tweener;
        }

        public static TweenScale TweenScale(this Transform transform, Vector3 from, Vector3 to, float duration)
        {
            var tweener = UTween.Scale(transform, from, to, duration);
            return tweener;
        }
    }

    #endregion
}