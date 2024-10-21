using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource AudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip)
    {
        AudioSource.PlayOneShot(audioClip);
    }
}
