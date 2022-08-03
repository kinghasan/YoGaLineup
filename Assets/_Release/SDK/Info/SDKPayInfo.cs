/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SDKPayInfo.cs
//  Info     : SDK 支付信息
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using Aya.Data.Json;

namespace Aya.SDK
{
	[Serializable]
	public class SDKPayInfo : SDKInfo
	{
		/// <summary>
		/// 充值虚拟币兑换系数 元 x 系数 = 虚拟币
		/// </summary>
		[JsonIgnore]
		public static int ExchangeCoefficient = 10;

		public string RequestID;				// CP订单号
		public string UserID;					// 用户ID
		public string UserName;					// 用户名，角色名
		public int UserLevel;					// 用户等级
		public string PartyName;				// 公会、帮派名
		public string ServerID;					// 所在服务器ID
		public string ServerName;				// 所在服务器名
		public string CompanyName;				// 商户、公司名
		public string Mark = "";				// 标记
		public string ProductID = "";			// 商品ID
		public string ProductName = "";			// 商品名字
		public string ProductDesc = "";			// 商品描述
		public SDKPayType PayType;				// 支付方式

		public int VipLevel = 0;				// VIP等级
		public int Amount = 0;					// 货币数量，价格，单位为分，如为1元，则 Amount = 100
		public int Balance;						// 余额

		/// <summary>
		/// 消费金额，人民币（元）
		/// </summary>
		[JsonIgnore]
		public float PriceCNY
		{
			get { return Amount * 1f / 100; }
		}

		/// <summary>
		/// 获得虚拟币数量
		/// </summary>
		[JsonIgnore]
		public int Currency
		{
			get { return (int) PriceCNY * ExchangeCoefficient; }
		}

		public SDKPayInfo()
		{
			UserID = SDKManager.Instance.UserID;
			UserName = "";
			UserLevel = 1;
			PartyName = "";
			ServerID = SDKManager.Instance.GetChannelID().ToString();
			ServerName = SDKManager.Instance.GetChannelName();
			Balance = 0;
			// RequestID 尽可能由SDK生成
			PayType = SDKPayType.Unknown;
			RequestID = CreateRequestID();
			CompanyName = "Aya Game Studio";
		}

		public override string ToJson()
		{
			if (string.IsNullOrEmpty(ProductDesc))
			{
				ProductDesc = ProductName;
			}

			return base.ToJson();
		}

		/// <summary>
		/// 创建订单号
		/// </summary>
		/// <returns></returns>
		public static string CreateRequestID()
		{
			return DateTime.UtcNow.ToString("yyyy-MM-dd-hh-mm-ss-fff-") + UnityEngine.Random.Range(10000, 99999);
		}
	}
}