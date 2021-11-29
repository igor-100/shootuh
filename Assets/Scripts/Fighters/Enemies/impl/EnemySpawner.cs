using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    [SerializeField] private float minSpawnDelay = 3f;
    [SerializeField] private float maxSpawnDelay = 10f;
    [SerializeField] private float maxSpawnXDistance = 75f;
    [SerializeField] private float maxSpawnZDistance = 75f;

    private IResourceManager resourceManager;

    private bool spawn = true;
    private IWarrior warrior;

    private void Awake()
    {
        resourceManager = CompositionRoot.GetResourceManager();
        warrior = CompositionRoot.GetWarrior();
    }

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        while (spawn)
        {
            yield return StartCoroutine(SpawnEnemy());
        }
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        Vector3 randomSpot = new Vector3(Random.Range(maxSpawnXDistance, -maxSpawnXDistance), 0, Random.Range(maxSpawnZDistance, -maxSpawnZDistance));

        var enemyObj = resourceManager.GetPooledObject<IEnemy, EComponents>(EComponents.Enemy_HM);
        var enemy = enemyObj.GetComponent<IEnemy>();
        enemy.TargetTransform = warrior.Transform;
        enemyObj.transform.position = randomSpot;
        enemyObj.transform.rotation = transform.rotation;
        enemyObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
