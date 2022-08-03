/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IAPManager.cs
//  Info     : IAP 应用内支付 管理器
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2018
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.IAP
{
	public static class IAPManager
	{
		public static IAPBase Instance { get; private set; }

		static IAPManager()
		{
			IAPDebug.Log("IAPManager Init");
			CreateSdkInstance();
		}

		private static void CreateSdkInstance() {
			var platform = "Unknow";
			var channel = "Unknow";
			var goName = "IAPManager";
			var iap = new GameObject(goName);
			Object.DontDestroyOnLoad(iap);
			iap.hideFlags = HideFlags.HideInHierarchy;

#if !IAP
			platform = "Without IAP";
			Instance = iap.AddComponent<IAPBase>();
#elif UNITY_STANDALONE // && !UNITY_EDITOR
			platform = "Windows";
			Instance = iap.AddComponent<IAPWindowsBase>();
#elif UNITY_EDITOR
			platform = "Editor";
			Instance = iap.AddComponent<IAPEditorBase>();
#elif UNITY_ANDROID && !UNITY_EDITOR
			platform = "Android";
			Instance = iap.AddComponent<IAPGooglePlay>();
#elif UNITY_IOS && !UNITY_EDITOR
			platform = "IOS";
			Instance = iap.AddComponent<IAPiOSBase>();
#endif
			channel = Instance.GetChannelName();
			IAPDebug.Log("Platform : " + platform + "\t Channel ：" + channel);

#if UNITY_EDITOR		
			Instance.Init(null, null);
#endif
		}
	}
}