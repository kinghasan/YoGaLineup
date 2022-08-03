using System;

namespace Aya.TweenPro
{
    public class TweenFloat : TweenValueFloat<UnityEngine.Object>
    {
        public override float Value { get; set; }
    }

    #region Extension
    
    public static partial class UTween
    {
        #region float

        public static TweenFloat Value(float from, float to, float duration, Action<float> onValue)
        {
            var tweener = Create<TweenFloat>()
                .SetFrom(from)
                .SetTo(to)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenFloat;
            return tweener;
        }

        public static TweenFloat Value(Func<float> fromGetter, Func<float> toGetter, float duration, Action<float> onValue)
        {
            var tweener = Create<TweenFloat>()
                .SetFromGetter(fromGetter)
                .SetToGetter(toGetter)
                .SetValueSetter(onValue)
                .SetDuration(duration)
                .Play() as TweenFloat;
            return tweener;
        } 

        #endregion
    } 

    #endregion
}