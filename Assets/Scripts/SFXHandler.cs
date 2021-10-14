using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXHandler : MonoBehaviour
{
    public static SFXHandler sFXHandler;

    private AudioSource audioSource;

    public List<AudioClip> foxDeathTrap = new List<AudioClip>();
    public List<AudioClip> foxDeathChicken = new List<AudioClip>();
    public AudioClip foxMultiply;
    public AudioClip skinBuy;
    public AudioClip buttonsTap;
    public AudioClip endHoleEnter;
    public AudioClip windowClose;
    public AudioClip windowOpen;
    public AudioClip success;
    public AudioClip lose;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        if (sFXHandler == null)
        {
            sFXHandler = this;
        }
        else if (sFXHandler != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayWindowOpen()
    {
        audioSource.PlayOneShot(windowOpen);
    }

    public void PlayStartMoving()
    {
        // audioSource.PlayOneShot(foxDeathChicken[1]);
        audioSource.PlayOneShot(windowClose);
    }

    public void PlayWindowClose()
    {
        audioSource.PlayOneShot(windowClose);
    }

    public void PlayEndHoleEnter()
    {
        audioSource.PlayOneShot(endHoleEnter);
    }

    public void PlayTap()
    {
        audioSource.PlayOneShot(buttonsTap);
    }

    public void PlaySkinBuy()
    {
        audioSource.PlayOneShot(skinBuy);
    }

    public void PlayFoxMultiply()
    {
        audioSource.PlayOneShot(foxMultiply);
    }

    public void PlaySuccess()
    {
        AudioHandler.audioHandler.audioSource.volume = 0f;
        audioSource.PlayOneShot(success);
    }

    public void PlayLose()
    {
        AudioHandler.audioHandler.audioSource.volume = 0f;
        audioSource.PlayOneShot(lose);
    }
}
