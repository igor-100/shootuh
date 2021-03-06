using Assets.Scripts.Core.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

public class CompositionRoot : MonoBehaviour
{
    private static IUIRoot UIRoot;
    private static IWarrior Warrior;
    private static IEnemySpawner EnemySpawner;
    private static GameObject EnvironmentGameObject;
    private static IPlayerInput PlayerInput;
    private static IGameCamera GameCamera;
    private static IViewFactory ViewFactory;
    private static ISceneLoader SceneLoader;
    //private static EventSystem EventSystem;
    private static ILevelSystem LevelSystem;
    private static IUnitRepository UnitRepository;
    private static IResourceManager ResourceManager;
    private static ISaveManager SaveManager;
    private static IAudioManager AudioManager;
    private static IConfiguration Configuration;

    private static IHUDWeapon HUDWeapon;
    private static IHUDLevel HUDLevel;
    private static IGameOverScreen GameOverScreen;
    private static IMainMenuScreen MainMenuScreen;
    private static ILoadScreen LoadScreen;
    private static IPauseScreen PauseScreen;

    private void OnDestroy()
    {
        UIRoot = null;
        Warrior = null;
        EnemySpawner = null;
        EnvironmentGameObject = null;
        PlayerInput = null;
        GameCamera = null;
        ViewFactory = null;
        Configuration = null;
        UnitRepository = null;
        LevelSystem = null;
        //EventSystem = null;

        HUDWeapon = null;
        HUDLevel = null;
        GameOverScreen = null;
        MainMenuScreen = null;
        PauseScreen = null;
        LoadScreen = null;

        var resourceManager = GetResourceManager();
        resourceManager.ResetPools();
    }

    public static IResourceManager GetResourceManager()
    {
        if (ResourceManager == null)
        {
            ResourceManager = new ResourceManager();
        }

        return ResourceManager;
    }

    public static IGameCamera GetGameCamera()
    {
        if (GameCamera == null)
        {
            var resourceManager = GetResourceManager();
            GameCamera = resourceManager.CreatePrefabInstance<IGameCamera, EComponents>(EComponents.Main_Camera);
        }

        return GameCamera;
    }

    public static IWarrior GetWarrior()
    {
        if (Warrior == null)
        {
            var resourceManager = GetResourceManager();
            Warrior = resourceManager.CreatePrefabInstance<IWarrior, EComponents>(EComponents.Warrior);
        }

        return Warrior;
    }

    public static IWarrior GetWarrior(string data)
    {
        if (Warrior == null)
        {
            var resourceManager = GetResourceManager();
            Warrior = resourceManager.CreatePrefabInstance<IWarrior, EComponents>(EComponents.Warrior);
        }

        return Warrior;
    }

    public static IEnemySpawner GetEnemySpawner()
    {
        if (EnemySpawner == null)
        {
            var resourceManager = GetResourceManager();
            EnemySpawner = resourceManager.CreatePrefabInstance<IEnemySpawner, EComponents>(EComponents.Enemy_Spawner);
        }

        return EnemySpawner;
    }

    public static GameObject GetEnvironment()
    {
        if (EnvironmentGameObject == null)
        {
            var resourceManager = GetResourceManager();
            EnvironmentGameObject = resourceManager.CreatePrefabInstance<EComponents>(EComponents.Environment);
        }

        return EnvironmentGameObject;
    }

    public static ISceneLoader GetSceneLoader()
    {
        if (SceneLoader == null)
        {
            var resourceManager = GetResourceManager();
            SceneLoader = resourceManager.CreatePrefabInstance<ISceneLoader, EComponents>(EComponents.SceneLoader);
        }

        return SceneLoader;
    }

    public static IConfiguration GetConfiguration()
    {
        if (Configuration == null)
        {
            Configuration = new Configuration();
        }

        return Configuration;
    }

    public static ILevelSystem GetLevelSystem()
    {
        if (LevelSystem == null)
        {
            var gameObject = new GameObject("LevelSystem");
            LevelSystem = gameObject.AddComponent<LevelSystem>();
        }

        return LevelSystem;
    }

    public static ISaveManager GetSaveManager()
    {
        if (SaveManager == null)
        {
            var gameObject = new GameObject("SaveManager");
            SaveManager = gameObject.AddComponent<SaveManager>();
        }

        return SaveManager;
    }

    public static IAudioManager GetAudioManager()
    {
        if (AudioManager == null)
        {
            var gameObject = new GameObject("AudioManager");
            AudioManager = gameObject.AddComponent<AudioManager>();
        }

        return AudioManager;
    }

    public static IUnitRepository GetUnitRepository()
    {
        if (UnitRepository == null)
        {
            UnitRepository = new UnitRepository();
        }

        return UnitRepository;
    }

    public static IUIRoot GetUIRoot()
    {
        if (UIRoot == null)
        {
            var resourceManager = GetResourceManager();
            UIRoot = resourceManager.CreatePrefabInstance<IUIRoot, EComponents>(EComponents.UIRoot);
        }

        return UIRoot;
    }

    public static IViewFactory GetViewFactory()
    {
        if (ViewFactory == null)
        {
            var uiRoot = GetUIRoot();
            var resourceManager = GetResourceManager();

            ViewFactory = new ViewFactory(uiRoot, resourceManager);
        }

        return ViewFactory;
    }

    public static IGameOverScreen GetGameOverScreen()
    {
        if (GameOverScreen == null)
        {
            var gameObject = new GameObject("GameOverScreen");
            GameOverScreen = gameObject.AddComponent<GameOverScreen>();
        }

        return GameOverScreen;
    }

    public static IPauseScreen GetPauseScreen()
    {
        if (PauseScreen == null)
        {
            var gameObject = new GameObject("PauseScreen");
            PauseScreen = gameObject.AddComponent<PauseScreen>();
        }

        return PauseScreen;
    }

    public static IMainMenuScreen GetMainMenuScreen()
    {
        if (MainMenuScreen == null)
        {
            var gameObject = new GameObject("MainMenuScreen");
            MainMenuScreen = gameObject.AddComponent<MainMenuScreen>();
        }

        return MainMenuScreen;
    }

    public static ILoadScreen GetLoadScreen()
    {
        if (LoadScreen == null)
        {
            var gameObject = new GameObject("LoadScreen");
            LoadScreen = gameObject.AddComponent<LoadScreen>();
        }

        return LoadScreen;
    }

    public static IHUDWeapon GetHUDWeapon()
    {
        if (HUDWeapon == null)
        {
            var gameObject = new GameObject("HUDWeapon");
            HUDWeapon = gameObject.AddComponent<HUDWeapon>();
        }

        return HUDWeapon;
    }

    public static IHUDLevel GetHUDLevel()
    {
        if (HUDLevel == null)
        {
            var gameObject = new GameObject("HUDLevel");
            HUDLevel = gameObject.AddComponent<HUDLevel>();
        }

        return HUDLevel;
    }

    public static IPlayerInput GetPlayerInput()
    {
        if (PlayerInput == null)
        {
            var gameObject = new GameObject("PlayerInput");
            PlayerInput = gameObject.AddComponent<PlayerInput>();
        }

        return PlayerInput;
    }
}
