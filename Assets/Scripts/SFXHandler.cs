using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    public static SFXHandler sFXHandler;

    public AudioClip foxDeath;
    public AudioClip foxMultiply;
    public AudioClip success;
    public AudioClip lose;

    private void Awake()
    {
        sFXHandler = this;
    }

    public void PlayFoxMultiply()
    {
        GetComponent<AudioSource>().PlayOneShot(foxMultiply);
    }

    public void PlaySuccess()
    {
        AudioHandler.audioHandler.audioSource.volume = 0f;
        GetComponent<AudioSource>().PlayOneShot(success);
    }

    public void PlayLose()
    {
        AudioHandler.audioHandler.audioSource.volume = 0f;
        GetComponent<AudioSource>().PlayOneShot(lose);
    }
}
