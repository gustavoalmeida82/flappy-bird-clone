using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Flap parameters")]
    [SerializeField] private float flapMaxHeight = 10.0f;
    [SerializeField] private float flapPeakTime = 5.0f;
    [Range(0, 1)]
    [SerializeField] private float flapPercentCancel = 0.1f;
    
    [Header("Rotation")]
    [Range(0, 180)]
    [SerializeField] private float angleRotation = 20;
    [SerializeField] private float downRotationSpeed = 150;
    
    [Header("Movement")]
    [SerializeField] private float forwardSpeed = 10f;

    public float Gravity => (flapMaxHeight * 2) / flapPeakTime * flapPeakTime;
    public float FlapVelocity => Gravity * flapPeakTime;

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
        _velocity.x = forwardSpeed;
    }

    private void ProcessInput()
    {
        if (_input.Press())
        {
            _velocity.y = FlapVelocity;
            _zRotation = angleRotation;
        }

        if (_input.Release() && _velocity.y > 0)
        {
            _velocity.y *= flapPercentCancel;
        }
    }

    private void ApplyGravity()
    {
        _velocity.y -= Gravity * Time.deltaTime;
    }

    private void ProcessDownRotation()
    {
        if (_velocity.y < 0)
        {
            _zRotation -= downRotationSpeed * Time.deltaTime;
            _zRotation = Mathf.Max(_zRotation, -angleRotation);
        }
    }
}
