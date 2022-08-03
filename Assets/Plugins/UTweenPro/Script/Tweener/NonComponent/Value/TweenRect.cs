using System;
using UnityEngine;

namespace Aya.TweenPro
{
    public class TweenRect : TweenValueRect<UnityEngine.Object>
    {
        public override Rect Value { get; set; }
    }

    #region Extension
    
    public static partial class UTween
    {
        public static TweenRect Value(Rect from, Rect to, float duration, Action<Rect> onValue)
        {
            var tweener = Create<TweenRect>()
                .SetFrom(from)
                .SetTo(to)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenRect;
            return tweener;
        }

        public static TweenRect Value(Func<Rect> fromGetter, Func<Rect> toGetter, float duration, Action<Rect> onValue)
        {
            var tweener = Create<TweenRect>()
                .SetFromGetter(fromGetter)
                .SetToGetter(toGetter)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenRect;
            return tweener;
        }
    } 

    #endregion
}