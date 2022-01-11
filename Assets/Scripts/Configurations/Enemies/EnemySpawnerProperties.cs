using System.Collections.Generic;
using UnityEngine;

public struct EnemySpawnerProperties
{
    public List<Wave> Waves;
}

public struct Wave
{
    public int Id;
    public float MinSpawnDelay;
    public float MaxSpawnDelay;
    public float DelayAfterWave;
    public bool IsParallelSpawning;
    public List<SpawnPoint> SpawnPoints;
}

public struct SpawnPoint
{
    public Vector3 PointPosition;
    public Dictionary<EComponents, int> EnemiesByNumber;
}
