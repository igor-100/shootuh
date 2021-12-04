using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour, IMainMenuScreen
{
    private IMainMenuScreenView View;
    private ISceneLoader SceneLoader;

    private void Awake()
    {
        SceneLoader = CompositionRoot.GetSceneLoader();
        var viewFactory = CompositionRoot.GetViewFactory();

        View = viewFactory.CreateMainMenuScreen();

        View.StartClicked += OnStartClicked;
        View.QuitClicked += OnQuitClicked;
    }

    private void OnQuitClicked()
    {
        SceneLoader.Quit();
    }

    private void OnStartClicked()
    {
        SceneLoader.LoadNextScene();
    }

    public void Hide()
    {
        View.Hide();
    }

    public void Show()
    {
        View.Show();
    }
}
