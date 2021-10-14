using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsInit : MonoBehaviour
{
    void Start()
    {
        GameAnalytics.Initialize();
    }
}