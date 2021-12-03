using System.Collections;
using UnityEngine;

public class LevelScene : MonoBehaviour
{
    private IGameCamera GameCam;
    private IWarrior Warrior;
    private IGameOverScreen GameOverScreen;

    private void Awake()
    {
        GameCam = CompositionRoot.GetGameCamera();
        Warrior = CompositionRoot.GetWarrior();
        
        var enemySpawner = CompositionRoot.GetEnemySpawner();
        var environment = CompositionRoot.GetEnvironment();
        var uiRoot = CompositionRoot.GetUIRoot();

        GameOverScreen = CompositionRoot.GetGameOverScreen();
        GameOverScreen.Hide();

        Warrior.Died += OnPlayerDied;
    }

    private void Start()
    {
        GameCam.Target = Warrior.Transform;
    }

    private void OnPlayerDied()
    {
        GameOverScreen.Show();
    }
}
