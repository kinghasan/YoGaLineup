using System;
using UnityEngine;

namespace Aya.TweenPro
{
    public static partial class TweenerExtension
    {
        #region Set Property
        
        public static TTweener SetActive<TTweener>(this TTweener tweener, bool active) where TTweener : Tweener
        {
            tweener.Active = active;
            return tweener;
        }

        public static TTweener SetDuration<TTweener>(this TTweener tweener, float duration) where TTweener : Tweener
        {
            tweener.Duration = duration;
            if (tweener.SingleMode)
            {
                tweener.Data.Duration = tweener.TotalDuration;
            }

            return tweener;
        }

        public static TTweener SetDelay<TTweener>(this TTweener tweener, float delay) where TTweener : Tweener
        {
            tweener.Delay = delay;
            if (tweener.SingleMode)
            {
                tweener.Data.Duration = tweener.TotalDuration;
            }

            return tweener;
        }

        public static TTweener SetHoldStart<TTweener>(this TTweener tweener, bool holdStart) where TTweener : Tweener
        {
            tweener.HoldStart = holdStart;
            return tweener;
        }

        public static TTweener SetHoldEnd<TTweener>(this TTweener tweener, bool holdEnd) where TTweener : Tweener
        {
            tweener.HoldEnd = holdEnd;
            return tweener;
        }

        public static TTweener SetEase<TTweener>(this TTweener tweener, int easeType) where TTweener : Tweener
        {
            var easeFunction = EaseType.FunctionDic[easeType];
            tweener.Ease = easeType;
            tweener.Strength = easeFunction.DefaultStrength;
            return tweener;
        }

        public static TTweener SetStrength<TTweener>(this TTweener tweener, float strength) where TTweener : Tweener
        {
            tweener.Strength = Mathf.Clamp01(strength);
            return tweener;
        }

        public static TTweener SetCurve<TTweener>(this TTweener tweener, AnimationCurve curve) where TTweener : Tweener
        {
            tweener.Ease = EaseType.Custom;
            tweener.Curve = curve;
            return tweener;
        }

        public static TTweener SetSpace<TTweener>(this TTweener tweener, SpaceMode spaceMode) where TTweener : Tweener
        {
            if (!tweener.SupportSpace) return tweener;
            tweener.Space = spaceMode;
            return tweener;
        }

        #endregion

        #region Set Data Property

        public static TTweener SetIdentifier<TTweener>(this TTweener tweener, string identifier) where TTweener : Tweener
        {
            tweener.Data.SetIdentifier(identifier);
            return tweener;
        }

        public static TTweener SetDataDuration<TTweener>(this TTweener tweener, float duration) where TTweener : Tweener
        {
            tweener.Data.SetDuration(duration);
            return tweener;
        }

        public static TTweener SetDataDelay<TTweener>(this TTweener tweener, float delay) where TTweener : Tweener
        {
            tweener.Data.SetDelay(delay);
            return tweener;
        }
        public static TTweener SetBackward<TTweener>(this TTweener tweener, bool backward) where TTweener : Tweener
        {
            tweener.Data.Backward = backward;
            return tweener;
        }

        public static TTweener SetPlayMode<TTweener>(this TTweener tweener, PlayMode playMode) where TTweener : Tweener
        {
            tweener.Data.SetPlayMode(playMode);
            return tweener;
        }

        public static TTweener SetPlayCount<TTweener>(this TTweener tweener, int playCount) where TTweener : Tweener
        {
            tweener.Data.SetPlayCount(playCount);
            return tweener;
        }

        public static TTweener SetPlayOnce<TTweener>(this TTweener tweener) where TTweener : Tweener
        {
            tweener.Data.SetPlayOnce();
            return tweener;
        }

        public static TTweener SetLoopCount<TTweener>(this TTweener tweener, int playCount) where TTweener : Tweener
        {
            tweener.Data.SetLoopCount(playCount);
            return tweener;
        }

        public static TTweener SetLoopUnlimited<TTweener>(this TTweener tweener) where TTweener : Tweener
        {
            tweener.Data.SetLoopUnlimited();
            return tweener;
        }

        public static TTweener SetPingPongCount<TTweener>(this TTweener tweener, int playCount) where TTweener : Tweener
        {
            tweener.Data.SetPingPongCount(playCount);
            return tweener;
        }

        public static TTweener SetPingPongUnlimited<TTweener>(this TTweener tweener) where TTweener : Tweener
        {
            tweener.Data.SetPingPongUnlimited();
            return tweener;
        }

        public static TTweener SetAutoPlay<TTweener>(this TTweener tweener, AutoPlayMode autoPlay) where TTweener : Tweener
        {
            tweener.Data.SetAutoPlay(autoPlay);
            return tweener;
        }

        public static TTweener SetUpdateMode<TTweener>(this TTweener tweener, UpdateMode updateMode) where TTweener : Tweener
        {
            tweener.Data.SetUpdateMode(updateMode);
            return tweener;
        }

        public static TTweener SetInterval<TTweener>(this TTweener tweener, float interval) where TTweener : Tweener
        {
            tweener.Data.SetInterval(interval);
            return tweener;
        }

        public static TTweener SetInterval<TTweener>(this TTweener tweener, float interval, float interval2) where TTweener : Tweener
        {
            tweener.Data.SetInterval(interval, interval2);
            return tweener;
        }

