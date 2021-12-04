using UnityEngine;

public class PauseScreen : MonoBehaviour, IPauseScreen
{
    // TODO: Extract to some State Service
    private static bool gameIsPaused = false;

    private IPauseScreenView View;
    private ISceneLoader SceneLoader;

    private void Awake()
    {
        SceneLoader = CompositionRoot.GetSceneLoader();
        var viewFactory = CompositionRoot.GetViewFactory();

        View = viewFactory.CreatePauseScreen();

        View.ResumeClicked += OnResumeClicked;
        View.RestartClicked += OnRestartClicked;
        View.QuitClicked += OnQuitClicked;
    }

    private void Update()
    {
        // TODO: InputManager
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }

    private void OnQuitClicked()
    {
        ToNormalSpeed();
        SceneLoader.LoadScene(EScenes.StartScene);
    }

    private void OnResumeClicked()
    {
        Resume();
    }

    private void OnRestartClicked()
    {
        ToNormalSpeed();
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
