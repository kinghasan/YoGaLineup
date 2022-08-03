using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace Aya.TweenPro
{
    public static partial class CoroutineExtension
    {
        #region TweenData
        
        public static IEnumerator WaitForPlaying(this TweenData tweenData)
        {
            return new WaitForPlaying(tweenData);
        }

        public static IEnumerator WaitForStart(this TweenData tweenData)
        {
            return new WaitForStart(tweenData);
        }

        public static IEnumerator WaitForComplete(this TweenData tweenData)
        {
            return new WaitForComplete(tweenData);
        }

        public static IEnumerator WaitForDuration(this TweenData tweenData, float duration)
        {
            return new WaitForDuration(tweenData, duration);
        }

        public static IEnumerator WaitForNormalizedDuration(this TweenData tweenData, float normalizedDuration)
        {
            return new WaitForNormalizedDuration(tweenData, normalizedDuration);
        }

        public static IEnumerator WaitForElapsedLoops(this TweenData tweenData, int loop)
        {
            return new WaitForElapsedLoops(tweenData, loop);
        }

        #endregion

        #region Tweener

        public static IEnumerator WaitForPlaying(this Tweener tweener)
        {
            return new WaitForPlaying(tweener.Data);
        }

        public static IEnumerator WaitForStart(this Tweener tweener)
        {
            return new WaitForStart(tweener.Data);
        }

        public static IEnumerator WaitForComplete(this Tweener tweener)
        {
            return new WaitForComplete(tweener.Data);
        }

        public static IEnumerator WaitForDuration(this Tweener tweener, float duration)
        {
            return new WaitForDuration(tweener.Data, duration);
        }

        public static IEnumerator WaitForNormalizedDuration(this Tweener tweener, float normalizedDuration)
        {
            return new WaitForNormalizedDuration(tweener.Data, normalizedDuration);
        }

        public static IEnumerator WaitForElapsedLoops(this Tweener tweener, int loop)
        {
            return new WaitForElapsedLoops(tweener.Data, loop);
        }

        #endregion
    }
}