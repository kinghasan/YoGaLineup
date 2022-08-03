using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.SDK
{
    [CreateAssetMenu(menuName = "SDK/Setting/SDK Setting")]
    public class SDKSetting : SDKSettingBase<SDKSetting>
    {
        [Header("Debug")]
        public bool DebugMode = false;
        public bool DebugLog = true;

        [Header("Store")]
        public string PackageName = "";
        public string StoreAppID = "";

        public string AppMarketUrl
        {
            get
            {
#if UNITY_ANDROID
                var url = string.Format("market://details?id={0}", PackageName);
                return url;
#elif UNITY_IOS
				return "itms-apps://itunes.apple.com/app/id" + StoreAppID;
#else
				return "";
#endif
            }
        }
    }
}
