using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioService : MonoBehaviour
{
    private AudioSource _sfxSource;

    private void Awake()
    {
        _sfxSource = GetComponent<AudioSource>();
        _sfxSource.loop = false;
    }

    public void PlayAudioCue(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
}
