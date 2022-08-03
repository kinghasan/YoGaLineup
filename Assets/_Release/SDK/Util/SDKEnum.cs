/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SDKEnum.cs
//  Info     : SDK枚举
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////

namespace Aya.SDK 
{
	/// <summary>
	/// 登录方式
	/// </summary>
	public enum SDKLoginType 
	{
		Channel = 0,            // 渠道登陆
		QQ = 1,                 // QQ登陆
		WeChat = 2,             // 微信登陆
	}

	/// <summary>
	/// 支付方式
	/// </summary>
	public enum SDKPayType 
	{
		Unknown = -1,			// 未知
		GooglePlay = 1,			// Google Play
		AppStore = 2,			// Apple App Store
		AliPay = 100,			// 支付宝
		WeChat = 101,			// 微信
		QQ = 102,				// QQ
		Other = 999,			// 其他
	}
}
