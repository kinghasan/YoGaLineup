using System;
using UnityEngine;

namespace Aya.TweenPro
{
    [Tweener("Rigidbody2D Position", "Physics")]
    [Serializable]
    public partial class TweenRigidbody2DPosition : TweenValueVector2<Rigidbody2D>
    {
        public override Vector2 Value
        {
            get => Target.position;
            set => Target.MovePosition(Value);
        }
    }

    #region Extension
    
    public static partial class UTween
    {
        public static TweenRigidbody2DPosition Position(Rigidbody2D rigidbody2D, Vector2 to, float duration)
        {
            var tweener = Play<TweenRigidbody2DPosition, Rigidbody2D, Vector2>(rigidbody2D, to, duration);
            return tweener;
        }

        public static TweenRigidbody2DPosition Position(Rigidbody2D rigidbody2D, Vector2 from, Vector2 to, float duration)
        {
            var tweener = Play<TweenRigidbody2DPosition, Rigidbody2D, Vector2>(rigidbody2D, from, to, duration);
            return tweener;
        }
    }

    public static partial class Rigidbody2DExtension
    {
        public static TweenRigidbody2DPosition TweenPosition(this Rigidbody2D rigidbody2D, Vector2 to, float duration)
        {
            var tweener = UTween.Position(rigidbody2D, to, duration);
            return tweener;
        }

        public static TweenRigidbody2DPosition TweenPosition(this Rigidbody2D rigidbody2D, Vector2 from, Vector2 to, float duration)
        {
            var tweener = UTween.Position(rigidbody2D, from, to, duration);
            return tweener;
        }
    } 

    #endregion
}