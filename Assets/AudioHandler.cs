using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    private static AudioHandler audioHandler;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (audioHandler == null)
        {
            audioHandler = this;
        }
        else if (audioHandler != this)
        {
            Destroy(gameObject);
        }
        ToggleMusic();
    }

    public void ToggleMusic()
    {
        int musicSetting = PlayerPrefs.GetInt("MusicSetting", 1);

        if (musicSetting == 0)
        {
            GetComponent<AudioSource>().mute = true;
        }
        else
        {
            GetComponent<AudioSource>().mute = false;

        }
    }
}
