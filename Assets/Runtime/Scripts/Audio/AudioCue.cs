using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCue : MonoBehaviour
{
    [SerializeField] private AudioClip cue;

    public void Play()
    {
        AudioUtility.PlayAudioCue(cue);
    }
}
