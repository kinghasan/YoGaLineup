using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aya.TweenPro
{
    public static partial class TaskExtension
    {
        #region TweenData
        
        public static async Task AsyncWaitForPlaying(this TweenData tweenData)
        {
            var task = WaitTask(() => !tweenData.IsPlaying);
            await task;
        }

        public static async Task AsyncWaitForStart(this TweenData tweenData)
        {
            var task = WaitTask(() => !tweenData.IsPlaying || (tweenData.IsPlaying && tweenData.IsDelaying));
            await task;
        }

        public static async Task AsyncWaitForComplete(this TweenData tweenData)
        {
            var task = WaitTask(() => !tweenData.IsCompleted);
            await task;
        }

        public static async Task AsyncWaitForDuration(this TweenData tweenData, float duration)
        {
            var task = WaitTask(() => tweenData.PlayTimer < duration);
            await task;
        }

        public static async Task AsyncWaitForNormalizedDuration(this TweenData tweenData, float normalizedDuration)
        {
            var task = WaitTask(() => tweenData.RuntimeNormalizedProgress < normalizedDuration);
            await task;
        }

        public static async Task AsyncWaitForElapsedLoops(this TweenData tweenData, int loop)
        {
            var task = WaitTask(() => tweenData.LoopCounter < loop);
            await task;
        }

        #endregion

        #region Tweener

        public static async Task AsyncWaitForPlaying(this Tweener tweener)
        {
            var task = tweener.Data.AsyncWaitForPlaying();
            await task;
        }

        public static async Task AsyncWaitForStart(this Tweener tweener)
        {
            var task = tweener.Data.AsyncWaitForStart();
            await task;
        }

        public static async Task AsyncWaitForComplete(this Tweener tweener)
        {
            var task = tweener.Data.AsyncWaitForComplete();
            await task;
        }

        public static async Task AsyncWaitForDuration(this Tweener tweener, float duration)
        {
            var task = tweener.Data.AsyncWaitForDuration(duration);
            await task;
        }

        public static async Task AsyncWaitForNormalizedDuration(this Tweener tweener, float normalizedDuration)
        {
            var task = tweener.Data.AsyncWaitForNormalizedDuration(normalizedDuration);
            await task;
        }

        public static async Task AsyncWaitForElapsedLoops(this Tweener tweener, int loop)
        {
            var task = tweener.Data.AsyncWaitForElapsedLoops(loop);
            await task;
        }

        #endregion

        #region Internal

        internal static Task WaitTask(Func<bool> keepWaiting)
        {
            var task = Task.Run(() =>
            {
                while (keepWaiting())
                {
                    Thread.Sleep(1);
                }
            });

            return task;
        } 

        #endregion
    }
}
