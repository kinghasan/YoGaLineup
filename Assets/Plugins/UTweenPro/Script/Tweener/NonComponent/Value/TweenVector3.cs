using System;
using UnityEngine;

namespace Aya.TweenPro
{
    public class TweenVector3 : TweenValueVector3<UnityEngine.Object>
    {
        public override Vector3 Value { get; set; }
    }

    #region Extension
    
    public static partial class UTween
    {
        public static TweenVector3 Value(Vector3 from, Vector3 to, float duration, Action<Vector3> onValue)
        {
            var tweener = Create<TweenVector3>()
                .SetFrom(from)
                .SetTo(to)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenVector3;
            return tweener;
        }

        public static TweenVector3 Value(Func<Vector3> fromGetter, Func<Vector3> toGetter, float duration, Action<Vector3> onValue)
        {
            var tweener = Create<TweenVector3>()
                .SetFromGetter(fromGetter)
                .SetToGetter(toGetter)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenVector3;
            return tweener;
        }
    } 

    #endregion
}