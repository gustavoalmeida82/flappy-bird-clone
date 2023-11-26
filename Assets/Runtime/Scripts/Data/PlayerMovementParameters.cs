using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerMovementParameters")]
public class PlayerMovementParameters : ScriptableObject
{
    [field: SerializeField] 
    public float FlapMaxHeight { get; private set; } = 5;

    [field: SerializeField] 
    public float FlapPeakTime { get; private set; } = 0.6f;

    [field: SerializeField] 
    public float FlapPercentCancel { get; private set; } = 0.5f;

    [field: SerializeField]
    public float AngleRotation { get; private set; } = 20;

    [field: SerializeField] 
    public float DownRotationSpeed { get; private set; } = 150;

    [field: SerializeField] 
    public float ForwardSpeed { get; private set; } = 2;
    
    public float Gravity => FlapMaxHeight == 0 
        ? 0 
        : (FlapMaxHeight * 2) / FlapPeakTime * FlapPeakTime;
    
    public float FlapVelocity => Gravity * FlapPeakTime;
}
