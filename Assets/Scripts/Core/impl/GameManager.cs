using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ISceneLoader SceneLoader;

    private void Awake()
    {
        SceneLoader = CompositionRoot.GetSceneLoader();
    }

    public void Restart()
    {
        SceneLoader.RestartScene();
    }

    public void Quit()
    {
        SceneLoader.LoadScene(EScenes.StartScene);
    }
}
