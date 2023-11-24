using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private PlayerController _player;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        var speedMultiplier = _player.Velocity.y <= 0 ? 1 : 2;
        animator.SetFloat(PlayerAnimationConstants.SpeedMultiplier, speedMultiplier);
    }

    public void Die()
    {
        animator.enabled = false;
        enabled = false;
    }
}
