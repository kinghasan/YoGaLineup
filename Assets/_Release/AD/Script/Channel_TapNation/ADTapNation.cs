#if TapNation
using System;
using System.Collections;
using System.Collections.Generic;
using Aya.Analysis;
using Aya.Async;
using Aya.SDK;
using UnityEngine;

namespace Aya.AD
{
    public class ADTapNation : ADChannelBase<ADTapNationLocationBanner, ADTapNationLocationInterstitial, ADTapNationLocationRewardedVideo>
    {
        private float _lastInterstitialTimer;

        public override void Init(params object[] args)
        {
            OnInit(args);
            Load();

            _lastInterstitialTimer = Time.realtimeSinceStartup;
            OnInterstitialShowed += () =>
            {
                _lastInterstitialTimer = Time.realtimeSinceStartup;
            };

            OnRewardedVedioShowed += () =>
            {
                _lastInterstitialTimer = Time.realtimeSinceStartup;
            };

            OnRewardedVedioCloseed += () =>
            {
                _lastInterstitialTimer = Time.realtimeSinceStartup;
            };
        }

        public override void ShowInterstitial(Action<bool> onDone = null)
        {
            var time = Time.realtimeSinceStartup;
            if (time - _lastInterstitialTimer >= 40f)
            {
                _lastInterstitialTimer = time;
                base.ShowInterstitial(onDone);
            }
        }
    }

#region AD Editor Location

    public class ADTapNationLocationBanner : ADLocationBase<ADTapNationSourceBanner>
    {
    }

    public class ADTapNationLocationInterstitial : ADLocationBase<ADTapNationSourceInterstitial>
    {
    }

    public class ADTapNationLocationRewardedVideo : ADLocationBase<ADTapNationSourceRewardedVideo>
    {
    }

#endregion

#region AD Editor Source

    public class ADTapNationSourceBanner : ADTapNationSourceBase
    {
        public override ADLocationType Type => ADLocationType.Banner;
        public override void Show(Action<bool> onDone = null)
        {
            FunGamesMax.ShowBannerAd();
            base.Show(onDone);
        }

        public override void Close()
        {
            FunGamesMax.HideBannerAd();
            base.Close();
        }
    }

    public class ADTapNationSourceInterstitial : ADTapNationSourceBase
    {
        public override ADLocationType Type => ADLocationType.Interstitial;

        private Action<bool> _onDone;

        public override void Show(Action<bool> onDone = null)
        {
            _onDone = onDone;
#if UNITY_EDITOR
            OnInterstitial("success", "", 0);
#else
            FunGamesMax.ShowInterstitial(OnInterstitial);
#endif
        }

        public void OnInterstitial(string status, string argString, int argInt)
        {
            if (status == "success")
            {
                OnShowed();
                _onDone?.Invoke(true);
                OnResult(true);
                AnalysisManager.Instance.Event("Show Interstitial");
            }
            else
            {
                _onDone?.Invoke(false);
                OnResult(false);
            }
        }

        public override void Close()
        {
            FunGamesMax.HideBannerAd();
            base.Close();
        }
    }

    public class ADTapNationSourceRewardedVideo : ADTapNationSourceBase
    {
        public override ADLocationType Type => ADLocationType.RewardedVideo;

#if UNITY_EDITOR
        public override bool IsReady => true;

#else
        public override bool IsReady
        {
            get
            {
                var result = FunGamesMax.IsRewardedAdReady();
                SDKDebug.Log("RV Ready " + result);
                return result;
            }
        }
#endif

        private Action<bool> _onDone;

        public override void Show(Action<bool> onDone = null)
        {
            SDKDebug.Log("RV Show Start");
            _onDone = onDone;
#if UNITY_EDITOR
            OnRewardedVideo("success", "", 0);
            return;
#else
            FunGamesMax.ShowRewarded(OnRewardedVideo);
#endif
        }

        public void OnRewardedVideo(string status, string argString, int argInt)
        {
            if (status == "success")
            {
                SDKDebug.Log("RV Show Success");
                OnShowed();
                _onDone?.Invoke(true);
                OnResult(true);
            }
            else
            {
                SDKDebug.Log("RV Show Fail");
                _onDone?.Invoke(false);
                OnResult(false);
            }
        }
    }

    public abstract class ADTapNationSourceBase : ADSourceBase
    {
        public override ADLocationType Type => ADLocationType.None;

        public override bool IsReady => true;

        public override void Init(params object[] args)
        {
            IsInited = true;
            OnInited(true);
        }

        public override void Load(Action<bool> onDone = null)
        {
            OnLoaded(true);
            onDone?.Invoke(true);
        }

        public override void Show(Action<bool> onDone = null)
        {
            OnShowed();
            onDone?.Invoke(true);
            OnResult(true);
        }

        public override void Close()
        {
            OnCloseed();
        }
    }

#endregion
}

#endif