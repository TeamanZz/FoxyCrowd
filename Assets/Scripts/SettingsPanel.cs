using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Toggle musicMark;
    [SerializeField] private MouseFollowing mouseFollowing;
    [SerializeField] private GameObject vibrationMark;

    private void OnEnable()
    {
        var musicSetting = PlayerPrefs.GetInt("MusicSetting", 1);

        if (musicSetting == 0)
            musicMark.isOn = false;
        else
            musicMark.isOn = true;
        SFXHandler.sFXHandler.PlayWindowOpen();
        mouseFollowing.enabled = false;
    }

    private void OnDisable()
    {
        SFXHandler.sFXHandler.PlayWindowClose();
        mouseFollowing.enabled = true;
    }
}