using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Transform EulerAngles", "Transform")]
    [Serializable]
    public class TweenEulerAngles : TweenValueVector3<Transform>
    {
        public override bool SupportSpace => true;

        public override Vector3 Value
        {
            get => Space == SpaceMode.World ? Target.eulerAngles : Target.localEulerAngles;
            set
            {
                if (Space == SpaceMode.World)
                {
                    Target.eulerAngles = value;
                }
                else
                {
                    Target.localEulerAngles = value;
                }
            }
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenEulerAngles EulerAngles(Transform transform, Vector3 to, float duration, SpaceMode spaceMode = SpaceMode.World)
        {
            var tweener = Play<TweenEulerAngles, Transform, Vector3>(transform, to, duration)
                .SetSpace(spaceMode)
                .SetCurrent2From() as TweenEulerAngles;
            return tweener;
        }

        public static TweenEulerAngles EulerAngles(Transform transform, Vector3 from, Vector3 to, float duration, SpaceMode spaceMode = SpaceMode.World)
        {
            var tweener = Play<TweenEulerAngles, Transform, Vector3>(transform, from, to, duration)
                .SetSpace(spaceMode);
            return tweener;
        }
    }

    public static partial class TransformExtension
    {
        public static TweenEulerAngles TweenEulerAngles(this Transform transform, Vector3 to, float duration, SpaceMode spaceMode = SpaceMode.World)
        {
            var tweener = UTween.EulerAngles(transform, to, duration, spaceMode);
            return tweener;
        }

        public static TweenEulerAngles TweenEulerAngles(this Transform transform, Vector3 from, Vector3 to, float duration, SpaceMode spaceMode = SpaceMode.World)
        {
            var tweener = UTween.EulerAngles(transform, from, to, duration, spaceMode);
            return tweener;
        }
    }

    #endregion
}