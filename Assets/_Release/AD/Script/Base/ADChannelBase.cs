using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Aya.AD
{
    public abstract class ADChannelBase
    {
        public bool IsInit { get; protected set; }

        #region Callback

        public Action<bool> OnInited = delegate { };

        public Action OnBannerShowed = delegate { };

        public Action OnInterstitialShowed = delegate { };
        public Action OnInterstitialClosed = delegate { };

        public Action<bool> OnRewardedVideoLoaded = delegate { };
        public Action OnRewardedVedioShowed = delegate { };
        public Action OnRewardedVedioCloseed = delegate { };
        public Action<bool> OnRewardedVedioResult = delegate { };

        #endregion

        #region Cache

        public List<ADLocationBase> Banners { get; protected set; }
        public Dictionary<string, ADLocationBase> BannerDic { get; protected set; }
        public ADLocationBase Banner
        {
            get
            {
                if (Banners == null || Banners.Count < 1) return null;
                return Banners[0];
            }
        }
        public List<ADLocationBase> Interstitials { get; protected set; }
        public Dictionary<string, ADLocationBase> InterstitialDic { get; protected set; }
        public ADLocationBase Interstitial
        {
            get
            {
                if (Interstitials == null || Interstitials.Count < 1) return null;
                return Interstitials[0];
            }
        }
        public List<ADLocationBase> RewardedVideos { get; protected set; }
        public Dictionary<string, ADLocationBase> RewardedVideosDic { get; protected set; }
        public ADLocationBase RewardedVideo
        {
            get
            {
                if (RewardedVideos == null || RewardedVideos.Count < 1) return null;
                return RewardedVideos[0];
            }
        }

        #endregion

        public abstract void Init(params object[] args);
        public abstract void OnInit(params object[] args);

        public abstract void Load();

        public abstract void ShowBanner(string key);
        public abstract void ShowBanner();
        public abstract void CloseBanner();

        public abstract void ShowInterstitial(string key, Action<bool> onDone = null);
        public abstract void ShowInterstitial(Action<bool> onDone = null);
        public abstract bool IsInterstitialReady();

        public abstract void ShowRewardedVideo(string key, Action<bool> onDone = null);
        public abstract void ShowRewardedVideo(Action<bool> onDone = null);
        public abstract bool IsRewardedVideoReady();
        public abstract bool IsRewardedVideoShowing();
    }

    public abstract class ADChannelBase<TLocationBanner, TLocationInterstitial, TLocationRewardedVideo> : ADChannelBase
        where TLocationBanner : ADLocationBase
        where TLocationInterstitial : ADLocationBase
        where TLocationRewardedVideo : ADLocationBase
    {
        #region Init     

        public override void OnInit(params object[] args)
        {
            var setting = args[0] as ADSetting;
            if (setting == null)
            {
                OnInited(false);
                return;
            }
            // Banner
            Banners = new List<ADLocationBase>();
            foreach (var param in setting.Banners)
            {
                var banner = Activator.CreateInstance<TLocationBanner>();
                banner.OnShowed += () => { OnBannerShowed(); };
                banner.Init(param);
                Banners.Add(banner);
            }

            BannerDic = Banners.ToDictionary(ad => ad.Key);

            // Interstitial
            Interstitials = new List<ADLocationBase>();
            foreach (var param in setting.Interstitials)
            {
                var interstitial = Activator.CreateInstance<TLocationInterstitial>();
                interstitial.OnShowed += () => { OnInterstitialShowed(); };
                interstitial.OnClosed += () => { OnInterstitialClosed(); };
                interstitial.Init(param);
                Interstitials.Add(interstitial);
            }

            InterstitialDic = Interstitials.ToDictionary(ad => ad.Key);

            // RewardedVideo
            RewardedVideos = new List<ADLocationBase>();
            foreach (var param in setting.RewardedVideos)
            {
                var rewardedVideo = Activator.CreateInstance<TLocationRewardedVideo>();
                rewardedVideo.OnLoaded += ret => { OnRewardedVideoLoaded(ret); };
                rewardedVideo.OnShowed += () => { OnRewardedVedioShowed(); };
                rewardedVideo.OnClosed += () => { OnRewardedVedioCloseed(); };
                rewardedVideo.OnResult += ret => { OnRewardedVedioResult(ret); };
                rewardedVideo.Init(param);
                RewardedVideos.Add(rewardedVideo);
            }

            RewardedVideosDic = RewardedVideos.ToDictionary(ad => ad.Key);

            IsInit = true;
            OnInited(true);
        }

        #endregion

        #region Load

        public override void Load()
        {
            if (!IsInit) return;
            foreach (var ad in Banners)
            {
                ad.Load();
            }
            foreach (var ad in Interstitials)
            {
                ad.Load();
            }
            foreach (var ad in RewardedVideos)
            {
                ad.Load();
            }
        }

        #endregion

        #region Banner

        public override void ShowBanner(string key)
        {
            if (!IsInit) return;
            ADLocationBase ad;
            if (BannerDic.TryGetValue(key, out ad))
            {
                ad.Show();
            }
        }

        public override void ShowBanner()
        {
            if (!IsInit) return;
            if (Banner == null || !Banner.IsReady) return;
            Banner.Show();
        }

        public override void CloseBanner()
        {
            if (!IsInit) return;
            if (Banner == null || !Banner.IsReady) return;
            Banner.Close();
        }

        #endregion

        #region Interstitial

        public override void ShowInterstitial(string key, Action<bool> onDone = null)
        {
            if (!IsInit) return;
            ADLocationBase ad;
            if (InterstitialDic.TryGetValue(key, out ad))
            {
                ad.Show(onDone);
            }
        }

        public override void ShowInterstitial(Action<bool> onDone = null)
        {
            if (!IsInit) return;
            if (Interstitial == null || !Interstitial.IsReady) return;
            Interstitial.Show(onDone);
        }

        public override bool IsInterstitialReady()
        {
            if (!IsInit) return false;
            if (Interstitial == null || !Interstitial.IsReady) return false;
            return Interstitial.IsReady;
        }

        #endregion

        #region RewardedVedio

        public override void ShowRewardedVideo(string key, Action<bool> onDone = null)
        {
            if (!IsInit) return;
            ADLocationBase ad;
            if (RewardedVideosDic.TryGetValue(key, out ad))
            {
                ad.Show(onDone);
            }
        }

        public override void ShowRewardedVideo(Action<bool> onDone = null)
        {
            if (!IsInit) return;
            if (RewardedVideo == null || !RewardedVideo.IsReady) return;
            RewardedVideo.Show(onDone);
        }

        public override bool IsRewardedVideoReady()
        {
            if (!IsInit) return false;
            if (RewardedVideo == null || !RewardedVideo.IsReady) return false;
            return RewardedVideo.IsReady;
        }

        public override bool IsRewardedVideoShowing()
        {
            if (!IsInit) return false;
            foreach (var rewardedVideo in RewardedVideos)
            {
                if (rewardedVideo.IsShowing) return true;
            }
            return false;
        }

        #endregion
    }
}
