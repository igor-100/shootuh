using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    private IResourceManager ResourceManager;
    private IUnitRepository UnitRepository;
    private IWarrior Warrior;
    private EnemySpawnerProperties EnemySpawnerProperties;

    private bool spawn = true;

    private void Awake()
    {
        EnemySpawnerProperties = CompositionRoot.GetConfiguration().GetEnemySpawnerProperties();
        ResourceManager = CompositionRoot.GetResourceManager();
        Warrior = CompositionRoot.GetWarrior();
        UnitRepository = CompositionRoot.GetUnitRepository();
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
            List<SpawnPoint> spawnPoints = wave.SpawnPoints;

            int seed = 0;
            int waveDelay = (int)(spawnPoints.Select(spawnPoint => spawnPoint.EnemiesByNumber)
                .Aggregate(seed, (x, y) => x + y.Values.ToList().Sum()) * wave.MaxSpawnDelay);

            foreach (var spawnPoint in wave.SpawnPoints)
            {
                StartCoroutine(SpawnEnemiesAtSpawnPoint(wave, spawnPoint));
            }
            yield return new WaitForSeconds(waveDelay);
        }
        else
        {
            foreach (var spawnPoint in wave.SpawnPoints)
            {
                yield return StartCoroutine(SpawnEnemiesAtSpawnPoint(wave, spawnPoint));
            }
        }
    }

    private IEnumerator SpawnEnemiesAtSpawnPoint(Wave wave, SpawnPoint spawnPoint)
    {
        foreach (var enemyByNumber in spawnPoint.EnemiesByNumber)
        {
            var enemyType = enemyByNumber.Key;
            var numberOfEnemies = enemyByNumber.Value;

            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnEnemy(spawnPoint, enemyType);

                yield return new WaitForSeconds(Random.Range(wave.MinSpawnDelay, wave.MaxSpawnDelay));
            }
            yield return new WaitForSeconds(Random.Range(wave.MinSpawnDelay, wave.MaxSpawnDelay));
        }
        yield return new WaitForSeconds(wave.DelayAfterWave);
    }

    private void SpawnEnemy(SpawnPoint spawnPoint, EComponents enemyType)
    {
        var spawnPointTransform = new Vector3(spawnPoint.PointPosition.x + Random.Range(-5f, 5f), spawnPoint.PointPosition.y,
                                    spawnPoint.PointPosition.z + Random.Range(-5f, 5f));

        var enemyObj = ResourceManager.GetPooledObject<IEnemy, EComponents>(enemyType);

        var enemy = enemyObj.GetComponent<IEnemy>();
        UnitRepository.AddUnit(enemy);

        enemy.TargetTransform = Warrior.Transform;

        enemyObj.transform.position = spawnPointTransform;
        enemyObj.transform.rotation = transform.rotation;
    }
}
