/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SDKAndroidBase.cs
//  Info     : SDK 安卓实现
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using Aya.Platform;
using UnityEngine;

namespace Aya.SDK
{
	public class SDKAndroidBase : SDKBase
	{
		#region Unity call Android

		protected AndroidHelper AndroidHelper
		{
			get
			{
				if (_androidHelper == null)
				{
					_androidHelper = new AndroidHelper(SDKGlobal.DefaultSDKPackageName, "getInstance");
				}
				return _androidHelper;
			}
		}
		private AndroidHelper _androidHelper;
		#endregion

		#region Init

		public override void Init(Action<bool> onDone)
		{
			base.Init(onDone);
			SDKDebug.Log("init");
			AndroidHelper.Invoke("init");
		}

		protected override void OnInit(string result)
		{
			SDKResult.OnResult(result,
				ret =>
				{
					IsInited = true;
					OnInited(IsInited);
				},
				(code, reason) =>
				{
					// 初始化失败，直接退出游戏
					Application.Quit();
				});
		}

		#endregion

		#region CheckUpdate

		public override void CheckUpdate()
		{
			SDKDebug.Log("checkUpdate");
			AndroidHelper.Invoke("checkUpdate");
		}

		protected override void OnCheckUpdate(string result)
		{
			SDKResult.OnResult(result);
		}

		#endregion

		#region Login

		public override void Login(SDKLoginType type, string account, Action<bool> onDone = null)
		{
			switch (type)
			{
				case SDKLoginType.Channel:
					Login(account, onDone);
					break;
				default:
					AndroidHelper.Invoke("login", (int) type, account);
					break;
			}
		}

		public override void Login(string account, Action<bool> onDone = null)
		{
			SDKDebug.Log("Login " + account);
			AndroidHelper.Invoke("login", account);
			if (onDone != null)
			{
				OnLogined = onDone;
			}
		}

		protected override void OnLogin(string result)
		{
			SDKResult.OnResult(result,
				ret =>
				{
					var playerID = ret["playerID"];
					if (string.IsNullOrEmpty(playerID))
					{
						SDKDebug.Log("Login SDK Fail ! plsyrtID is null !");
						OnLogined(false);
					}
					else
					{
						// TODO.. 游戏的登陆逻辑
						OnLogined(true);
					}
				},
				(code, reason) =>
				{
					OnLogined(false);
				});
		}

		#endregion

		#region Logout

		public override void Logout()
		{
			SDKDebug.Log("Logout");
			AndroidHelper.Invoke("logout");
		}

		protected override void OnLogout(string result)
		{
			SDKResult.OnResult(result,
				ret =>
				{
					IsLogIn = false;
					UserID = null;
					// TODO.. 游戏登出逻辑，返回登陆界面
				});
		}

		#endregion

		#region SwitchAccount

		public override void SwitchAccount(string account)
		{
			SDKDebug.Log("SwitchAccount " + account);
			AndroidHelper.Invoke("login", account);
		}

		protected override void OnSwitchAccount(string result)
		{
			SDKResult.OnResult(result, ret =>
			{
				OnLogout(SDKResult.Success.ToString());
				Login("");
			});
		}

		#endregion

		#region FloatWindow

		public override void ShowFloatWindow()
		{
			SDKDebug.Log("ShowFloatWindow ");
			AndroidHelper.Invoke("showFloatWindow");
		}

		protected override void HideFloatWindow()
		{
			SDKDebug.Log("HideFloatWindow ");
			AndroidHelper.Invoke("hideFloatWindow");
		}

		#endregion

		#region PushPlayerInfo

		public override void PushPlayerInfo(SDKPlayerInfo info)
		{
			SDKDebug.Log("PushPlayerInfo ");
			if (info != null)
			{
				AndroidHelper.Invoke("pushPlayerInfo", info.ToString());
			}
		}

		protected override void OnPushPlayerInfo(string result)
		{
			SDKResult.OnResult(result);
		}

		#endregion

		#region Pay

		public override void Pay(SDKPayInfo info, Action<bool> onDone)
		{
			SDKDebug.Log("Pay " + info);
			OnPaid = onDone;
			AndroidHelper.Invoke("pay", info.ToString());
		}

		protected override void OnPay(string result)
		{
			SDKResult.OnResult(result,
				ret => {
					if (OnPaid != null)
					{
						OnPaid(true);
					}
				},
				(code, reason) => {
					if (OnPaid != null)
					{
						OnPaid(false);
					}
				});
		}

		#endregion

		#region ExitGame

		public override void ExitGame()
		{
			SDKDebug.Log("Exit Game");
			AndroidHelper.Invoke("exitGame");
		}

		protected override void OnExitGame(string result)
		{
			SDKResult.OnResult(result, ret =>
			{
				// TODO.. 游戏退出逻辑
				Application.Quit();
			});
		}

		#endregion

		#region Get Info

		public override string GetUserID()
		{
			return AndroidHelper.Invoke<string>("getUserID");
		}

		public override bool IsSupport(string method)
		{
			return AndroidHelper.Invoke<bool>("isSupport", method);
		}

		//版本号
		public override string GetVersionCode()
		{
			return AndroidHelper.Invoke<string>("getVersionCode");
		}

		//运营商ID
		public override int GetNetworkID()
		{
			return AndroidHelper.Invoke<int>("getNetworkID");
		}

		//设备ID
		public override string GetDeviceID()
		{
			if (string.IsNullOrEmpty(UserID))
			{
				UserID = AndroidHelper.Invoke<string>("getDeviceID");
			}
			return UserID;
		}

		//渠道ID
		public override int GetChannelID()
		{
			return AndroidHelper.Invoke<int>("getChannelID");
		}

		//渠道名称
		public override string GetChannelName()
		{
			return AndroidHelper.Invoke<string>("getChannelName");
		}

		//省份ID
		public override int GetProvinceId()
		{
			return AndroidHelper.Invoke<int>("getProvinceId");
		}

		//城市ID
		public override int GetCityId()
		{
			return AndroidHelper.Invoke<int>("getCityId");
		}

		#endregion
	}
}