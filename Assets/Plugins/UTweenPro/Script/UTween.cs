using Object = UnityEngine.Object;

namespace Aya.TweenPro
{
    public static partial class UTween
    {
        #region Create / Play Single

        public static TTweener Play<TTweener, TTarget>(TTarget target, float duration)
            where TTweener : Tweener<TTarget>, new()
            where TTarget : Object
        {
            var tweener = Create<TTweener>()
                .SetTarget(target)
                .SetDuration(duration)
                .Play();
            return tweener;
        }

        public static TTweener Play<TTweener, TTarget, TValue>(TTarget target, TValue to, float duration)
            where TTweener : Tweener<TTarget, TValue>, new()
            where TTarget : Object
        {
            var tweener = Create<TTweener>()
                .SetTarget(target)
                .SetDuration(duration)
                .SetCurrent2From()
                .SetTo(to)
                .Play() as TTweener;
            return tweener;
        }

        public static TTweener Play<TTweener, TTarget, TValue>(TTarget target, TValue from, TValue to, float duration)
            where TTweener : Tweener<TTarget, TValue>, new()
            where TTarget : Object
        {
            var tweener = Create<TTweener>()
                .SetTarget(target)
                .SetDuration(duration)
                .SetFrom(from)
                .SetTo(to)
                .Play() as TTweener;
            return tweener;
        }

        public static TTweener Create<TTweener>() where TTweener : Tweener, new()
        {
            var tweenData = CreateTweenData();
            var tweener = CreateWithoutData<TTweener>();
            tweenData.AddTweener(tweener);
            return tweener;
        }

        public static TTweener CreateWithoutData<TTweener>() where TTweener : Tweener, new()
        {
            var tweener = Pool.Spawn<TTweener>();
            tweener.Reset();
            return tweener;
        }

        public static TweenData CreateTweenData()
        {
            var tweenData = Pool.Spawn<TweenData>();
            tweenData.Reset();
            tweenData.ControlMode = TweenControlMode.TweenManager;
            return tweenData;
        }

        #endregion

        #region Stop

        public static void Stop<TTarget>(TTarget target) where TTarget : Object
        {
            foreach (var tweenData in UTweenManager.Ins.PlayingList)
            {
                foreach (var tweener in tweenData.TweenerList)
                {
                    if (tweener is Tweener<TTarget> tweenerTemp)
                    {
                        if (tweenerTemp.Target != target) continue;
                        tweenerTemp.Stop();
                        break;
                    }
                }
            }
        }

        public static void Stop<TTarget, TTweener>(TTarget target)
            where TTarget : Object
            where TTweener : Tweener<TTarget>
        {
            foreach (var tweenData in UTweenManager.Ins.PlayingList)
            {
                foreach (var tweener in tweenData.TweenerList)
                {
                    if (tweener is TTweener tweenerTemp)
                    {
                        if (tweenerTemp.Target != target) continue;
                        tweenerTemp.Stop();
                        break;
                    }
                }
            }
        }

        #endregion
    }
}