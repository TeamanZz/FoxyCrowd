using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
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
        GameObject.FindObjectOfType<AudioHandler>().ToggleMusic();
        SFXHandler.sFXHandler.PlayTap();
    }

    public void OnVibrationToggle()
    {

    }
}
