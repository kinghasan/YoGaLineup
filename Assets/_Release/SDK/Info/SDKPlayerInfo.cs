/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SDKPlayerInfo.cs
//  Info     : SDK 玩家信息
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using Aya.Util;

namespace Aya.SDK
{
	[Serializable]
	public class SDKPlayerInfo : SDKInfo
	{
		public string UserID;                   // 用户ID
		public string UserName;                 // 用户昵称
		public int UserLevel = 0;               // 用户等级
		public int VipLevel = 0;                // 用户VIP等级
		public int Coin = 0;                    // 金币数量
		public int Diamond = 0;                 // 钻石数量
		public string PartyName;                // 公会名字
		public string ServerID;                 // 所在服务器ID
		public string ServerName;               // 所在服务器名字
		public long UserCreateTime = 0;         // 用户创建时间
		public long LastLoginTime = 0;          // 最后一次登录时间

		public SDKPlayerInfo() 
		{
			UserID = SDKManager.Instance.UserID;
			UserName = "";
			UserLevel = 1;
			PartyName = "";
			Coin = 0;
			Diamond = 0;
			ServerID = SDKManager.Instance.GetChannelID().ToString();
			ServerName = SDKManager.Instance.GetChannelName();
			LastLoginTime = TimeUtil.TimeStamp;
		}

		public static SDKPlayerInfo Get
		{
			get
			{
				SDKPlayerInfo info;
				try
				{
					info = new SDKPlayerInfo();
				}
				catch
				{
					info = null;
				}
				return info;
			}
		}
	}
}
