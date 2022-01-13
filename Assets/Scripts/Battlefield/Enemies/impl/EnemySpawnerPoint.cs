using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerPoint : MonoBehaviour, IEnemySpawner
{
    private IResourceManager ResourceManager;
    private IUnitRepository UnitRepository;
    private IWarrior Warrior;

    private bool isSpawningTriggered = false;
    private Wave wave;
    private SpawnPoint spawnPoint;

    private void Awake()
    {
        ResourceManager = CompositionRoot.GetResourceManager();
        Warrior = CompositionRoot.GetWarrior();
        UnitRepository = CompositionRoot.GetUnitRepository();
    }

    public void TriggerSpawning(Wave wave, SpawnPoint spawnPoint)
    {
        this.wave = wave;
        this.spawnPoint = spawnPoint;
        isSpawningTriggered = true;
    }

    // Update allows to create a separate thread, that is used for parallel spawning
    private void Update()
    {
        if (isSpawningTriggered)
        {
            isSpawningTriggered = false;
            StartCoroutine(SpawnEnemiesAtSpawnPoint(wave, spawnPoint));
        }
    }

    public IEnumerator SpawnEnemiesAtSpawnPoint(Wave wave, SpawnPoint spawnPoint)
    {
        foreach (var enemyByNumber in spawnPoint.EnemiesByNumber)
        {
            var enemyType = enemyByNumber.Key;
            var numberOfEnemies = enemyByNumber.Value;

            for (int i = 0; i < numberOfEnemies; i++)
            {
                SpawnEnemy(enemyType);

                yield return new WaitForSeconds(UnityEngine.Random.Range(wave.MinSpawnDelay, wave.MaxSpawnDelay));
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(wave.MinSpawnDelay, wave.MaxSpawnDelay));
        }
    }

    private void SpawnEnemy(EComponents enemyType)
    {
        var enemyObj = ResourceManager.GetPooledObject<IEnemy, EComponents>(enemyType);

        var enemy = enemyObj.GetComponent<IEnemy>();
        UnitRepository.AddUnit(enemy);

        enemy.TargetTransform = Warrior.Transform;

        enemyObj.transform.position = new Vector3(transform.position.x + UnityEngine.Random.Range(-5f, 5f), transform.position.y,
                                    transform.position.z + UnityEngine.Random.Range(-5f, 5f));
        enemyObj.transform.rotation = transform.rotation;
        enemyObj.SetActive(true);
    }
}
