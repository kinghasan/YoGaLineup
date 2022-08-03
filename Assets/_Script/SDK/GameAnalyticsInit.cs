#if GA
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsInit : MonoBehaviour
{
    public void Awake()
    {
        GameAnalytics.Initialize();
    }

    public void Start()
    {
        GameAnalytics.NewDesignEvent("Game Start");
    }

}
#endif