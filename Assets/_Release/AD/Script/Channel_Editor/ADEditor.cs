using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.AD
{
    public class ADEditor : ADChannelBase<ADEditorLocationBanner, ADEditorLocationInterstitial, ADEditorLocationRewardedVideo>
    {
        public override void Init(params object[] args) { }

        public override void ShowBanner() { }

        public override bool IsInterstitialReady() => true;

        public override void ShowInterstitial(Action<bool> onDone = null) => onDone?.Invoke(true);

        public override bool IsRewardedVideoReady() => true;

        public override void ShowRewardedVideo(Action<bool> onDone = null) => onDone?.Invoke(true);

    }

    #region AD Editor Location

    public class ADEditorLocationBanner : ADLocationBase<ADEditorSourceBanner>
    {
    }

    public class ADEditorLocationInterstitial : ADLocationBase<ADEditorSourceInterstitial>
    {
    }

    public class ADEditorLocationRewardedVideo : ADLocationBase<ADEditorSourceRewardedVideo>
    {
    }

    #endregion

    #region AD Editor Source

    public class ADEditorSourceBanner : ADEditorSourceBase
    {
        public override ADLocationType Type => ADLocationType.Banner;
    }

    public class ADEditorSourceInterstitial : ADEditorSourceBase
    {
        public override ADLocationType Type => ADLocationType.Interstitial;
    }

    public class ADEditorSourceRewardedVideo : ADEditorSourceBase
    {
        public override ADLocationType Type => ADLocationType.RewardedVideo;
    }

    public class ADEditorSourceBase : ADSourceBase
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

