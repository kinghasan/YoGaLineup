using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rigidbody Rotation", "Physics")]
    [Serializable]
    public partial class TweenRigidbodyRotation : TweenValueQuaternion<Rigidbody>
    {
        public override Quaternion Value
        {
            get => Target.rotation;
            set => Target.MoveRotation(Value);
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenRigidbodyRotation Rotation(Rigidbody rigidbody, Quaternion to, float duration)
        {
            var tweener = Play<TweenRigidbodyRotation, Rigidbody, Quaternion>(rigidbody, to, duration);
            return tweener;
        }

        public static TweenRigidbodyRotation Rotation(Rigidbody rigidbody, Quaternion from, Quaternion to, float duration)
        {
            var tweener = Play<TweenRigidbodyRotation, Rigidbody, Quaternion>(rigidbody, from, to, duration);
            return tweener;
        }
    }

    public static partial class RigidbodyExtension
    {
        public static TweenRigidbodyRotation TweenRotation(this Rigidbody rigidbody, Quaternion to, float duration)
        {
            var tweener = UTween.Rotation(rigidbody, to, duration);
            return tweener;
        }

        public static TweenRigidbodyRotation TweenRotation(this Rigidbody rigidbody, Quaternion from, Quaternion to, float duration)
        {
            var tweener = UTween.Rotation(rigidbody, from, to, duration);
            return tweener;
        }

        public static TweenRigidbodyRotation TweenLookAt(this Rigidbody rigidbody, Transform target, float duration)
        {
            var direction = target.position - rigidbody.position;
            var rotation = Quaternion.LookRotation(direction);
            var tweener = UTween.Rotation(rigidbody, rotation, duration);
            return tweener;
        }

        public static TweenRigidbodyRotation TweenLookAt(this Rigidbody rigidbody, Vector3 target, float duration)
        {
            var direction = target - rigidbody.position;
            var rotation = Quaternion.LookRotation(direction);
            var tweener = UTween.Rotation(rigidbody, rotation, duration);
            return tweener;
        }
    }

    #endregion
}