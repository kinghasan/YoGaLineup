using System.Collections;
using System.Collections.Generic;
using Aya.Analysis;
using Aya.Async;
using UnityEngine;
using Aya.SDK;

namespace Aya.AD
{
    public class ADManager
    {
        public static ADChannelBase Instance { get; private set; }

        static ADManager()
        {
        }

        public static void Init()
        {
            SDKDebug.Log("AD", "Init.");
            CreateSdkInstance();
        }

        private static void CreateSdkInstance()
        {

#if UNITY_ANDROID || UNITY_IOS

            // TODO..
#if TapNation
            SDKDebug.Log("AD", "Init AD TapNation.");
            Instance = new ADTapNation();
#endif

#endif
            if (Instance == null)
            {
                SDKDebug.Log("AD", "Init Editor.");
                Instance = new ADEditor();
            }

            var setting = ADSetting.Ins;
            if (setting == null)
            {
                Debug.LogError("ADSetting not found!!!");
            }
            else
            {
                Instance.Init(setting);
            }
        }

    }
}
