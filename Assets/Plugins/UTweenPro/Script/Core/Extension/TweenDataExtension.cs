using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.TweenPro
{
    public static partial class TweenDataExtension
    {
        #region Set Target

        public static TweenData SetTarget<T>(this TweenData tweenData, T target) where T : UnityEngine.Object
        {
            foreach (var tweener in tweenData.TweenerList)
            {
                if (tweener is Tweener<T> tempTweener)
                {
                    tempTweener.SetTarget(target);
                }
            }

            return tweenData;
        }

        public static TweenData SetTarget<T>(this TweenData tweenData, int index, T target) where T : UnityEngine.Object
        {
            var tweener = tweenData.TweenerList[index];
            if (tweener is Tweener<T> tempTweener)
            {
                tempTweener.SetTarget(target);
            }

            return tweenData;
        }

        public static TweenData SetTarget<T>(this TweenData tweenData, params T[] targets) where T : UnityEngine.Object
        {
            var index = 0;
            foreach (var tweener in tweenData.TweenerList)
            {
                if (tweener is Tweener<T> tempTweener)
                {
                    if (index >= targets.Length) break;
                    var target = targets[index];
                    tempTweener.SetTarget(target);
                    index++;
                }
            }

            return tweenData;
        }


        #endregion

        #region Set Property

        public static TweenData SetIdentifier(this TweenData tweenData, string identifier)
        {
            tweenData.Identifier = identifier;
            return tweenData;
        }

        public static TweenData SetDuration(this TweenData tweenData, float duration)
        {
            tweenData.Duration = duration;
            return tweenData;
        }

        public static TweenData SetDelay(this TweenData tweenData, float delay)
        {
            tweenData.Delay = delay;
            return tweenData;
        }

        public static TweenData SetBackward(this TweenData tweenData, bool backward)
        {
            tweenData.Backward = backward;
            return tweenData;
        }

        public static TweenData SetPlayMode(this TweenData tweenData, PlayMode playMode)
        {
            tweenData.PlayMode = playMode;
            if (playMode == PlayMode.Once) tweenData.PlayCount = 1;
            return tweenData;
        }

        public static TweenData SetPlayCount(this TweenData tweenData, int playCount)
        {
            tweenData.PlayCount = playCount;
            return tweenData;
        }

        public static TweenData SetPlayOnce(this TweenData tweenData)
        {
            tweenData.PlayMode = PlayMode.Once;
            tweenData.PlayCount = 1;
            return tweenData;
        }

        public static TweenData SetLoopCount(this TweenData tweenData, int playCount)
        {
            tweenData.PlayMode = PlayMode.Loop;
            tweenData.PlayCount = playCount;
            return tweenData;
        }

        public static TweenData SetLoopUnlimited(this TweenData tweenData)
        {
            tweenData.PlayMode = PlayMode.Loop;
            tweenData.PlayCount = -1;
            return tweenData;
        }

        public static TweenData SetPingPongCount(this TweenData tweenData, int playCount)
        {
            tweenData.PlayMode = PlayMode.PingPong;
            tweenData.PlayCount = playCount;
            return tweenData;
        }

        public static TweenData SetPingPongUnlimited(this TweenData tweenData)
        {
            tweenData.PlayMode = PlayMode.PingPong;
            tweenData.PlayCount = -1;
            return tweenData;
        }

        public static TweenData SetAutoPlay(this TweenData tweenData, AutoPlayMode autoPlay)
        {
            tweenData.AutoPlay = autoPlay;
            return tweenData;
        }

        public static TweenData SetUpdateMode(this TweenData tweenData, UpdateMode updateMode)
        {
            tweenData.UpdateMode = updateMode;
            return tweenData;
        }

        public static TweenData SetInterval(this TweenData tweenData, float interval)
        {
            tweenData.Interval = interval;
            return tweenData;
        }

        public static TweenData SetInterval(this TweenData tweenData, float interval, float interval2)
        {
            tweenData.Interval = interval;
            tweenData.Interval2 = interval2;
            return tweenData;
        }

        public static TweenData SetTimeMode(this TweenData tweenData, TimeMode timeMode)
        {
            tweenData.TimeMode = timeMode;
            return tweenData;
        }

        public static TweenData SetSelfScale(this TweenData tweenData, float selfScale)
        {
            tweenData.SelfScale = selfScale;
            return tweenData;
        }

        public static TweenData SetAutoKill(this TweenData tweenData, bool autoKill)
        {
            tweenData.AutoKill = autoKill;
            return tweenData;
        }

        public static TweenData SetPreSample(this TweenData tweenData, PreSampleMode preSampleMode)
        {
            tweenData.PreSampleMode = preSampleMode;
            return tweenData;
        }

        public static TweenData SetSpeedBased(this TweenData tweenData, bool speedBased = true)
        {
            tweenData.SpeedBased = speedBased;
            return tweenData;
        }

        public static TweenData AdaptDuration(this TweenData tweenData)
        {
            var duration = float.MinValue;
            foreach (var tweener in tweenData.TweenerList)
            {
                if (tweener.TotalDuration > duration) duration = tweener.TotalDuration;
            }

            tweenData.Duration = duration;
            return tweenData;
        }

        #endregion

        #region Set Stats

        public static TweenData SetProgress(this TweenData tweenData, float progress)
        {
            tweenData.PlayTimer = progress;
            tweenData.Update(0f);
            return tweenData;
        }

        public static TweenData SetNormalizedProgress(this TweenData tweenData, float normalizedProgress)
        {
            normalizedProgress = Mathf.Clamp01(normalizedProgress);
            var progress = tweenData.RuntimeDuration * normalizedProgress;
            tweenData.SetProgress(progress);
            return tweenData;
        }

        #endregion

        #region Set Event

        public static TweenData SetOnPlay(this TweenData tweenData, Action onPlay)
        {
            tweenData.OnPlay.AddListener(onPlay);
            return tweenData;
        }

        public static TweenData SetOnUpdate(this TweenData tweenData, Action onPlay)
        {
            tweenData.OnUpdate.AddListener(onPlay);
            return tweenData;
        }

        public static TweenData SetOnLoopStart(this TweenData tweenData, Action onLoopStart)
        {
            tweenData.OnLoopStart.AddListener(onLoopStart);
            return tweenData;
        }

        public static TweenData SetOnLoopEnd(this TweenData tweenData, Action onLoopEnd)
        {
            tweenData.OnLoopEnd.AddListener(onLoopEnd);
            return tweenData;
        }

        public static TweenData SetOnPause(this TweenData tweenData, Action onPlay)
        {
            tweenData.OnPause.AddListener(onPlay);
            return tweenData;
        }

        public static TweenData SetOnResume(this TweenData tweenData, Action onPlay)
        {
            tweenData.OnResume.AddListener(onPlay);
            return tweenData;
        }

        public static TweenData SetOnStop(this TweenData tweenData, Action onPlay)
        {
            tweenData.OnStop.AddListener(onPlay);
            return tweenData;
        }

        public static TweenData SetOnComplete(this TweenData tweenData, Action onPlay)
        {
            tweenData.OnComplete.AddListener(onPlay);
            return tweenData;
        }

        #endregion

        #region Get Tweener

        public static TTweener GetTweener<TTweener>(this TweenData tweenData) where TTweener : Tweener
        {
            foreach (var tweener in tweenData.TweenerList)
            {
                if (tweener is TTweener result) return result;
            }

            return default;
        }

        public static List<TTweener> GetTweeners<TTweener>(this TweenData tweenData) where TTweener : Tweener
        {
            var result = new List<TTweener>();
            foreach (var tweener in tweenData.TweenerList)
            {
                if (tweener is TTweener temp)
                {
                    result.Add(temp);
                }
            }

            return result;
        }

        #endregion

        #region Append / Join / Insert

        public static TweenData Append(this TweenData tweenData, Tweener tweener)
        {
            tweener.Delay = tweenData.Duration;
            tweenData.Duration += tweener.Duration;
            tweenData.AddTweener(tweener);
            return tweenData;
        }

        public static TweenData AppendInterval(this TweenData tweenData, float interval)
        {
            tweenData.Duration += interval;
            return tweenData;
        }

        public static TweenData Join(this TweenData tweenData, Tweener tweener)
        {
            Insert(tweenData, 0f, tweener);
            return tweenData;
        }

        public static TweenData Insert(this TweenData tweenData, float delay, Tweener tweener)
        {
            tweener.Delay = delay;
            tweenData.AddTweener(tweener);
            tweenData.AdaptDuration();
            return tweenData;
        }

        #endregion
    }
}