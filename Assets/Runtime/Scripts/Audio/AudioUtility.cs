using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioUtility
{
    public static AudioService AudioService { private get; set; }

    public static void PlayAudioCue(AudioClip clip)
    {
        if (clip == null) return;
        AudioService.PlayAudioCue(clip);
    }
}
