using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aya.SDK;

namespace Aya.AD
{
    [CreateAssetMenu(menuName = "SDK/Setting/AD Setting")]
    public class ADSetting : SDKSettingBase<ADSetting>
    {
        [Header("App")]
        public string AppID;

        [Header("Banner")]
        public List<ADLocationParam> Banners;

        [Header("Interstitial")]
        public List<ADLocationParam> Interstitials;

        [Header("RewardedVideo")]
        public List<ADLocationParam> RewardedVideos;
    }
}