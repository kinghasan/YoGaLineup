/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IAPBaseProductInfo.cs
//  Info     : IAP 商品信息基类
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;

namespace Aya.IAP
{
	public enum IAPPayType
	{
	    Repurchase = 10,            // 可重复购买，可消耗商品
	    Permanent = 20,             // 一次性购买，功能性商品
        Subscription = 30,          // 订阅商品，按周期付费
        RewardVideo = 40,           // 激励视频，非付费购买类商品	    
    }

	[Serializable]
	public class IAPBaseProductInfo
	{
		public string ProductID;						    // 商品ID
		public string ProductName;						    // 商品名称
		public string ProductDesc;						    // 商品描述
		public IAPPayType PayType = IAPPayType.Repurchase;	// 支付方式
		public int Amount;								    // 支付货币数量(单位:美元)，广告支付时无效
		public float Discount = 1f;						    // 折扣
		public string ItemKey;							    // 商品对应的道具ID，如为一次性购买或订阅功能，则为对应功能开关存储Key
		public int Value;								    // 获得数量
		public int Reward;								    // 额外奖励数量
	}
}

