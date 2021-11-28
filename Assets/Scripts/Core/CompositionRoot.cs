using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class CompositionRoot : MonoBehaviour
{
    //private static IUIRoot UIRoot;
    private static IWarrior Warrior;
    private static GameObject WarriorGameObject;
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

    static CompositionRoot()
    {
        ResourceManager = GetResourceManager();
    }

    private void OnDestroy()
    {
        //UIRoot = null;
        Warrior = null;
        //UserInput = null;
        GameCamera = null;
        //ViewFactory = null;
        //EventSystem = null;

        //GameHUD = null;
        //GameOverScreen = null;
        //MainMenuScreen = null;
        //PauseScreen = null;
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
            GameCamera = ResourceManager.CreatePrefabInstance<GameCamera, EComponents>(EComponents.Main_Camera);
        }

        return GameCamera;
    }

    public static IWarrior GetWarrior()
    {
        if (Warrior == null)
        {
            Warrior = ResourceManager.CreatePrefabInstance<Warrior, EComponents>(EComponents.Warrior);
        }

        return Warrior;
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
