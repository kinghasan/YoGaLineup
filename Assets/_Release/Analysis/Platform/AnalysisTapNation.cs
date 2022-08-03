#if TapNation
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aya.Util;
using Facebook.Unity;
using FunGames.Sdk.Analytics;
using GameAnalyticsSDK;
using UnityEngine;

namespace Aya.Analysis
{
    public class AnalysisTapNation : AnalysisBase
    {
        public override void Init()
        {
            base.Init();
        }

        public override void LevelStart(string level)
        {
            FunGamesAnalytics.NewProgressionEvent("Start", level);
        }

        public override void LevelCompleted(string level)
        {
            FunGamesAnalytics.NewProgressionEvent("Complete", level);
        }

        public override void LevelFailed(string level)
        {
            FunGamesAnalytics.NewProgressionEvent("Fail", level);
        }

        public override void Event(string eventID, Dictionary<string, object> args = null)
        {
            if (AnalysisDebug.IsDebug)
            {
                string str = "Event :" + eventID;
                if (args != null)
                    foreach (var Dic in args)
                    {
                        str += (" + " + Dic.Key + " : " + Dic.Value);
                    }
                AnalysisDebug.Log(str);
            }

            FunGamesAnalytics.NewDesignEvent(eventID, args);
        }
    }
}
#endif