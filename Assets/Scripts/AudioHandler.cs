using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler audioHandler;
    [HideInInspector] public AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
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
            audioSource.mute = true;
        }
        else
        {
            audioSource.mute = false;
        }
    }
}
