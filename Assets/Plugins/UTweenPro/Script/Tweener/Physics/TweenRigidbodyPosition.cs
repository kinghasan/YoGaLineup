using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rigidbody Position", "Physics")]
    [Serializable]
    public partial class TweenRigidbodyPosition : TweenValueVector3<Rigidbody>
    {
        public override Vector3 Value
        {
            get => Target.position;
            set => Target.MovePosition(Value);
        }
    }

    #region Extension
    
    public static partial class UTween
    {
        public static TweenRigidbodyPosition Position(Rigidbody rigidbody, Vector3 to, float duration)
        {
            var tweener = Play<TweenRigidbodyPosition, Rigidbody, Vector3>(rigidbody, to, duration);
            return tweener;
        }

        public static TweenRigidbodyPosition Position(Rigidbody rigidbody, Vector3 from, Vector3 to, float duration)
        {
            var tweener = Play<TweenRigidbodyPosition, Rigidbody, Vector3>(rigidbody, from, to, duration);
            return tweener;
        }
    }

    public static partial class RigidbodyExtension
    {
        public static TweenRigidbodyPosition TweenPosition(this Rigidbody rigidbody, Vector3 to, float duration)
        {
            var tweener = UTween.Position(rigidbody, to, duration);
            return tweener;
        }

        public static TweenRigidbodyPosition TweenPosition(this Rigidbody rigidbody, Vector3 from, Vector3 to, float duration)
        {
            var tweener = UTween.Position(rigidbody, from, to, duration);
            return tweener;
        }
    } 

    #endregion
}