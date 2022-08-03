using UnityEngine;

namespace Aya.SDK 
{
	public static class SDKDebug
	{
		public static bool ShowLog = SDKSetting.Ins.DebugLog;

        #region Log / Warning / Error

        public static void Log(string msg)
        {
            Log("SDK", msg);
        }

        public static void Log(string title, string msg)
        {
            if (ShowLog)
            {
                Debug.Log("<color=green>[" + title + "]</color>\t" + msg);
            }
        }

        public static void Warning(string msg)
        {
            Warning("SDK", msg);
        }

        public static void Warning(string title, string msg)
        {
            if (ShowLog)
            {
                Debug.Log("<color=yellow>[" + title + "]</color>\t" + msg);
            }
        }

        public static void Error(string msg)
        {
            Warning("SDK", msg);
        }

        public static void Error(string title, string msg)
        {
            if (ShowLog)
            {
                Debug.Log("<color=red>[" + title + "]</color>\t" + msg);
            }
        } 
        #endregion
    }
}

