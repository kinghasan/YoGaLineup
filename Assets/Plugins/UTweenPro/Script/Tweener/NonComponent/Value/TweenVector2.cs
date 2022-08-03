using System;
using UnityEngine;

namespace Aya.TweenPro
{
    public class TweenVector2 : TweenValueVector2<UnityEngine.Object>
    {
        public override Vector2 Value { get; set; }
    }

    #region Extension
    
    public static partial class UTween
    {
        public static TweenVector2 Value(Vector2 from, Vector2 to, float duration, Action<Vector2> onValue)
        {
            var tweener = Create<TweenVector2>()
                .SetFrom(from)
                .SetTo(to)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenVector2;
            return tweener;
        }

        public static TweenVector2 Value(Func<Vector2> fromGetter, Func<Vector2> toGetter, float duration, Action<Vector2> onValue)
        {
            var tweener = Create<TweenVector2>()
                .SetFromGetter(fromGetter)
                .SetToGetter(toGetter)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenVector2;
            return tweener;
        }
    } 

    #endregion
}