using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPrefsTool : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("PlayerPrefs/Clear Player Prefs")]
    static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    [MenuItem("PlayerPrefs/Add1000PP")]

    static void Add1000PP()
    {
        var oldValue = PlayerPrefs.GetInt("PopulationPoints");

        oldValue += 1000;
        PlayerPrefs.SetInt("PopulationPoints", oldValue);
    }
#endif
}
