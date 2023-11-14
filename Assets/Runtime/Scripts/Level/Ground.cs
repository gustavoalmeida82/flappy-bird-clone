using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private BoxCollider2D coll;
    private BoxCollider2D Collider2D => coll == null ? coll = GetComponent<BoxCollider2D>() : coll;
    public float Width => Collider2D.size.x;
}
