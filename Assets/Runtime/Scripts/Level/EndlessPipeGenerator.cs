using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPipeGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private List<Ground> Grounds;
    [SerializeField] private PipeSpawner pipeSpawnerPrefab;
    [SerializeField] private float initialDistance = 10;
    [SerializeField] private int initialSpawnAmount = 10;
    [SerializeField] private float gapBetweenPipeSpawners = 5;
    
    private Camera _camera;
    private List<PipeSpawner> _pipeSpawners = new List<PipeSpawner>();

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        UpdatePipeSpawners();
        UpdateGrounds();
    }

    private void UpdatePipeSpawners()
    {
        if (_pipeSpawners.Count <= 0) return;
        
        var lastPipeSpawner = _pipeSpawners[^1];
        if (IsPipeSpawnerVisible(lastPipeSpawner))
        {
            SpawnMultiplesPipes(2);
        }

        var index = GetIndex();
        if (index < 0) return;

        for (var i = 0; i <= index; i++)
        {
            Destroy(_pipeSpawners[i].gameObject);
        }
            
        _pipeSpawners.RemoveRange(0, index + 1);
    }

    private int GetIndex()
    {
        for (var i = _pipeSpawners.Count - 1; i >= 0; i--)
        {
            var pipe = _pipeSpawners[i];
            if (!IsPipeSpawnerVisible(pipe) && player.transform.position.x > pipe.transform.position.x)
            {
                return i;
            }
        }

        return -1;
    }

    private void SpawnMultiplesPipes(int amount)
    {
        if (_pipeSpawners.Count == 0) return;
        
        for (var i = 0; i < amount; i++)
        {
            var lastPipeSpawner = _pipeSpawners[^1];
            var pipePosition = transform.position;
            pipePosition.x = lastPipeSpawner.transform.position.x + gapBetweenPipeSpawners;
            SpawnSinglePipe(pipePosition);
        }
    }

    private void SpawnSinglePipe(Vector3 position)
    {
        var pipeSpawner = Instantiate(pipeSpawnerPrefab, position, Quaternion.identity, transform);
        _pipeSpawners.Add(pipeSpawner);
        pipeSpawner.SpawnPipes();
    }

    private void UpdateGrounds()
    {
        foreach (var ground in Grounds)
        {
            if (!IsGroundVisible(ground) && ground.transform.position.x < player.transform.position.x)
            {
                var groundPosition = ground.transform.position;
                groundPosition.x += ground.Width * 2;
                ground.transform.position = groundPosition;
            }
        }
    }

    private bool IsPipeSpawnerVisible(PipeSpawner pipe)
    {
        return IsBoxVisibleXOnly(pipe.transform.position, pipe.Width);
    }

    private bool IsGroundVisible(Ground ground)
    {
        return IsBoxVisibleXOnly(ground.transform.position, ground.Width);
    }

    private bool IsBoxVisibleXOnly(Vector3 center, float width)
    {
        var left = center - Vector3.right * (width * 0.5f);
        var right = center + Vector3.right * (width * 0.5f);

        var leftClipPos = _camera.WorldToViewportPoint(left);
        var rightClipPos = _camera.WorldToViewportPoint(right);
        
        return !(leftClipPos.x >= 1 || rightClipPos.x <= 0);
    }

    public void StartPipeSpawn()
    {
        SpawnSinglePipe(transform.position + Vector3.right * initialDistance);
        SpawnMultiplesPipes(initialSpawnAmount - 1);
    }
}
