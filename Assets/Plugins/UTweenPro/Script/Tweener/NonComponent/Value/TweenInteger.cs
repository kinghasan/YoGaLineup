using System;

namespace Aya.TweenPro
{
    public class TweenInteger : TweenValueInteger<UnityEngine.Object>
    {
        public override int Value { get; set; }
    }

    #region Extension
    
    public static partial class UTween
    {
        #region byte

        public static TweenInteger Value(byte from, byte to, float duration, Action<byte> onValue)
        {
            var tweener = Create<TweenInteger>()
                .SetFrom((int)from)
                .SetTo((int)to)
                .SetValueSetter(value => { onValue((byte)value); })
                .SetDuration(duration)
                .Play() as TweenInteger;
            return tweener;
        }

        public static TweenInteger Value(Func<byte> fromGetter, Func<byte> toGetter, float duration, Action<byte> onValue)
        {
            var tweener = Create<TweenInteger>()
                .SetFromGetter(() => (int)fromGetter())
                .SetToGetter(() => (int)toGetter())
                .SetValueSetter(value => { onValue((byte)value); })
                .SetDuration(duration)
                .Play() as TweenInteger;
            return tweener;
        }

        #endregion

        #region short

        public static TweenInteger Value(ushort from, ushort to, float duration, Action<ushort> onValue)
        {
            var tweener = Create<TweenInteger>()
                .SetFrom((int)from)
                .SetTo((int)to)
                .SetValueSetter(value => { onValue((ushort)value); })
                .SetDuration(duration)
                .Play() as TweenInteger;
            return tweener;
        }

        public static TweenInteger Value(Func<ushort> fromGetter, Func<ushort> toGetter, float duration, Action<ushort> onValue)
        {
            var tweener = Create<TweenInteger>()
                .SetFromGetter(() => (int)fromGetter())
                .SetToGetter(() => (int)toGetter())
                .SetValueSetter(value => { onValue((ushort)value); })
                .SetDuration(duration)
                .Play() as TweenInteger;
            return tweener;
        }

        #endregion

        #region short

        public static TweenInteger Value(short from, short to, float duration, Action<short> onValue)
        {
            var tweener = Create<TweenInteger>()
                .SetFrom((int)from)
                .SetTo((int)to)
                .SetValueSetter(value => { onValue((short)value); })
                .SetDuration(duration)
                .Play() as TweenInteger;
            return tweener;
        }

        public static TweenInteger Value(Func<short> fromGetter, Func<short> toGetter, float duration, Action<short> onValue)
        {
            var tweener = Create<TweenInteger>()
                .SetFromGetter(() => (int)fromGetter())
                .SetToGetter(() => (int)toGetter())
                .SetValueSetter(value => { onValue((short)value); })
                .SetDuration(duration)
                .Play() as TweenInteger;
            return tweener;
        }

        #endregion

        #region uint

        public static TweenInteger Value(uint from, uint to, float duration, Action<uint> onValue)
        {
            var tweener = Create<TweenInteger>()
                .SetFrom((int)from)
                .SetTo((int)to)
                .SetValueSetter(value => { onValue((uint)value); })
                .SetDuration(duration)
                .Play() as TweenInteger;
            return tweener;
        }

        public static TweenInteger Value(Func<uint> fromGetter, Func<uint> toGetter, float duration, Action<uint> onValue)
        {
            var tweener = Create<TweenInteger>()
                .SetFromGetter(() => (int)fromGetter())
                .SetToGetter(() => (int)toGetter())
                .SetValueSetter(value => { onValue((uint)value); })
                .SetDuration(duration)
                .Play() as TweenInteger;
            return tweener;
        } 

        #endregion

        #region int

        public static TweenInteger Value(int from, int to, float duration, Action<int> onValue)
        {
            var tweener = Create<TweenInteger>()
                .SetFrom(from)
                .SetTo(to)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenInteger;
            return tweener;
        }

        public static TweenInteger Value(Func<int> fromGetter, Func<int> toGetter, float duration, Action<int> onValue)
        {
            var tweener = Create<TweenInteger>()
                .SetFromGetter(fromGetter)
                .SetToGetter(toGetter)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenInteger;
            return tweener;
        } 

        #endregion
    } 

    #endregion
}