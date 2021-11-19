using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private GameObject backgroundObject;
    private GameManager gameManager;

    private void Start()
    {
        backgroundObject = transform.Find("Background").gameObject;
        gameManager = FindObjectOfType<GameManager>();
        resumeButton.onClick.AddListener(Resume);
        restartButton.onClick.AddListener(Restart);
        quitButton.onClick.AddListener(Quit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        backgroundObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    private void Resume()
    {
        backgroundObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Restart()
    {
        Time.timeScale = 1f;
        gameManager.Restart();
    }

    private void Quit()
    {
        Time.timeScale = 1f;
        gameManager.Quit();
    }
}
