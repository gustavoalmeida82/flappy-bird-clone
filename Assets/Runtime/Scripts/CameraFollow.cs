using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    
    private void LateUpdate()
    {
        var position = transform.position;
        position.x = player.transform.position.x;

        transform.position = position;
    }
}
