using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rigidbody2D Rotation", "Physics")]
    [Serializable]
    public partial class TweenRigidbody2DRotation : TweenValueFloat<Rigidbody2D>
    {
        public override float Value
        {
            get => Target.rotation;
            set => Target.MoveRotation(Value);
        }
    }

    #region Extension

    public static partial class UTween
    {
        public static TweenRigidbody2DRotation Rotation(Rigidbody2D rigidbody2D, float to, float duration)
        {
            var tweener = Play<TweenRigidbody2DRotation, Rigidbody2D, float>(rigidbody2D, to, duration);
            return tweener;
        }

        public static TweenRigidbody2DRotation Rotation(Rigidbody2D rigidbody2D, float from, float to, float duration)
        {
            var tweener = Play<TweenRigidbody2DRotation, Rigidbody2D, float>(rigidbody2D, from, to, duration);
            return tweener;
        }
    }

    public static partial class Rigidbody2DExtension
    {
        public static TweenRigidbody2DRotation TweenRotation(this Rigidbody2D rigidbody2D, float to, float duration)
        {
            var tweener = UTween.Rotation(rigidbody2D, to, duration);
            return tweener;
        }

        public static TweenRigidbody2DRotation TweenRotation(this Rigidbody2D rigidbody2D, float from, float to, float duration)
        {
            var tweener = UTween.Rotation(rigidbody2D, from, to, duration);
            return tweener;
        }
    }

    #endregion
}