using UnityEngine;

public class PauseScreen : MonoBehaviour, IPauseScreen
{
    // TODO: Extract to some State Service
    private static bool gameIsPaused = false;

    private IPauseScreenView View;
    private ISceneLoader SceneLoader;
    private ISaveManager SaveManager;

    private void Awake()
    {
        SceneLoader = CompositionRoot.GetSceneLoader();
        SaveManager = CompositionRoot.GetSaveManager();
        var playerInput = CompositionRoot.GetPlayerInput();
        var viewFactory = CompositionRoot.GetViewFactory();

        playerInput.Escape += OnEscape;

        View = viewFactory.CreatePauseScreen();

        View.ResumeClicked += OnResumeClicked;
        View.RestartClicked += OnRestartClicked;
        View.SaveClicked += OnSaveClicked;
        View.QuitClicked += OnQuitClicked;
    }

    private void OnEscape()
    {
        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void OnResumeClicked()
    {
        Resume();
    }

    private void OnRestartClicked()
    {
        ToNormalSpeed();
        SaveManager.New();
        SceneLoader.RestartScene();
    }

    private void OnSaveClicked()
    {
        SaveManager.Save();
    }

    private void OnQuitClicked()
    {
        ToNormalSpeed();
        SceneLoader.LoadScene(EScenes.StartScene);
    }

    public void Hide()
    {
        View.Hide();
    }

    public void Show()
    {
        View.Show();
    }

    private void Pause()
    {
        Show();
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private void Resume()
    {
        Hide();
        ToNormalSpeed();
        gameIsPaused = false;
    }

    private static void ToNormalSpeed()
    {
        Time.timeScale = 1f;
    }
}
