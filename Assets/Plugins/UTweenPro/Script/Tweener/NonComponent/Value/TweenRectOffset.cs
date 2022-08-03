using System;
using UnityEngine;

namespace Aya.TweenPro
{
    public class TweenRectOffset : TweenValueRectOffset<UnityEngine.Object>
    {
        public override RectOffset Value { get; set; }
    }

    #region Extension
    
    public static partial class UTween
    {
        public static TweenRectOffset Value(RectOffset from, RectOffset to, float duration, Action<RectOffset> onValue)
        {
            var tweener = Create<TweenRectOffset>()
                .SetFrom(from)
                .SetTo(to)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenRectOffset;
            return tweener;
        }

        public static TweenRectOffset Value(Func<RectOffset> fromGetter, Func<RectOffset> toGetter, float duration, Action<RectOffset> onValue)
        {
            var tweener = Create<TweenRectOffset>()
                .SetFromGetter(fromGetter)
                .SetToGetter(toGetter)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenRectOffset;
            return tweener;
        }
    } 

    #endregion
}