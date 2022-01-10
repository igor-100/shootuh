using System.Collections.Generic;

public struct EnemySpawnerProperties
{
    public List<EnemiesWave> EnemiesWaves;
    public float MaxSpawnXDistance;
    public float MaxSpawnZDistance;
}

public struct EnemiesWave
{
    public float MinSpawnDelay;
    public float MaxSpawnDelay;
    public Dictionary<EComponents, int> EnemiesByNumber;
}
