using System;
using UnityEngine;

namespace Aya.TweenPro
{
    public class TweenVector4 : TweenValueVector4<UnityEngine.Object>
    {
        public override Vector4 Value { get; set; }
    }

    #region Extension
    
    public static partial class UTween
    {
        public static TweenVector4 Value(Vector4 from, Vector4 to, float duration, Action<Vector4> onValue)
        {
            var tweener = Create<TweenVector4>()
                .SetFrom(from)
                .SetTo(to)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenVector4;
            return tweener;
        }

        public static TweenVector4 Value(Func<Vector4> fromGetter, Func<Vector4> toGetter, float duration, Action<Vector4> onValue)
        {
            var tweener = Create<TweenVector4>()
                .SetFromGetter(fromGetter)
                .SetToGetter(toGetter)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenVector4;
            return tweener;
        }
    } 

    #endregion
}