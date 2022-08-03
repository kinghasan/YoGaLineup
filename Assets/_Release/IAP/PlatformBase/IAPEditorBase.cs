/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IAPEditorBase.cs
//  Info     : IAP 编辑器实现
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;
using Aya.SDK;
using Aya.Util;
//using Game;

namespace Aya.IAP
{
    public class IAPEditorBase : IAPBase
    {
        #region Init
        public override void Init(IAPInitInfo initInifo, Action<bool> onDone)
        {
            if (onDone != null)
            {
                OnInited = onDone;
            }
            OnInit(IAPResult.Success.ToString());
        }

        protected override void OnInit(string result)
        {
            IsInited = true;
            OnInited(IsInited);
        }
        #endregion

        #region Pay

        public override void Pay(SDKPayInfo info, Action<IAPResult> onDone = null)
        {
            IAPDebug.Log("Pay " + info);
            OnPayDone = onDone;

            var result = IAPResult.Success;
            result["ProductID"] = info.ProductID;
            OnPay(result.ToString());
        }

        #endregion

        #region Pay Once
        public override void PayOnce(SDKPayInfo info, Action<IAPResult> onDone = null)
        {
            IAPDebug.Log("Pay Once " + info);
            OnPayOnceDone = onDone;

            var result = IAPResult.Success;
            result["ProductID"] = info.ProductID;
            OnPayOnce(result.ToString());
        }
        #endregion

        #region Subscribed
        public override void Subscribe(SDKPayInfo info, Action<IAPResult> onDone = null)
        {
            IAPDebug.Log("Subscribe " + info);
            OnSubscribeDone = onDone;

            var result = IAPResult.Success;
            result["ProductID"] = info.ProductID;
            OnSubscribe(result.ToString());
        }

        #endregion

        #region Restore Purchase
        public override void RestorePurchase(Action<IAPResult> onDone = null)
        {
            IAPDebug.Log("Restore Purchase");
            OnRestoreDone = onDone;
            OnRestorePurchase(IAPResult.Success.ToString());
        }
        #endregion
    }
}

