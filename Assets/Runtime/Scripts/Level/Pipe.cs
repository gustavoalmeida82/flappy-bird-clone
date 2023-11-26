using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform tailTransform;

    public Vector2 Head => headTransform.position;
    public Vector2 Tail => tailTransform.position;
    public float Width => SpriteRenderer.size.x;
    private SpriteRenderer SpriteRenderer =>
        _spriteRenderer == null 
            ? GetComponentInChildren<SpriteRenderer>() 
            : _spriteRenderer;

    private SpriteRenderer _spriteRenderer;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(Head, Vector2.one * 0.25f);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(Tail, Vector2.one * 0.25f);
    }
}
