using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    private EnemySpawnerProperties EnemySpawnerProperties;

    private bool spawn = true;

    private void Awake()
    {
        EnemySpawnerProperties = CompositionRoot.GetConfiguration().GetEnemySpawnerProperties();
    }

    private IEnumerator Start()
    {
        while (spawn)
        {
            yield return StartCoroutine(SpawnAllWaves(EnemySpawnerProperties.Waves));
        }
    }

    private IEnumerator SpawnAllWaves(List<Wave> waves)
    {
        foreach (var wave in waves)
        {
            yield return StartCoroutine(SpawnWave(wave));
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Wave " + wave.Id);
        if (wave.IsParallelSpawning)
        {
            yield return StartCoroutine(SpawnWaveInParallel(wave));
        }
        else
        {
            yield return StartCoroutine(SpawnWaveSequentially(wave));
        }
        yield return new WaitForSeconds(wave.DelayAfterWave);
    }

    private IEnumerator SpawnWaveInParallel(Wave wave)
    {
        List<SpawnPoint> spawnPoints = wave.SpawnPoints;

        int seed = 0;
        int waveDelay = (int)(spawnPoints.Select(spawnPoint => spawnPoint.EnemiesByNumber)
            .Aggregate(seed, (x, y) => x + y.Values.ToList().Sum()) * wave.MaxSpawnDelay);

        for (int i = 0; i < wave.SpawnPoints.Count; i++)
        {
            SpawnPoint spawnPoint = wave.SpawnPoints[i];

            var gameObject = new GameObject("EnemySpawnerPoint", typeof(EnemySpawnerPoint));
            gameObject.transform.position = spawnPoint.PointPosition;
            var spawnerPoint = gameObject.GetComponent<EnemySpawnerPoint>();

            spawnerPoint.TriggerSpawning(wave, spawnPoint);
        }
        yield return new WaitForSeconds(waveDelay);
    }

    private IEnumerator SpawnWaveSequentially(Wave wave)
    {
        foreach (var spawnPoint in wave.SpawnPoints)
        {
            var gameObject = new GameObject("EnemySpawnerPoint", typeof(EnemySpawnerPoint));
            gameObject.transform.position = spawnPoint.PointPosition;
            var spawnerPoint = gameObject.GetComponent<EnemySpawnerPoint>();

            yield return StartCoroutine(spawnerPoint.SpawnEnemiesAtSpawnPoint(wave, spawnPoint));
        }
    }
}
