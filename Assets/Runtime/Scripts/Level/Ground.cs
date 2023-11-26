using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters frozenParameters;
    
    private BoxCollider2D coll;
    private BoxCollider2D Collider2D => coll == null ? coll = GetComponent<BoxCollider2D>() : coll;
    public float Width => Collider2D.size.x;

    private SpriteRenderer _spriteRenderer;
    public Bounds Bounds => _spriteRenderer.bounds;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            player.MovementParameters = frozenParameters;
            player.OnHitGround();
        }
    }
}
