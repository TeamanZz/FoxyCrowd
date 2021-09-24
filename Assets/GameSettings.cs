using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private AudioHandler audioHandler;
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle vibrationToggle;

    public void OnMusicToggle()
    {
        if (musicToggle.isOn)
        {
            PlayerPrefs.SetInt("MusicSetting", 1);
        }
        else
        {
            PlayerPrefs.SetInt("MusicSetting", 0);
        }
        audioHandler.ToggleMusic();
    }

    public void OnVibrationToggle()
    {

    }
}
