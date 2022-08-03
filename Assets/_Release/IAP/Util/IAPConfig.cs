/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IAPConfig.cs
//  Info     : IAP 配置
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using Aya.Extension;
using UnityEngine;
using Aya.Util;
using Aya.SDK;

namespace Aya.IAP
{
    public class IAPConfig : MonoBehaviour
    {
        #region Instance
        public static IAPConfig Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = Resources.Load<IAPConfig>("IAPConfig");
                }
                return _ins;
            }
        }
        private static IAPConfig _ins;

        protected IAPConfig()
        {
        }
        #endregion

        public List<IAPBaseProductInfo> Products;

        public Dictionary<string, IAPBaseProductInfo> ProductDic
        {
            get
            {
                if (_productDic == null)
                {
                    _productDic = Products.ToDictionary(prodouc => prodouc.ProductID);
                }
                return _productDic;
            }
        }
        private Dictionary<string, IAPBaseProductInfo> _productDic;

        public string AppID;
        [Multiline]
        public string Base64EncodedPublicKey;

        // 客户端ID				193020803983-ht8lk55tb2aua7ok9rl8qu5isgf6pan0.apps.googleusercontent.com
        // 签名证书指纹 (SHA1)	6E:AF:05:B6:16:78:5F:B7:0A:31:5D:FD:14:61:47:69:51:FF:5F:EE
        public SDKPayInfo GetPayInfo(string id)
        {
            return GetPayInfo(ProductDic.GetValue(id));
        }
        public static SDKPayInfo GetPayInfo(IAPBaseProductInfo info)
        {
            if (info == null) return null;
            var sdkPayInfo = new SDKPayInfo();
            sdkPayInfo.ProductID = info.ProductID;
            sdkPayInfo.ProductName = info.ProductName;
            sdkPayInfo.ProductDesc = info.ProductDesc;
            sdkPayInfo.Amount = info.Amount;
            sdkPayInfo.PayType = SDKPayType.GooglePlay;
            return sdkPayInfo;
        }
    }
}
