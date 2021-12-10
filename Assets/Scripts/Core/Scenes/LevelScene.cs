﻿using UnityEngine;

public class LevelScene : MonoBehaviour
{
    private IGameCamera GameCam;
    private IWarrior Warrior;
    private IPlayerInput PlayerInput;
    private IGameOverScreen GameOverScreen;
    private IPauseScreen PauseScreen;
    private IHUD HUD;

    private void Awake()
    {
        GameCam = CompositionRoot.GetGameCamera();
        Warrior = CompositionRoot.GetWarrior();

        PlayerInput = CompositionRoot.GetPlayerInput();
        var enemySpawner = CompositionRoot.GetEnemySpawner();
        var environment = CompositionRoot.GetEnvironment();
        var uiRoot = CompositionRoot.GetUIRoot();

        HUD = CompositionRoot.GetHUD();
        HUD.Show();
        GameOverScreen = CompositionRoot.GetGameOverScreen();
        GameOverScreen.Hide();
        PauseScreen = CompositionRoot.GetPauseScreen();
        PauseScreen.Hide();

        Warrior.Died += OnPlayerDied;
    }

    private void Start()
    {
        GameCam.Target = Warrior.Transform;
        Warrior.SetCamera(GameCam.CameraComponent);
    }

    private void OnPlayerDied()
    {
        PlayerInput.Disable();
        GameOverScreen.Show();
        HUD.Hide();
    }
}
