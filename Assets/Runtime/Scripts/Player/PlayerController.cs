using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private AudioClip flapAudio;
    
    public PlayerMovementParameters MovementParameters { get; set; }
    public Vector3 Velocity => _velocity;
    public bool IsDead { get; private set; }
    public bool IsGrounded { get; private set; }

    private PlayerInput _input;
    private Vector3 _velocity;
    private float _zRotation;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        ApplyGravity();
        ProcessDownRotation();
        ProcessForwardMovement();
        ProcessInput();
        
        transform.rotation = Quaternion.Euler(Vector3.forward * _zRotation);
        transform.position += _velocity * Time.deltaTime;
    }

    private void ProcessForwardMovement()
    {
        _velocity.x = MovementParameters.ForwardSpeed;
    }

    private void ProcessInput()
    {
        if (_input.Press())
        {
            Flap();
        }

        if (_input.Release() && _velocity.y > 0)
        {
            _velocity.y *= MovementParameters.FlapPercentCancel;
        }
    }

    public void Flap()
    {
        AudioUtility.PlayAudioCue(flapAudio);
        _velocity.y = MovementParameters.FlapVelocity;
        _zRotation = MovementParameters.AngleRotation;
    }

    private void ApplyGravity()
    {
        _velocity.y -= MovementParameters.Gravity * Time.deltaTime;
    }

    private void ProcessDownRotation()
    {
        if (_velocity.y < 0)
        {
            _zRotation -= MovementParameters.DownRotationSpeed * Time.deltaTime;
            _zRotation = Mathf.Max(_zRotation, -MovementParameters.AngleRotation);
        }
    }

    public void Die()
    {
        if (IsDead) return;

        IsDead = true;
        _velocity = Vector3.zero;
        _input.enabled = false;
        
        var animationController = GetComponent<PlayerAnimationController>();
        if (animationController != null)
        {
            animationController.Die();
        }
        
        gameMode.GameOver();
    }

    public void ScoreUp()
    {
        gameMode.ScoreUp();
    }

    public void OnHitGround()
    {
        IsGrounded = true;
        enabled = false;
    }
}
