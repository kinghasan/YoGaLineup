/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IAPInitInfoGooglePlay.cs
//  Info     : IAP 谷歌内购初始化参数
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;

namespace Aya.IAP
{
	[Serializable]
	public class IAPInitInfoGooglePlay : IAPInitInfo
	{
		public string AppID = "";
		public string Base64EncodedPublicKey = "";
	}
}
