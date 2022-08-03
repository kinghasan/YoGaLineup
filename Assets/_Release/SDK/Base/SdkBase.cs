/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SDKBase.cs
//  Info     : SDK 实现基类
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.SDK
{
	public class SDKBase : MonoBehaviour
	{
		public bool IsInited { get; protected set; }
		public bool IsLogIn { get; protected set; }
		public bool IsSwitchAccount { get; protected set; }

		protected Action<bool> OnInited = delegate { };
		protected Action<bool> OnPaid = delegate { };
		protected Action<bool> OnLogined = delegate { };

		public string UserID { get; internal set; }

		#region Init

		public virtual void Init(Action<bool> onDone)
		{
			if (onDone != null)
			{
				OnInited = onDone;
			}
		}

		protected virtual void OnInit(string result)
		{
			IsInited = false;
			OnInited(IsInited);
		}

		#endregion

		#region CheckUpdate

		public virtual void CheckUpdate()
		{

		}

		protected virtual void OnCheckUpdate(string result)
		{

		}

		#endregion

		#region Login

		public virtual void Login(SDKLoginType type, string account, Action<bool> onDone = null)
		{
			switch (type)
			{
				case SDKLoginType.Channel:
					Login(account, onDone);
					break;
			}
		}

		public virtual void Login(string account, Action<bool> onDone = null)
		{

		}

		protected virtual void OnLogin(string result)
		{
		}

		#endregion

		#region SwitchAccount

		public virtual void SwitchAccount(string account)
		{

		}

		protected virtual void OnSwitchAccount(string result)
		{
		}

		#endregion

		#region Logout

		public virtual void Logout()
		{
			IsLogIn = false;
			UserID = null;
		}

		protected virtual void OnLogout(string result)
		{
		}

		#endregion

		#region Pay

		public virtual void Pay(SDKPayInfo info, Action<bool> onDone = null) {
			SDKDebug.Log("Pay " + info);
			OnPaid = onDone;
			OnPay(SDKResult.Success.ToString());
		}

		protected virtual void OnPay(string result) {
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

		#region Download

		protected virtual void OnDownloadStart(string result)
		{
		}

		protected virtual void OnDownloadSuccess(string result)
		{
		}

		protected virtual void OnDownloadFail(string result)
		{
		}

		protected virtual void OnDownLoadProgress(string result)
		{
		}

		#endregion

		#region FloatWindow

		public virtual void ShowFloatWindow()
		{
		}

		protected virtual void HideFloatWindow()
		{
		}

		#endregion

		#region PushPlayerInfo

		public virtual void PushPlayerInfo(SDKPlayerInfo info)
		{

		}

		protected virtual void OnPushPlayerInfo(string result)
		{

		}

		#endregion

		#region ExitGame
		public virtual void ExitGame() {
			OnExitGame(null);
		}

		protected virtual void OnExitGame(string result) {
			Application.Quit();
		} 
		#endregion

		#region Get Info
		public virtual bool IsSupport(string method) {
			return false;
		}

		public virtual string GetDeviceID() {
			return "";
		}

		public virtual string GetUserID() {
			return UserID;
		}

		public virtual string GetLoginID() {
			return GetChannelID() + "_" + GetUserID();
		}

		public virtual int GetChannelID() {
			return 0;
		}

		public virtual string GetChannelName() {
			return "Default";
		}

		public virtual int GetCityId() {
			return 999999;
		}

		public virtual int GetProvinceId() {
			return 999998;
		}

		public virtual int GetNetworkID() {
			return 0;
		}

		public virtual string GetVersionCode() {
			return "1.0.0";
		}

		public virtual string GetTitleName() {
			return "Aya Game Tools";
		}

		public virtual bool IsNetworkConnected() {
			return Application.internetReachability != NetworkReachability.NotReachable;
		}

		public virtual int GetChannelType() {
			return 1;
		}
		#endregion
	}

}
