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
    public class AdMobSourceBanner : ADSourceBase
    {
        public override ADLocationType Type
        {
            get { return ADLocationType.Banner; }
        }

        private BannerView _bannerView;
        private string _bannerID;
        private Action<bool> _onLoadedCallback;

        public override bool IsReady
        {
            get { return true; }
        }

        public override void Init(params object[] args)
        {
            if (IsInited) return;
            _bannerID = args[0] as string;
            IsInited = true;
            OnInited(true);
            SDKDebug.Log("AD", Name + "\tInit Success");
        }

        public override void Load(Action<bool> onDone = null)
        {
            if (!IsInited) return;
            _onLoadedCallback = onDone;
        }

        public override void Show(Action<bool> onDone = null)
        {
            if (!IsInited) return;
            if (_bannerView != null)
            {
                _bannerView.Hide();
                _bannerView.Destroy();
            }

            _bannerView = new BannerView(_bannerID, AdSize.Banner, AdPosition.Bottom);
            _bannerView.LoadAd(new AdRequest.Builder().Build());

            // Callback
            _bannerView.OnAdLoaded += OnAdLoaded;
            _bannerView.OnAdFailedToLoad += OnAdFailedToLoad;
            _bannerView.OnAdOpening += OnAdOpening;
            _bannerView.OnAdClosed += OnAdClosed;

            SDKDebug.Log("AD", Name + "\tShow Request");
        }

        public override void Close()
        {
            if (!IsInited) return;
            if (_bannerView != null)
            {
                _bannerView.Hide();
            }
        }

        #region Callback

        protected void OnAdLoaded(object sender, EventArgs args)
        {
            UnityThread.ExecuteUpdate(() =>
            {
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
                OnLoaded(false);
                if (_onLoadedCallback != null)
                {
                    _onLoadedCallback(false);
                }

                UnityThread.ExecuteDelay(() => Show(), 5f);
                SDKDebug.Error("AD", Name + "\tLoad Fail");
            });
        }

        protected void OnAdOpening(object sender, EventArgs args)
        {
            UnityThread.ExecuteUpdate(() =>
            {
                OnShowed();
                SDKDebug.Log("AD", Name + "\tShow Success");
            });
        }

        protected void OnAdClosed(object sender, EventArgs args)
        {
            UnityThread.ExecuteUpdate(() =>
            {
                OnCloseed();
                SDKDebug.Log("AD", Name + "\tClose");
            });
        } 
        #endregion
    }
}
#endif