        public static TTweener SetTimeMode<TTweener>(this TTweener tweener, TimeMode timeMode) where TTweener : Tweener
        {
            tweener.Data.SetTimeMode(timeMode);
            return tweener;
        }

        public static TTweener SetSelfScale<TTweener>(this TTweener tweener, float selfScale) where TTweener : Tweener
        {
            tweener.Data.SetSelfScale(selfScale);
            return tweener;
        }

        public static TTweener SetAutoKill<TTweener>(this TTweener tweener, bool autoKill) where TTweener : Tweener
        {
            tweener.Data.SetAutoKill(autoKill);
            return tweener;
        }

        public static TTweener SetPreSample<TTweener>(this TTweener tweener, PreSampleMode preSampleMode) where TTweener : Tweener
        {
            tweener.Data.SetPreSample(preSampleMode);
            return tweener;
        }

        public static TTweener SetSpeedBased<TTweener>(this TTweener tweener, bool speedBased = true) where TTweener : Tweener
        {
            tweener.Data.SetSpeedBased(speedBased);
            return tweener;
        }

        #endregion

        #region Play / Pasue / Resume / Stop

        public static TTweener Play<TTweener>(this TTweener tweener, bool forward = true) where TTweener : Tweener
        {
            if (tweener.Data == null)
            {
                var tweenData = UTween.CreateTweenData();
                tweenData.AddTweener(tweener);
            }

            tweener.Data?.Play(forward);
            return tweener;
        }

        public static TTweener Pause<TTweener>(this TTweener tweener) where TTweener : Tweener
        {
            tweener.Data?.Pause();
            return tweener;
        }

        public static TTweener Resume<TTweener>(this TTweener tweener) where TTweener : Tweener
        {
            tweener.Data?.Resume();
            return tweener;
        }

        public static TTweener Stop<TTweener>(this TTweener tweener) where TTweener : Tweener
        {
            tweener.Data?.Stop();
            return tweener;
        }

        public static TTweener PlayForward<TTweener>(this TTweener tweener) where TTweener : Tweener
        {
            tweener.Data?.PlayForward();
            return tweener;
        }

        public static TTweener PlayBackward<TTweener>(this TTweener tweener) where TTweener : Tweener
        {
            tweener.Data?.PlayBackward();
            return tweener;
        }

        #endregion

        #region Add / Remove Tweener

        public static TTweener AddTweener<TTweener>(this TTweener tweener, Tweener otherTweener) where TTweener : Tweener
        {
            tweener.Data.AddTweener(otherTweener);
            return tweener;
        }

        public static TTweener RemoveTweener<TTweener>(this TTweener tweener, Tweener otherTweener) where TTweener : Tweener
        {
            tweener.Data.RemoveTweener(otherTweener);
            return tweener;
        }

        #endregion

        #region Append / Join / Insert

        public static TTweener Append<TTweener>(this TTweener tweener, Tweener otherTweener) where TTweener : Tweener
        {
            tweener.Data.Append(otherTweener);
            return tweener;
        }

        public static TTweener AppendInterval<TTweener>(this TTweener tweener, float interval) where TTweener : Tweener
        {
            tweener.Data.AppendInterval(interval);
            return tweener;
        }

        public static TTweener Join<TTweener>(this TTweener tweener, Tweener otherTweener) where TTweener : Tweener
        {
            tweener.Data.Join(otherTweener);
            return tweener;
        }

        public static TTweener Insert<TTweener>(this TTweener tweener, float delay, Tweener otherTweener) where TTweener : Tweener
        {
            tweener.Data.Insert(delay, otherTweener);
            return tweener;
        }

        #endregion

        #region Set Data Event

        public static TTweener SetOnPlay<TTweener>(this TTweener tweener, Action onPlay) where TTweener : Tweener
        {
            tweener.Data.OnPlay.AddListener(onPlay);
            return tweener;
        }

        public static TTweener SetOnUpdate<TTweener>(this TTweener tweener, Action onPlay) where TTweener : Tweener
        {
            tweener.Data.OnUpdate.AddListener(onPlay);
            return tweener;
        }

        public static TTweener SetOnLoopStart<TTweener>(this TTweener tweener, Action onLoopStart) where TTweener : Tweener
        {
            tweener.Data.OnLoopStart.AddListener(onLoopStart);
            return tweener;
        }

        public static TTweener SetOnLoopEnd<TTweener>(this TTweener tweener, Action onLoopEnd) where TTweener : Tweener
        {
            tweener.Data.OnLoopEnd.AddListener(onLoopEnd);
            return tweener;
        }

        public static TTweener SetOnPause<TTweener>(this TTweener tweener, Action onPlay) where TTweener : Tweener
        {
            tweener.Data.OnPause.AddListener(onPlay);
            return tweener;
        }

        public static TTweener SetOnResume<TTweener>(this TTweener tweener, Action onPlay) where TTweener : Tweener
        {
            tweener.Data.OnResume.AddListener(onPlay);
            return tweener;
        }

        public static TTweener SetOnStop<TTweener>(this TTweener tweener, Action onPlay) where TTweener : Tweener
        {
            tweener.Data.OnStop.AddListener(onPlay);
            return tweener;
        }

        public static TTweener SetOnComplete<TTweener>(this TTweener tweener, Action onPlay) where TTweener : Tweener
        {
            tweener.Data.OnComplete.AddListener(onPlay);
            return tweener;
        }

        #endregion
    }
}