#if ADMOB
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.Async;
using Aya.SDK;
using GoogleMobileAds.Api;

namespace Aya.AD
{
    public class AdMobSourceRewardedVideo : ADSourceBase
    {
        public override ADLocationType Type
        {
            get { return ADLocationType.RewardedVideo; }
        }

        private RewardedAd _rewardedVideo;
        private string _rewardedVideoID;
        private Action<bool> _onLoadedCallback;
        private Action<bool> _onShowedCallback;

        public override bool IsReady
        {
            get { return _rewardedVideo != null && _rewardedVideo.IsLoaded(); }
        }

        public override void Init(params object[] args)
        {
            if (IsInited) return;
            _rewardedVideoID = args[0] as string;

            IsInited = true;
            OnInited(true);
            SDKDebug.Log("AD", Name + "\tInit Success");
        }

        public override void Load(Action<bool> onDone = null)
        {
            if (!IsInited) return;
            if (IsLoading) return;
            _onLoadedCallback = onDone;

            // AdMob 新激励视频API，必须重新创建才能获取到资源
            _rewardedVideo = new RewardedAd(_rewardedVideoID);
            // Callback
            _rewardedVideo.OnAdLoaded += OnAdLoaded;
            _rewardedVideo.OnAdFailedToLoad += OnAdFailedToLoad;
            _rewardedVideo.OnAdOpening += OnAdOpening;
            _rewardedVideo.OnAdClosed += OnAdClosed;
            _rewardedVideo.OnAdFailedToShow += OnAdFailedToShow;
            _rewardedVideo.OnUserEarnedReward += OnUserEarnedReward;
            var request = new AdRequest.Builder().Build();
            IsLoading = true;
            _rewardedVideo.LoadAd(request);
            SDKDebug.Log("AD", Name + "\tLoad Start");
        }

        public override void Show(Action<bool> onDone = null)
        {
            if (!IsInited) return;
            if (IsShowing) return;
            _onShowedCallback = onDone;

            IsShowing = true;
            _rewardedVideo.Show();
            SDKDebug.Log("AD", Name + "\tShow Request");

            // 防止 Close 回调不触发，修改Showing状态
            UnityThread.ExecuteDelay(() => { IsShowing = false; }, 10f);
#if UNITY_EDITOR
            OnResult(true);
            if (_onShowedCallback != null)
            {
                IsShowing = false;
                _onShowedCallback(true);     
            }
#endif
        }

        public override void Close()
        {
        }

        #region Callback

        protected void OnAdLoaded(object sender, EventArgs args)
        {
            UnityThread.ExecuteUpdate(() =>
            {
                IsLoading = false;
                OnLoaded(true);
                if (_onLoadedCallback != null)
                {
                    _onLoadedCallback(true);
                }

                SDKDebug.Log("AD", Name + "\tLoad Success");
            });
        }

        protected void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            UnityThread.ExecuteUpdate(() =>
            {
                IsLoading = false;
                OnLoaded(false);
                if (_onLoadedCallback != null)
                {
                    _onLoadedCallback(false);
                }
                SDKDebug.Error("AD", Name + "\tLoad Fail   " + args.LoadAdError);
                UnityThread.ExecuteDelay(() => Load(), 5f);
            });
        }

        protected void OnAdOpening(object sender, EventArgs args)
        {
            UnityThread.ExecuteUpdate(() =>
            {
                IsShowing = true;
                OnShowed();
                SDKDebug.Log("AD", Name + "\tShow Success");
            });
        }

        protected void OnAdClosed(object sender, EventArgs args)
        {
            UnityThread.ExecuteUpdate(() =>
            {
                IsShowing = false;
                OnCloseed();
                IsLoading = false;
                SDKDebug.Log("AD", Name + "\tClose");
                UnityThread.ExecuteDelay(() => Load(), 5f);
            });
        }

        protected void OnAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            UnityThread.ExecuteUpdate(() =>
            {
                IsShowing = false;
                OnResult(false);
                if (_onShowedCallback != null)
                {
                    _onShowedCallback(false);
                }
                SDKDebug.Error("AD", Name + "\tShow Fail   " + args.Message);
            });
        }

        protected void OnUserEarnedReward(object sender, EventArgs args)
        {
            UnityThread.ExecuteUpdate(() =>
            {
                IsShowing = false;
                OnResult(true);
                if (_onShowedCallback != null)
                {
                    _onShowedCallback(true);
                }
                SDKDebug.Log("AD", Name + "\tReward Success");
            });
        }
        #endregion
    }
}
#endif