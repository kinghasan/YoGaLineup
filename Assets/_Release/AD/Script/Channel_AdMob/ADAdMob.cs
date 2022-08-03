#if ADMOB
using System;
using System.Collections;
using System.Collections.Generic;
using Aya.Analysis;
using Aya.Async;
using UnityEngine;
using Aya.SDK;
using GoogleMobileAds.Api;

namespace Aya.AD
{
    public class AdMobLocationBanner : ADLocationBase<AdMobSourceBanner>
    {
    }

    public class AdMobLocationInterstitial : ADLocationBase<AdMobSourceInterstitial>
    {
    }

    public class AdMobLocationRewardedVideo : ADLocationBase<AdMobSourceRewardedVideo>
    {
    }

    public class ADAdMob : ADChannelBase<AdMobLocationBanner, AdMobLocationInterstitial, AdMobLocationRewardedVideo>
    {
        public override void Init(params object[] args)
        {
            SDKDebug.Log("AD", "AdMob Init Start.");
            MobileAds.Initialize(initializationStatus =>
            {
                var setting = ADSetting.Ins;
                OnInit(setting);
                SDKDebug.Log("AD", "AdMob Init Success.");
                Load();

                UnityThread.ExecuteCoroutine(AdsHolder());
            });
        }

        // 广告守护协程，防止部分机型上因为缺失回调导致广告流程中断
        public IEnumerator AdsHolder()
        {
            bool CheckNeedLoad()
            {
                foreach (var source in ADManager.Instance.RewardedVideo.Sources)
                {
                    if (source.IsReady || source.IsLoading || source.IsShowing) return false;
                }
                return true;
            }

            while (true)
            {
                yield return new WaitForSecondsRealtime(10f);

                if (CheckNeedLoad())
                {
                    SDKDebug.Log("Reload All Video!");
                    Load();
                }
            }
        }
    }
}
#endif