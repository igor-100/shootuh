using UnityEngine;

public class GameOverScreen : MonoBehaviour, IGameOverScreen
{
    private IGameOverScreenView View;
    private ISceneLoader SceneLoader;

    private void Awake()
    {
        SceneLoader = CompositionRoot.GetSceneLoader();
        var viewFactory = CompositionRoot.GetViewFactory();

        View = viewFactory.CreateGameOverScreen();

        View.RestartClicked += OnRestartClicked;
        View.QuitClicked += OnQuitClicked;
    }

    private void OnQuitClicked()
    {
        SceneLoader.LoadScene(EScenes.StartScene);
    }

    private void OnRestartClicked()
    {
        SceneLoader.RestartScene();
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
