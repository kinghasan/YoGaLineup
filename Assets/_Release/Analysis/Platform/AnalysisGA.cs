#if GA
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aya.Util;
using Facebook.Unity;
using GameAnalyticsSDK;
using UnityEngine;

namespace Aya.Analysis
{
    public class AnalysisGA : AnalysisBase
    {
        public override void Init()
        {
            base.Init();
        }

        public override void MissionStart(string mission, MissionType type)
        {
            Event("MissionStart", "LevelName", mission);
        }

        public override void MissionCompleted(string mission)
        {
            Event("MissionCompleted", "LevelName", mission);
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

            GameAnalytics.NewDesignEvent(eventID);
        }
    }
}
#endif