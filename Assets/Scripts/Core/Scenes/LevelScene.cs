using System.Collections;
using UnityEngine;

public class LevelScene : MonoBehaviour
{
    private IGameCamera gameCam;
    private IWarrior warrior;

    private void Awake()
    {
        gameCam = CompositionRoot.GetGameCamera();
        warrior = CompositionRoot.GetWarrior();
        var enemySpawner = CompositionRoot.GetEnemySpawner();
    }

    private void Start()
    {
        // Difference between these 2?
        // gameCam.SetTarget(warrior.Transform);
        gameCam.Target = warrior.Transform;
    }
}
