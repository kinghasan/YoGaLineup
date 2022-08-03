using System;

namespace Aya.TweenPro
{
    public class TweenBoolean : TweenValueBoolean<UnityEngine.Object>
    {
        public override bool Value { get; set; }
    }

    #region Extension
    
    public static partial class UTween
    {
        #region bool

        public static TweenBoolean Value(bool from, bool to, float duration, Action<bool> onValue)
        {
            var tweener = Create<TweenBoolean>()
                .SetFrom(from)
                .SetTo(to)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenBoolean;
            return tweener;
        }

        public static TweenBoolean Value(Func<bool> fromGetter, Func<bool> toGetter, float duration, Action<bool> onValue)
        {
            var tweener = Create<TweenBoolean>()
                .SetFromGetter(fromGetter)
                .SetToGetter(toGetter)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenBoolean;
            return tweener;
        } 

        #endregion
    } 

    #endregion
}