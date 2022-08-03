/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AnalysisTalkingData.cs
//  Info     : 统计分析管理器 -- TalkingData 实现
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;
using Aya.SDK;

#if TALKINGDATA
namespace Aya.Analysis
{
	public class AnalysisTalkingData : AnalysisBase
	{
		protected static TDGAAccount Account;
		protected static string TalkingDataID = "";

		#region Init & End

		/// <summary>
		/// 初始化
		/// </summary>
		public override void Init()
		{
			var appid = TalkingDataID;
			var channel = SDKManager.Instance.GetChannelName();
			if (string.IsNullOrEmpty(appid))
			{
				AnalysisDebug.Error("Analytics APP_ID is null ! Please set " + "APP_ID".ToMarkup(Color.yellow) + ".");
			}
			if (string.IsNullOrEmpty(channel))
			{
				AnalysisDebug.Error("Analytics Channel Name is null !  Please set Channel Name.");
			}
			AnalysisDebug.Log("Start " + appid + " \t" + channel);
			TalkingDataGA.OnStart(appid, channel);
		}

		/// <summary>
		/// 结束
		/// </summary>
		public override void End()
		{
			TalkingDataGA.OnEnd();
		}

		#endregion

		#region Info

		/// <summary>
		/// 设置帐号
		/// </summary>
		/// <param name="uid">唯一标识</param>
		/// <param name="nickName">昵称</param>
		/// <param name="type">账户类型</param>
		public override void SetAccount(string uid, string nickName, AccountType type = AccountType.Default) {
			Account = TDGAAccount.SetAccount(uid);
			Account.SetAccountName(nickName);
			switch (type)
			{
				case AccountType.Default:
					Account.SetAccountType(global::AccountType.ANONYMOUS);
					break;
				case AccountType.QQ:
					Account.SetAccountType(global::AccountType.QQ);
					break;
				default:
					Account.SetAccountType(global::AccountType.ANONYMOUS);
					break;
			}
		}

		/// <summary>
		/// 设置服务器
		/// </summary>
		/// <param name="server">服务器</param>
		public override void SetServer(string server)
		{
			if(Account == null) return;
			Account.SetGameServer(server);
		}

		/// <summary>
		/// 设置等级
		/// </summary>
		/// <param name="level">等级</param>
		public override void SetLevel(int level)
		{
			if (Account == null) return;
			Account.SetLevel(level);
		}

		/// <summary>
		/// 设置性别
		/// </summary>
		/// <param name="gender">性别类型</param>
		public override void SetGender(GenderType gender) 
		{
			if (Account == null) return;
			switch (gender)
			{
				case GenderType.Unknown:
					Account.SetGender(Gender.UNKNOW);
					break;
				case GenderType.Male:
					Account.SetGender(Gender.MALE);
					break;
				case GenderType.Female:
					Account.SetGender(Gender.FEMALE);
					break;
			}
		}

		/// <summary>
		/// 设置性别
		/// </summary>
		/// <param name="age">年龄</param>
		public override void SetAge(int age) 
		{
			if (Account == null) return;
			Account.SetAge(age);
		}
		#endregion

		#region Currency

		/// <summary>
		/// 支付
		/// </summary>
		/// <param name="requestID">支付订单号</param>
		/// <param name="title">支付标题</param>
		/// <param name="price">支付金额(单位：元)</param>
		/// <param name="currency">获得虚拟币</param>
		/// <param name="payType">支付方式</param>
		public override void Pay(string requestID, string title, double price, double currency, string payType = "Unknown") 
		{
			TDGAVirtualCurrency.OnChargeRequest(requestID, title, price, "CNY", currency, payType);
			
		}

		/// <summary>
		/// 支付失败
		/// </summary>
		/// <param name="requestID">支付订单号</param>
		/// <param name="reason">失败原因</param>
		public override void PayFail(string requestID, string reason) {

		}

		/// <summary>
		/// 支付成功
		/// </summary>
		/// <param name="requestID">支付订单号</param>
		public override void PaySuccess(string requestID) 
		{
			TDGAVirtualCurrency.OnChargeSuccess(requestID);
		}

		/// <summary>
		/// 奖励虚拟币(不算充值收入)
		/// </summary>
		/// <param name="amount">数量</param>
		/// <param name="reason">原因</param>
		public override void Reward(double amount, string reason)
		{
			TDGAVirtualCurrency.OnReward(amount, reason);
		}

		#endregion

		#region Item

		/// <summary>
		/// 购买物品
		/// </summary>
		/// <param name="itemName">物品名称</param>
		/// <param name="amount">数量</param>
		/// <param name="currency">消费虚拟币</param>
		public override void Purchase(string itemName, int amount, double currency)
		{
			TDGAItem.OnPurchase(itemName, amount, currency);
		}

		/// <summary>
		/// 使用物品
		/// </summary>
		/// <param name="itemName">物品名称</param>
		/// <param name="amount">数量</param>
		public override void Use(string itemName, int amount)
		{
			TDGAItem.OnUse(itemName, amount);
		}

		#endregion

		#region Mission

		/// <summary>
		/// 任务开始
		/// </summary>
		/// <param name="mission">任务名</param>
		/// <param name="type">任务类型</param>
		public override void MissionStart(string mission, MissionType type = MissionType.Unknown)
		{
			TDGAMission.OnBegin(mission);
		}

		/// <summary>
		/// 任务完成
		/// </summary>
		/// <param name="mission">任务名称</param>
		public override void MissionCompleted(string mission)
		{
			TDGAMission.OnCompleted(mission);
		}

		/// <summary>
		/// 任务失败
		/// </summary>
		/// <param name="mission">任务名称</param>
		/// <param name="reason">失败原因</param>
		public override void MissionFailed(string mission, string reason)
		{
			TDGAMission.OnFailed(mission, reason);
		}

		#endregion

		#region Event

		/// <summary>
		/// 自定义事件
		/// </summary>
		/// <param name="eventID">事件ID</param>
		/// <param name="add"></param>
		/// <param name="args">事件参数</param>
		public override void Event(string eventID, Dictionary<string, object> args = null)
		{
			if (args == null)
			{
				args = new Dictionary<string, object>();
			}
			TalkingDataGA.OnEvent(eventID, args);
		}

		#endregion
	}
}
#endif