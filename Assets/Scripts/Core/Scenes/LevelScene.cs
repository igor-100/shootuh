using UnityEngine;

public class LevelScene : MonoBehaviour
{
    private const string FloorMaskName = "Floor";

    private IGameCamera GameCam;
    private IWarrior Warrior;
    private IPlayerInput PlayerInput;
    private IGameOverScreen GameOverScreen;
    private IPauseScreen PauseScreen;
    private IHUD HUD;

    private Camera gameCamComponent;

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

        gameCamComponent = GameCam.CameraComponent;

        PlayerInput.MousePositionUpdated += OnMousePositionUpdated;
        Warrior.StartedDying += OnPlayerDying;
        Warrior.Died += OnPlayerDied;
    }

    private void OnMousePositionUpdated(Vector3 mousePos)
    {
        RaycastHit objectHit;
        if (Physics.Raycast(gameCamComponent.ScreenPointToRay(mousePos), out objectHit, LayerMask.GetMask(FloorMaskName)))
        {
            var floorPoint = objectHit.point;
            Warrior.Rotate(floorPoint);
        }
    }

    private void Start()
    {
        GameCam.Target = Warrior.Transform;
    }

    private void OnPlayerDying()
    {
        PlayerInput.Disable();
    }

    private void OnPlayerDied()
    {
        GameOverScreen.Show();
        HUD.Hide();
    }
}
