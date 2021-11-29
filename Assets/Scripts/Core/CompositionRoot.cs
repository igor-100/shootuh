using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CompositionRoot : MonoBehaviour
{
    //private static IUIRoot UIRoot;
    private static IWarrior Warrior;
    private static GameObject WarriorGameObject;
    private static GameObject EnemyGameObject;
    private static IEnemySpawner EnemySpawner;
    //private static IUserInput UserInput;
    private static IGameCamera GameCamera;
    //private static IViewFactory ViewFactory;
    //private static ISceneLoader SceneLoader;
    //private static EventSystem EventSystem;
    private static IResourceManager ResourceManager;

    //private static IGameHUD GameHUD;
    //private static IGameOverScreen GameOverScreen;
    //private static IMainMenuScreen MainMenuScreen;
    //private static IPauseScreen PauseScreen;

    private void OnDestroy()
    {
        //UIRoot = null;
        Warrior = null;
        EnemySpawner = null;
        //UserInput = null;
        GameCamera = null;
        //ViewFactory = null;
        //EventSystem = null;

        //GameHUD = null;
        //GameOverScreen = null;
        //MainMenuScreen = null;
        //PauseScreen = null;

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

    public static IEnemySpawner GetEnemySpawner()
    {
        if (EnemySpawner == null)
        {
            var resourceManager = GetResourceManager();
            EnemySpawner = resourceManager.CreatePrefabInstance<IEnemySpawner, EComponents>(EComponents.Enemy_Spawner);
        }

        return EnemySpawner;
    }

    //public static ISceneLoader GetSceneLoader()
    //{
    //    if (SceneLoader == null)
    //    {
    //        SceneLoader = new SceneLoader();
    //    }

    //    return SceneLoader;
    //}
    
    //public static IUIRoot GetResourceManager()
    //{
    //    if (UIRoot == null)
    //    {
    //        UIRoot = new UIRoot();
    //    }

    //    return UIRoot;
    //}
}
