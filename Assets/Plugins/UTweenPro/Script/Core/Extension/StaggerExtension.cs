using System;
using System.Collections.Generic;

namespace Aya.TweenPro
{
    public static partial class UTween
    {
        public static TweenData Stagger<TTweener, TTarget, TValue>(IEnumerable<TTarget> targets, TValue from, TValue to, float duration, float delayOffset, Action<int, TTweener> setter = null)
            where TTweener : Tweener<TTarget, TValue>
            where TTarget : UnityEngine.Object
        {
            var tweenData = CreateTweenData();
            var index = 0;
            foreach (var target in targets)
            {
                var tweener = Pool.Spawn<TTweener>();
                tweener.Reset();
                tweenData.AddTweener(tweener);
                tweener.SetTarget(target)
                    .SetFrom(from)
                    .SetTo(to)
                    .SetDelay(delayOffset * index)
                    .SetDuration(duration);
                setter?.Invoke(index, tweener);
                index++;
            }

            tweenData.AdaptDuration()
                .Play();
            return tweenData;
        }
    }
}