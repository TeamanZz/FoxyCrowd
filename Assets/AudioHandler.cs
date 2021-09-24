using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    private static AudioHandler audioHandler;

    [SerializeField] private AudioClip audioClip;
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

        // AudioSource audioSource;
        // if (TryGetComponent<AudioSource>(out audioSource))
        //     return;

        // audioSource = gameObject.AddComponent<AudioSource>();
        // audioSource.clip = audioClip;
    }
}
