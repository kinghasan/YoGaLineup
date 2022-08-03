/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AnalysisManager.cs
//  Info     : 统计分析管理器
//  Author   : ls9512
//  E-mail   : 598914653@qq.com
//
//  Copyright : Aya Game Studio 2017
//
/////////////////////////////////////////////////////////////////////////////
//using GameClient;

using System;
using UnityEngine;

namespace Aya.Analysis 
{
	public static class AnalysisManager 
	{
		public static AnalysisBase Instance { get; private set; }
	    public static bool EnableAnalysis = true;

		#region UserID
		public static string UserID
		{
			get
			{
				if (string.IsNullOrEmpty(_userID))
				{
					_userID = SystemInfo.deviceUniqueIdentifier;
				}
				return _userID;
			}
		}
		private static string _userID;

		public static string AnalysisUserID
		{
			get
			{
				var analysisUserID = UserID + "_" + RegisterTime + "+" + DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
				return analysisUserID;
			}
		}
		#endregion

		#region Setting
		// 第一次启动游戏的时间
		public static string RegisterTime
		{
			get
			{
				var str = PlayerPrefs.GetString("FirstLaunchTime", "");
				if (string.IsNullOrEmpty(str))
				{
					str = DateTime.UtcNow.AddHours(8).ToString("yyyyMMdd");
					PlayerPrefs.SetString("FirstLaunchTime", str);
				}
				return str;
			}
		}
        #endregion

        #region Setting
        // 第一次启动游戏的版本
        public static string RegisterVersion
        {
            get
            {
                var str = PlayerPrefs.GetString("FirstLaunchVersion", "");
                if (string.IsNullOrEmpty(str))
                {
                    str = Application.version;
                    PlayerPrefs.SetString("FirstLaunchVersion", str);
                }
                return str;
            }
        }
        #endregion

        #region Setting
        // 第一次启动游戏的时间
        public static string EventTime
		{
			get
			{
				return DateTime.UtcNow.AddHours(8).ToString("yyyyMMdd-HHmmss");
			}
		}
		#endregion

		static AnalysisManager() 
		{
			AnalysisDebug.Log("Analysis Init.");
			CreateSdkInstance();
		}

		private static void CreateSdkInstance() 
		{
		    if (EnableAnalysis)
            {
#if UNITY_ANDROID || UNITY_IOS

				// TODO..
#if TapNation
                AnalysisDebug.Log("Analysis TapNation.");
				Instance = new AnalysisTapNation();
#endif

#elif UNITY_STANDALONE
				AnalysisDebug.Log("Analysis not use");
				Instance = new AnalysisBase();
#endif
			}

			if (Instance == null)
            {
                AnalysisDebug.Log("Analysis not use.");
                Instance = new AnalysisBase();
            }

            Instance.Init();
		}
	}
}
