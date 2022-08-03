using System;

namespace Aya.TweenPro
{
    public class TweenString : TweenValueString<UnityEngine.Object>
    {
        public override string Value { get; set; }
    }

    #region Extension
    
    public static partial class UTween
    {
        public static TweenString Value(string from, string to, float duration, Action<string> onValue)
        {
            var tweener = Create<TweenString>()
                .SetFrom(from)
                .SetTo(to)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenString;
            return tweener;
        }

        public static TweenString Value(Func<string> fromGetter, Func<string> toGetter, float duration, Action<string> onValue)
        {
            var tweener = Create<TweenString>()
                .SetFromGetter(fromGetter)
                .SetToGetter(toGetter)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenString;
            return tweener;
        }
    } 

    #endregion
}