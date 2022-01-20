using UnityEngine;

public class MainMenuScreen : MonoBehaviour, IMainMenuScreen
{
    private IMainMenuScreenView View;
    private ISceneLoader SceneLoader;
    private ISaveManager SaveManager;

    private void Awake()
    {
        SceneLoader = CompositionRoot.GetSceneLoader();
        SaveManager = CompositionRoot.GetSaveManager();
        var viewFactory = CompositionRoot.GetViewFactory();

        View = viewFactory.CreateMainMenuScreen();

        View.StartClicked += OnStartClicked;
        View.LoadClicked += OnLoadClicked;
        View.QuitClicked += OnQuitClicked;
    }

    private void OnStartClicked()
    {
        SceneLoader.LoadNextScene();
    }

    private void OnQuitClicked()
    {
        SceneLoader.Quit();
    }

    private void OnLoadClicked()
    {
        SaveManager.Load();
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
