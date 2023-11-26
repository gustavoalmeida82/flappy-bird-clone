using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
public class ScoreUpTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip scoreUpAudio;
    
    private void Awake()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            AudioUtility.PlayAudioCue(scoreUpAudio);
            player.ScoreUp();
        }
    }
}
