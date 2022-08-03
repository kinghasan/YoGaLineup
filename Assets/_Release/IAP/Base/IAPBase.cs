/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IAPBase.cs
//  Info     : IAP 实现基类
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;
using Aya.Events;
using UnityEngine;
using Aya.SDK;
using Aya.Util;
//using Game;

namespace Aya.IAP
{
    public enum IAPPEvent
    {
        OnInit,
        OnPayDone,
        OnPayOnceDone,
        OnSubscribeDone,
        OnRestoreDone,
        OnCheckSubscribe,
    }

    public class IAPBase : MonoBehaviour
    {
        public bool IsInited { get; protected set; }
        public Action<bool> OnInited = delegate { };
        public Action<bool> OnPaid = delegate { };
        public Action<bool> OnPaidOnce = delegate { };
        public Action<bool> OnSubscribed = delegate { };
        public Action<bool> OnRestored = delegate { };

        protected Action<IAPResult> OnPayDone = delegate { };
        protected Action<IAPResult> OnPayOnceDone = delegate { };
        protected Action<IAPResult> OnSubscribeDone = delegate { };
        protected Action<IAPResult> OnRestoreDone = delegate { };

        public SDKPayInfo CurrentPayInfo { get; internal set; }
        public IAPResult CurrentResult { get; internal set; }

        #region Init
        public virtual void Init(IAPInitInfo initInifo, Action<bool> onDone)
        {
            if (onDone != null)
            {
                OnInited = onDone;
            }
            OnInit(IAPResult.Success.ToString());
        }

        protected virtual void OnInit(string result)
        {
            IsInited = true;
            OnInited(IsInited);
        }
        #endregion

        #region Pay

        public virtual void Pay(SDKPayInfo info, Action<IAPResult> onDone = null)
        {
            IAPDebug.Log("Pay " + info);
            OnPayDone = onDone;
            OnPay(IAPResult.Success.ToString());
        }

        protected virtual void OnPay(string result)
        {
            IAPDebug.Log("OnPay :" + result);
            UEvent.Dispatch(IAPPEvent.OnPayDone, new IAPResult(result));
        }

        #endregion

        #region Pay Once
        public virtual void PayOnce(SDKPayInfo info, Action<IAPResult> onDone = null)
        {
            IAPDebug.Log("Pay Once " + info);
            OnPayOnceDone = onDone;
            OnPayOnce(IAPResult.Success.ToString());
        }

        protected virtual void OnPayOnce(string result)
        {
            IAPDebug.Log("OnPayOnce :" + result);
            UEvent.Dispatch(IAPPEvent.OnPayOnceDone, new IAPResult(result));
        }
        #endregion

        #region Subscribed
        public virtual void Subscribe(SDKPayInfo info, Action<IAPResult> onDone = null)
        {
            IAPDebug.Log("Subscribe " + info);
            OnSubscribeDone = onDone;
            OnSubscribe(IAPResult.Success.ToString());
        }

        protected virtual void OnSubscribe(string result)
        {
            IAPDebug.Log("OnSubscribe :" + result);
            UEvent.Dispatch(IAPPEvent.OnSubscribeDone, new IAPResult(result));
        }

        public virtual void CheckSubscribe(string itemID)
        {
        }

        protected virtual void OnCheckSubscribe(string result)
        {
            IAPDebug.Log("OnCheckSubscribe :" + result);
            UEvent.Dispatch(IAPPEvent.OnCheckSubscribe, new IAPResult(result));
        }
        #endregion

        #region Restore Purchase
        public virtual void RestorePurchase(Action<IAPResult> onDone = null)
        {
            IAPDebug.Log("Restore Purchase");
            OnRestoreDone = onDone;
            OnRestorePurchase(IAPResult.Success.ToString());
        }

        protected virtual void OnRestorePurchase(string result)
        {
            IAPDebug.Log("OnRestorePurchase :" + result);
            UEvent.Dispatch(IAPPEvent.OnRestoreDone, new IAPResult(result));
        }
        #endregion

        #region Get Info
        public virtual int GetChannelID()
        {
            return 0;
        }

        public virtual string GetChannelName()
        {
            return "Default";
        }
        #endregion
    }
}
