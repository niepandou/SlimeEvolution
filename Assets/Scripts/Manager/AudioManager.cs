using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public AudioData audioData;
    public AudioSource bgmSource;
    public AudioSource fxSource;
    public AudioClip bgmClip;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        { 
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        PlayBgmSound(bgmClip);
    }

    public void PlayFxSound(AudioClip clip)
    {
        fxSource.clip = clip;
        fxSource.Play();
    }
    public void PlayBgmSound(AudioClip audioClip)
    {
        bgmSource.clip = audioClip;
        bgmSource.Play();        
    }
}
