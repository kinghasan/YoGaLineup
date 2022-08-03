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
    public class AdMobSourceInterstitial : ADSourceBase
    {
        public override ADLocationType Type
        {
            get { return ADLocationType.Interstitial; }
        }

        private InterstitialAd _interstitial;
        private string _interstitialID;
        private Action<bool> _onLoadedCallback;
        private Action<bool> _onShowedCallback;

        public override bool IsReady
        {
            get { return _interstitial != null && _interstitial.IsLoaded(); }
        }

        public override void Init(params object[] args)
        {
            if(IsInited) return;
            _interstitialID = args[0] as string;
            _interstitial = new InterstitialAd(_interstitialID);

            // Callback
            _interstitial.OnAdLoaded += OnAdLoaded;
            _interstitial.OnAdFailedToLoad += OnAdFailedToLoad;
            _interstitial.OnAdOpening += OnAdOpening;
            _interstitial.OnAdClosed += OnAdClosed;

            IsInited = true;
            OnInited(true);
            SDKDebug.Log("AD", Name + "\tInit Success");
        }

        public override void Load(Action<bool> onDone = null)
        {
            if (!IsInited) return;
            if (IsLoading) return;
            var request = new AdRequest.Builder().Build();
            _onLoadedCallback = onDone;
            IsLoading = true;
            _interstitial.LoadAd(request);
            SDKDebug.Log("AD", Name + "\tLoad Start");
        }

        public override void Show(Action<bool> onDone = null)
        {
            if (!IsInited) return;
            _onShowedCallback = onDone;
            _interstitial.Show();
            SDKDebug.Log("AD", Name + "\tShow Request");
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

        protected void OnAdFailedToLoad(object sender, EventArgs args)
        {
            UnityThread.ExecuteUpdate(() =>
            {
                IsLoading = false;
                OnLoaded(false);
                if (_onLoadedCallback != null)
                {
                    _onLoadedCallback(false);
                }
                SDKDebug.Error("AD", Name + "\tLoad Fail");
                UnityThread.ExecuteDelay(() => Load(), 5f);
            });
        }

        protected void OnAdOpening(object sender, EventArgs args)
        {
            UnityThread.ExecuteUpdate(() =>
            {
                OnShowed();
                if (_onShowedCallback != null)
                {
                    _onShowedCallback(true);
                }
                SDKDebug.Log("AD", Name + "\tShow Success");
            });
        }

        protected void OnAdClosed(object sender, EventArgs args)
        {
            UnityThread.ExecuteUpdate(() =>
            {
                OnCloseed();
                SDKDebug.Log("AD", Name + "\tClose");
                UnityThread.ExecuteDelay(() => Load(), 5f);
            });
        } 
        #endregion
    }
}
#endif