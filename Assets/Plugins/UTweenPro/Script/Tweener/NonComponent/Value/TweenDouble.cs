using System;

namespace Aya.TweenPro
{
    public class TweenDouble : TweenValueDouble<UnityEngine.Object>
    {
        public override double Value { get; set; }
    }

    #region Extension
    
    public static partial class UTween
    {
        #region double

        public static TweenDouble Value(double from, double to, float duration, Action<double> onValue)
        {
            var tweener = Create<TweenDouble>()
                .SetFrom(from)
                .SetTo(to)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenDouble;
            return tweener;
        }

        public static TweenDouble Value(Func<double> fromGetter, Func<double> toGetter, float duration, Action<double> onValue)
        {
            var tweener = Create<TweenDouble>()
                .SetFromGetter(fromGetter)
                .SetToGetter(toGetter)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenDouble;
            return tweener;
        }

        #endregion
    } 

    #endregion
}