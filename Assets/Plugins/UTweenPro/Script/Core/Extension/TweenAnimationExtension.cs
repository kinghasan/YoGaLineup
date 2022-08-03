
namespace Aya.TweenPro
{
    public static class TweenAnimationExtension
    {
        #region Play / Pasue / Resume / Stop

        public static UTweenAnimation Play(this UTweenAnimation tweenAnimation, bool forward = true)
        {
            tweenAnimation.Data?.Play(forward);
            return tweenAnimation;
        }

        public static UTweenAnimation Pause(this UTweenAnimation tweenAnimation)
        {
            tweenAnimation.Data?.Pause();
            return tweenAnimation;
        }

        public static UTweenAnimation Resume(this UTweenAnimation tweenAnimation)
        {
            tweenAnimation.Data?.Resume();
            return tweenAnimation;
        }

        public static UTweenAnimation Stop(this UTweenAnimation tweenAnimation)
        {
            tweenAnimation.Data?.Stop();
            return tweenAnimation;
        }

        public static UTweenAnimation PlayForward(this UTweenAnimation tweenAnimation)
        {
            tweenAnimation.Data?.PlayForward();
            return tweenAnimation;
        }

        public static UTweenAnimation PlayBackward(this UTweenAnimation tweenAnimation)
        {
            tweenAnimation.Data?.PlayBackward();
            return tweenAnimation;
        }

        #endregion
    }
}