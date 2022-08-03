/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AnalysisEnum.cs
//  Info     : 统计分析用枚举
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////

namespace Aya.Analysis
{
	/// <summary>
	/// 任务类型
	/// </summary>
	public enum MissionType
	{
		/// <summary>
		/// 未知
		/// </summary>
		Unknown = -1,
		/// <summary>
		/// 新手引导
		/// </summary>
		Guide = 0,
		/// <summary>
		/// 每日
		/// </summary>
		Daily = 1,
		/// <summary>
		/// 每周
		/// </summary>
		Weekly = 2,
		/// <summary>
		/// 每月
		/// </summary>
		Monthly = 3,
		/// <summary>
		/// 限时活动
		/// </summary>
		Activity = 4,
		/// <summary>
		/// 剧情/闯关
		/// </summary>
		Story = 5,
		/// <summary>
		/// 成就/挑战
		/// </summary>
		Challenge = 6,
		/// <summary>
		/// 其他
		/// </summary>
		Other = 100
	}

	/// <summary>
	/// 性别
	/// </summary>
	public enum GenderType {
		/// <summary>
		/// 未知
		/// </summary>
		Unknown = -1,
		/// <summary>
		/// 男性
		/// </summary>
		Male = 0,
		/// <summary>
		/// 女性
		/// </summary>
		Female = 1
	}

	/// <summary>
	/// 账户类型
	/// </summary>
	public enum AccountType
	{
		/// <summary>
		/// 默认渠道
		/// </summary>
		Default = 0,
		/// <summary>
		/// QQ
		/// </summary>
		QQ = 1,
		/// <summary>
		/// 微信
		/// </summary>
		WeChat = 2,
		/// <summary>
		/// 谷歌
		/// </summary>
		Google = 3,
		/// <summary>
		/// 其他
		/// </summary>
		Other = 999
	}
}

