using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.SDK
{
    [CreateAssetMenu(menuName = "Setting/Share Setting")]
    public class ShareSetting : SDKSettingBase<ShareSetting>
    {
        [Header("Share")]
        public string ShareUrl;
        public string ShareTitle;
        public string ShareDesc;
    }
}
