using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private Pipe bottomPipePrefab;
    [SerializeField] private Pipe topPipePrefab;
    [SerializeField] private float minGapSize = 3.0f;
    [SerializeField] private float maxGapSize = 3.0f;
    [SerializeField] private float minGapCenter = 1.5f;
    [SerializeField] private float maxGapCenter = 2.8f;

    public float Width => topPipePrefab.Width;
    private Vector3 PipeSpawnerPosition => transform.position;

    private Pipe _topPipe;
    private Pipe _bottomPipe;
    
    public void SpawnPipes()
    {
        var gapPosY = transform.position.y + Random.Range(-minGapCenter, maxGapCenter);
        var gapSize = Random.Range(minGapSize, maxGapSize);

        _bottomPipe = Instantiate(bottomPipePrefab, PipeSpawnerPosition, Quaternion.identity, transform);
        var bottomPipePos = _bottomPipe.transform.position;
        bottomPipePos.y = (gapPosY - gapSize * 0.5f) - (_bottomPipe.Head.y - _bottomPipe.transform.position.y);
        _bottomPipe.transform.position = bottomPipePos;

        _topPipe = Instantiate(topPipePrefab, PipeSpawnerPosition, Quaternion.identity, transform);
        var topPipePos = _topPipe.transform.position;
        topPipePos.y = (gapPosY + gapSize * 0.5f) + (_topPipe.transform.position.y - _topPipe.Head.y);
        _topPipe.transform.position = topPipePos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(PipeSpawnerPosition + Vector3.up * maxGapCenter, Vector3.one * 0.25f);
        Gizmos.DrawCube(PipeSpawnerPosition - Vector3.up * minGapCenter, Vector3.one * 0.25f);
        Gizmos.DrawLine(PipeSpawnerPosition + Vector3.up * (maxGapCenter + maxGapSize * 0.5f), PipeSpawnerPosition + Vector3.up * (maxGapCenter - maxGapSize * 0.5f));
        Gizmos.DrawLine(PipeSpawnerPosition - Vector3.up * (minGapCenter + maxGapSize * 0.5f), PipeSpawnerPosition - Vector3.up * (minGapCenter - maxGapSize * 0.5f));
    }
}
