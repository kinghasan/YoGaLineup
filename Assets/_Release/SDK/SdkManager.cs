/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SDKManager.cs
//  Info     : SDK管理器
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace Aya.SDK
{
	public static class SDKManager
	{
		public static SDKBase Instance { get; private set; }

		static SDKManager()
		{
			SDKDebug.Log("SDKManager Init");
			CreateSdkInstance();
		}

		private static void CreateSdkInstance()
		{
			var platform = "Unknow";
			var channel = "Unknow";
			var goName = "SDKManager";
			var sdk = new GameObject(goName);
			Object.DontDestroyOnLoad(sdk);
		    sdk.hideFlags = HideFlags.HideInHierarchy;

#if !SDK
			platform = "Without SDK";
			Instance = sdk.AddComponent<SDKBase>();
#elif UNITY_STANDALONE // && !UNITY_EDITOR
			platform = "Windows";
			Instance = sdk.AddComponent<SDKWindowsBase>();
#elif UNITY_EDITOR
			platform = "Editor";
			Instance = sdk.AddComponent<SDKEditorBase>();
#elif UNITY_ANDROID && !UNITY_EDITOR
			platform = "Android";
			Instance = sdk.AddComponent<SDKAndroidBase>();
#elif UNITY_IOS && !UNITY_EDITOR
			platform = "IOS";
			Instance = sdk.AddComponent<SDKiOSBase>();
#endif
			channel = Instance.GetChannelName();
			SDKDebug.Log("Platform : " + platform + "\t Channel ：" + channel);

			Instance.Init(ret =>
			{
				Instance.CheckUpdate();
				if (!Instance.IsLogIn) Instance.Login("", LoginCallBack);
			});
		}

		public static void LoginCallBack(bool result)
		{
			if (!result) return;
			Instance.ShowFloatWindow();
			Instance.PushPlayerInfo(SDKPlayerInfo.Get);
		}
	}
}