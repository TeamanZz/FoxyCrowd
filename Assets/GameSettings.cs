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

        }
    }

    public void OnVibrationToggle()
    {

    }
}
