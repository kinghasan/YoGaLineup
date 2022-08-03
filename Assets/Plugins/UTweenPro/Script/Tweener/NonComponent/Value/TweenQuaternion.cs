using System;
using UnityEngine;

namespace Aya.TweenPro
{
    public class TweenQuaternion : TweenValueQuaternion<UnityEngine.Object>
    {
        public override Quaternion Value { get; set; }
    }

    #region Extension
    
    public static partial class UTween
    {
        public static TweenQuaternion Value(Quaternion from, Quaternion to, float duration, Action<Quaternion> onValue)
        {
            var tweener = Create<TweenQuaternion>()
                .SetFrom(from)
                .SetTo(to)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenQuaternion;
            return tweener;
        }

        public static TweenQuaternion Value(Func<Quaternion> fromGetter, Func<Quaternion> toGetter, float duration, Action<Quaternion> onValue)
        {
            var tweener = Create<TweenQuaternion>()
                .SetFromGetter(fromGetter)
                .SetToGetter(toGetter)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenQuaternion;
            return tweener;
        }
    } 

    #endregion
}