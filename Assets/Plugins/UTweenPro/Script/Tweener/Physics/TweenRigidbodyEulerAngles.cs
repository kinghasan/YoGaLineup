using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rigidbody EulerAngles", "Physics")]
    [Serializable]
    public partial class TweenRigidbodyEulerAngles : TweenValueVector3<Rigidbody>
    {
        public override Vector3 Value
        {
            get => Target.rotation.eulerAngles;
            set => Target.MoveRotation(Quaternion.Euler(Value));
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenRigidbodyEulerAngles EulerAngles(Rigidbody rigidbody, Vector3 to, float duration)
        {
            var tweener = Play<TweenRigidbodyEulerAngles, Rigidbody, Vector3>(rigidbody, to, duration);
            return tweener;
        }

        public static TweenRigidbodyEulerAngles EulerAngles(Rigidbody rigidbody, Vector3 from, Vector3 to, float duration)
        {
            var tweener = Play<TweenRigidbodyEulerAngles, Rigidbody, Vector3>(rigidbody, from, to, duration);
            return tweener;
        }
    }

    public static partial class RigidbodyExtension
    {
        public static TweenRigidbodyEulerAngles TweenEulerAngles(this Rigidbody rigidbody, Vector3 to, float duration)
        {
            var tweener = UTween.EulerAngles(rigidbody, to, duration);
            return tweener;
        }

        public static TweenRigidbodyEulerAngles TweenEulerAngles(this Rigidbody rigidbody, Vector3 from, Vector3 to, float duration)
        {
            var tweener = UTween.EulerAngles(rigidbody, from, to, duration);
            return tweener;
        }
    }

    #endregion
}