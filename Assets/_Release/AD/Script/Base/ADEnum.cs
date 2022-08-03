using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.AD
{
    public enum ADLocationType
    {
        None = -1,
        Banner = 10,
        // NativeBanner = 11,
        Interstitial = 20,
        RewardedVideo = 30,
        NativeAd = 40,
    }

    public enum ADSourceState
    {
        Unload = 0,
        Loading = 1,
        Loaded = 2,
    }
}