/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IAPGooglePlay.cs
//  Info     : IAP Google Play 实现
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.IAP
{
	public class IAPGooglePlay : IAPAndroidBase 
	{
        private void Awake()
        {
            var initInfo = new IAPInitInfoGooglePlay();
            initInfo.AppID = IAPConfig.Ins.AppID;
            initInfo.Base64EncodedPublicKey = IAPConfig.Ins.Base64EncodedPublicKey;
            for (var i = 0; i < IAPConfig.Ins.Products.Count; i++)
            {
                var product = IAPConfig.Ins.Products[i];
                initInfo.Products.Add(product);
            }
            Init(initInfo, ret =>
            {

            });
        }
    }
}
