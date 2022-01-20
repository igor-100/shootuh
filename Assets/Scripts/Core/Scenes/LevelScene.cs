using UnityEngine;

public class LevelScene : MonoBehaviour
{
    private const string floorMaskName = "Floor";
    private const int levelUpHealMultiplier = 10;

    private IGameCamera GameCam;
    private IWarrior Warrior;
    private IPlayerInput PlayerInput;
    private ILevelSystem LevelSystem;

    private IGameOverScreen GameOverScreen;
    private IPauseScreen PauseScreen;
    private IHUDWeapon HUDWeapon;
    private IHUDLevel HUDLevel;

    private Camera gameCamComponent;
    private CharacterStatModifier levelUpHealthModifier;

    private void Awake()
    {
        GameCam = CompositionRoot.GetGameCamera();
        Warrior = CompositionRoot.GetWarrior();
        PlayerInput = CompositionRoot.GetPlayerInput();
        var saveManager = CompositionRoot.GetSaveManager();

        var enemySpawner = CompositionRoot.GetEnemySpawner();
        var environment = CompositionRoot.GetEnvironment();
        LevelSystem = CompositionRoot.GetLevelSystem();

        var uiRoot = CompositionRoot.GetUIRoot();
        HUDWeapon = CompositionRoot.GetHUDWeapon();
        HUDWeapon.Show();
        HUDLevel = CompositionRoot.GetHUDLevel();
        HUDLevel.Show();
        GameOverScreen = CompositionRoot.GetGameOverScreen();
        GameOverScreen.Hide();
        PauseScreen = CompositionRoot.GetPauseScreen();
        PauseScreen.Hide();

        gameCamComponent = GameCam.CameraComponent;

        PlayerInput.MousePositionUpdated += OnMousePositionUpdated;
        Warrior.StartedDying += OnPlayerDying;
        Warrior.Died += OnPlayerDied;
        LevelSystem.LevelUp += OnLevelUp;
    }

    private void Start()
    {
        GameCam.Target = Warrior.Transform;
        levelUpHealthModifier = new CharacterStatModifier(0.1f, StatModType.PercentAdd);
    }

    private void OnLevelUp(int level)
    {
        Warrior.HealthStat.AddModifier(levelUpHealthModifier);
        Warrior.Heal(level * levelUpHealMultiplier);
        Debug.Log("Level up. Max health is updated: " + Warrior.HealthStat.Value);
    }

    private void OnMousePositionUpdated(Vector3 mousePos)
    {
        RaycastHit objectHit;
        if (Physics.Raycast(gameCamComponent.ScreenPointToRay(mousePos), out objectHit, LayerMask.GetMask(floorMaskName)))
        {
            var floorPoint = objectHit.point;
            Warrior.Rotate(floorPoint);
        }
    }

    private void OnPlayerDying()
    {
        PlayerInput.Disable();
    }

    private void OnPlayerDied(IAlive warrior)
    {
        GameOverScreen.Show();
        HUDWeapon.Hide();
    }
}
