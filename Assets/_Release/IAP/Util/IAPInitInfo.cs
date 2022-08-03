/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IAPInitInfo.cs
//  Info     : IAP 初始化参数
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using Aya.SDK;

namespace Aya.IAP
{
	[Serializable]
	public class IAPInitInfo : SDKInfo
	{
        public List<IAPBaseProductInfo> Products = new List<IAPBaseProductInfo>();
	}
}
