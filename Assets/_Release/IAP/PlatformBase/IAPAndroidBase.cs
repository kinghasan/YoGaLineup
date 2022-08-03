/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IAPAndroidBase.cs
//  Info     : IAP 安卓实现
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;
using Aya.Events;
using Aya.Platform;
using Aya.SDK;
using Aya.Util;
//using Game;

namespace Aya.IAP
{
	public class IAPAndroidBase : IAPBase
	{
		#region Unity call Android
		protected AndroidHelper AndroidHelper
		{
			get
			{
				if (_androidHelper == null)
				{
					_androidHelper = new AndroidHelper(IAPGlobal.DefaultIAPPackageName, "getInstance");
				}
				return _androidHelper;
			}
		}
		private AndroidHelper _androidHelper;
		#endregion

		#region Init
		public override void Init(IAPInitInfo initInifo, Action<bool> onDone) {
			IAPDebug.Log("init");
			AndroidHelper.Invoke("init", initInifo.ToJson());
		}

		protected override void OnInit(string result) {
			IAPResult.OnResult(result,
				ret => {
					IsInited = true;
					OnInited(IsInited);
                    UEvent.Dispatch(IAPPEvent.OnInit);
				},
				(code, reason) => {
					IsInited = false;
				});
		} 
		#endregion

		#region Pay

		public override void Pay(SDKPayInfo info, Action<IAPResult> onDone = null) {
			IAPDebug.Log("Pay " + info);
			OnPayDone = onDone;
			CurrentPayInfo = info;
			AndroidHelper.Invoke("pay", info.ToString());
		}

//		protected override void OnPay(string result)
//		{
//			IAPResult.OnResult(result,
//				ret =>
//				{
//				    var prodouctID = ret["ProductID"].CastType<string>();
//				    var product = IAPConfig.Ins.ProductDic.GetValue(prodouctID);    

//                    // TODO..
////				    var oriValue = SaveData.Get<int>(product.ItemKey, 0);
////				    var newValue = oriValue + product.Value;
////                    SaveData.Set(product.ItemKey, newValue);
////
////				    var onChange = PlayerData.Ins.OnItemChange.GetOrAdd(product.ItemKey, delegate { });
////				    onChange(newValue);

//                    if (OnPayDone != null)
//					{
//						OnPayDone(true);
//					}
//				    OnPaid(true);
//                },
//				(code, reason) =>
//				{
//					if (OnPayDone != null)
//					{
//						OnPayDone(false);
//					}
//				    OnPaid(false);
//                });
//		}

        #endregion

        #region Pay Once
        public override void PayOnce(SDKPayInfo info, Action<IAPResult> onDone = null)
        {
            IAPDebug.Log("Pay Once " + info);
            OnPayOnceDone = onDone;
	        CurrentPayInfo = info;
			AndroidHelper.Invoke("payOnce", info.ToString());
        }

//        protected override void OnPayOnce(string result)
//        {
//            IAPResult.OnResult(result,
//                ret =>
//                {
//                    var prodouctID = ret["ProductID"].CastType<string>();
//                    var product = IAPConfig.Ins.ProductDic.GetValue(prodouctID);
//                    var funcKey = product.ItemKey;

//                    // TODO..
////                    SaveData.Set<bool>(funcKey, true);

//                    if (OnPayOnceDone != null)
//                    {
//                        OnPayOnceDone(true);
//                    }
//                    OnPaidOnce(true);
//                },
//                (code, reason) =>
//                {
//                    if (OnPayOnceDone != null)
//                    {
//                        OnPayOnceDone(false);
//                    }
//                    OnPaidOnce(false);
//                });
//        }
        #endregion

        #region Subscribed
        public override void Subscribe(SDKPayInfo info, Action<IAPResult> onDone = null)
        {
            IAPDebug.Log("Subscribe " + info);
            OnSubscribeDone = onDone;
	        CurrentPayInfo = info;
			AndroidHelper.Invoke("subscribe", info.ToString());
        }

//        protected override void OnSubscribe(string result)
//        {
//            IAPResult.OnResult(result,
//                ret =>
//                {
//                    var prodouctID = ret["ProductID"].CastType<string>();
//                    var product = IAPConfig.Ins.ProductDic.GetValue(prodouctID);
//                    var funcKey = product.ItemKey;

//                    // TODO..
////                    SaveData.Set<bool>(funcKey, true);

//                    if (OnSubscribeDone != null)
//                    {
//                        OnSubscribeDone(true);
//                    }
//                    OnSubscribed(true);
//                },
//                (code, reason) =>
//                {
//                    if (OnSubscribeDone != null)
//                    {
//                        OnSubscribeDone(false);
//                    }
//                    OnSubscribed(false);
//                });
//        }
        #endregion

        #region Restore Purchase
        public override void RestorePurchase(Action<IAPResult> onDone = null)
        {
            IAPDebug.Log("Restore Purchase ");
            OnRestoreDone = onDone;
            AndroidHelper.Invoke("restorePurchase");
        }

        public override void CheckSubscribe(string itemID)
        {
            IAPDebug.Log("CheckSubscribe itemID:"+itemID);
            AndroidHelper.Invoke("checkSubscribed",itemID);
        }

        //protected override void OnRestorePurchase(string result)
        //{
        //    IAPResult.OnResult(result,
        //        ret =>
        //        {
        //            if (OnRestoreDone != null)
        //            {
        //                OnRestoreDone(true);
        //            }
        //            OnRestored(true);
        //        },
        //        (code, reason) =>
        //        {
        //            if (OnRestoreDone != null)
        //            {
        //                OnRestoreDone(false);
        //            }
        //            OnRestored(false);
        //        });
        //}
        #endregion

        #region Get Info
        //渠道ID
        public override int GetChannelID() {
			return AndroidHelper.Invoke<int>("getChannelID");
		}

		//渠道名称
		public override string GetChannelName() {
			return AndroidHelper.Invoke<string>("getChannelName");
		} 
		#endregion
	}
}

