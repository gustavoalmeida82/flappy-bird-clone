using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player.transform.position.x > _spriteRenderer.bounds.max.x + 4)
        {
            var position = transform.position;
            position.x += _spriteRenderer.size.x * 2;
            transform.position = position;
        }
    }
}
