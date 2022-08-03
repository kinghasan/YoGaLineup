using System;

namespace Aya.TweenPro
{
    public class TweenLong : TweenValueLong<UnityEngine.Object>
    {
        public override long Value { get; set; }
    }

    #region Extension
    
    public static partial class UTween
    {
        #region long

        public static TweenLong Value(long from, long to, float duration, Action<long> onValue)
        {
            var tweener = Create<TweenLong>()
                .SetFrom(from)
                .SetTo(to)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenLong;
            return tweener;
        }

        public static TweenLong Value(Func<long> fromGetter, Func<long> toGetter, float duration, Action<long> onValue)
        {
            var tweener = Create<TweenLong>()
                .SetFromGetter(fromGetter)
                .SetToGetter(toGetter)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenLong;
            return tweener;
        }

        #endregion

        #region ulong

        public static TweenLong Value(ulong from, ulong to, float duration, Action<ulong> onValue)
        {
            var tweener = Create<TweenLong>()
                .SetFrom((long)from)
                .SetTo((long)to)
                .SetValueSetter(value => { onValue((ulong)value); })
                .SetDuration(duration)
                .Play() as TweenLong;
            return tweener;
        }

        public static TweenInteger Value(Func<ulong> fromGetter, Func<ulong> toGetter, float duration, Action<ulong> onValue)
        {
            var tweener = Create<TweenInteger>()
                .SetFromGetter(() => (int)fromGetter())
                .SetToGetter(() => (int)toGetter())
                .SetValueSetter(value => { onValue((ulong)value); })
                .SetDuration(duration)
                .Play() as TweenInteger;
            return tweener;
        }

        #endregion
    } 

    #endregion
}