/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AnalysisBase.cs
//  Info     : 统计分析管理器基类
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
// #define GoogleAnalytic
using System;
using System.Collections.Generic;
using Aya.SDK;
using UnityEngine;

namespace Aya.Analysis
{
    public class AnalysisBase
    {
        #region Init & End

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// 结束
        /// </summary>
        public virtual void End()
        {
        }

        #endregion

        #region Info

        /// <summary>
        /// 设置帐号
        /// </summary>
        /// <param name="uid">唯一标识</param>
        /// <param name="nickName">昵称</param>
        /// <param name="type">账户类型</param>
        public virtual void SetAccount(string uid, string nickName, AccountType type = AccountType.Default)
        {
        }

        /// <summary>
        /// 设置服务器
        /// </summary>
        /// <param name="server">服务器</param>
        public virtual void SetServer(string server)
        {
        }

        /// <summary>
        /// 设置等级
        /// </summary>
        /// <param name="level">等级</param>
        public virtual void SetLevel(int level)
        {
        }

        /// <summary>
        /// 设置性别
        /// </summary>
        /// <param name="gender">性别类型</param>
        public virtual void SetGender(GenderType gender)
        {
        }

        /// <summary>
        /// 设置性别
        /// </summary>
        /// <param name="age"></param>
        public virtual void SetAge(int age)
        {
        }
        #endregion

        #region Currency
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="info">充值信息</param>
        public void Pay(SDKPayInfo info)
        {
            Pay(info.RequestID, info.ProductName, info.PriceCNY, info.Currency, info.PayType.ToString());
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="requestID">支付订单号</param>
        /// <param name="title">支付标题</param>
        /// <param name="price">支付金额(单位：元)</param>
        /// <param name="currency">获得虚拟币</param>
        /// <param name="payType">支付方式</param>
        public virtual void Pay(string requestID, string title, double price, double currency, string payType = "Unknown")
        {
        }

        /// <summary>
        /// 支付失败
        /// </summary>
        /// <param name="requestID">支付订单号</param>
        /// <param name="reason">失败原因</param>
        public virtual void PayFail(string requestID, string reason)
        {

        }

        /// <summary>
        /// 支付成功
        /// </summary>
        /// <param name="requestID">支付订单号</param>
        public virtual void PaySuccess(string requestID)
        {

        }

        /// <summary>
        /// 奖励虚拟币(不算充值收入)
        /// </summary>
        /// <param name="amount">数量</param>
        /// <param name="reason">原因</param>
        public virtual void Reward(double amount, string reason)
        {
        }

        #endregion

        #region Item

        /// <summary>
        /// 购买物品
        /// </summary>
        /// <param name="itemName">物品名称</param>
        /// <param name="amount">数量</param>
        /// <param name="currency">消费虚拟币</param>
        public virtual void Purchase(string itemName, int amount, double currency)
        {
        }

        /// <summary>
        /// 使用物品
        /// </summary>
        /// <param name="itemName">物品名称</param>
        /// <param name="amount">数量</param>
        public virtual void Use(string itemName, int amount)
        {
        }

        #endregion

        #region Level

        public virtual void LevelStart(string level)
        {
        }

        public virtual void LevelCompleted(string level)
        {
        }

        public virtual void LevelFailed(string level)
        {
        }

        #endregion

        #region Mission

        /// <summary>
        /// 任务开始
        /// </summary>
        /// <param name="mission">任务名</param>
        /// <param name="type">任务类型</param>
        public virtual void MissionStart(string mission, MissionType type = MissionType.Unknown)
        {
        }

        /// <summary>
        /// 任务完成
        /// </summary>
        /// <param name="mission">任务名称</param>
        public virtual void MissionCompleted(string mission)
        {
        }

        /// <summary>
        /// 任务失败
        /// </summary>
        /// <param name="mission">任务名称</param>
        /// <param name="reason">失败原因</param>
        public virtual void MissionFailed(string mission, string reason)
        {
        }

        #endregion

        #region Event

        /// <summary>
        /// 自定义事件
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="args">参数列表</param>
        public void Event(string eventID, params object[] args)
        {
// #if !UNITY_EDITOR
            var preArgs = new[]
            {
                "UserID", AnalysisManager.UserID,
                "RegisterTime", AnalysisManager.RegisterTime,
                "EventTime", AnalysisManager.EventTime,
                "RegisterVersion",AnalysisManager.RegisterVersion,
                //"ABID",ABTestSetting.Ins.Version.ToString()
            };
            var newArgs = new List<object>();
            newArgs.AddRange(preArgs);
            newArgs.AddRange(args);
            // Event(eventID + "_" + ABTestSetting.Ins.Version, AnalysisUtil.ParamToDic(newArgs.ToArray()));
// #endif
        }

        /// <summary>
        /// 自定义事件
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="add"></param>
        /// <param name="args">事件参数</param>
        public virtual void Event(string eventID, Dictionary<string, object> args)
        {
        }

#if GoogleAnalytic
		public virtual void Event(string category, string action, string lable, int value) 
		{
		}
#endif

        #endregion

    }
}