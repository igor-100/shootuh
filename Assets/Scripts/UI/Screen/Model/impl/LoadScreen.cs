using UnityEngine;
using System.Collections.Generic;

public class LoadScreen : MonoBehaviour, ILoadScreen
{
    private ILoadScreenView View;
    private ISceneLoader SceneLoader;
    private List<SaveFile> SaveFiles;

    private void Awake()
    {
        SceneLoader = CompositionRoot.GetSceneLoader();
        var viewFactory = CompositionRoot.GetViewFactory();

        View = viewFactory.CreateLoadScreen();

        SaveFiles = SaveSystem.GetAllSaveFiles();
        View.DisplayLoadSlots(SaveFiles);

        View.BackClicked += OnBackClicked;
    }

    private void OnBackClicked()
    {
        View.Hide();
        var mainMenuScreen = CompositionRoot.GetMainMenuScreen();
        mainMenuScreen.Show();
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
