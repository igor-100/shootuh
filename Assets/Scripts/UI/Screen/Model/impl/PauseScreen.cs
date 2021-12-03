using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour, IPauseScreen
{
    private static bool gameIsPaused = false;

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

    private void Pause()
    {
        backgroundObject.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    private void Resume()
    {
        backgroundObject.SetActive(false);
        ToNormalSpeed();
        gameIsPaused = false;
    }


    private void Restart()
    {
        ToNormalSpeed();
        gameManager.Restart();
    }

    private void Quit()
    {
        ToNormalSpeed();
        gameManager.Quit();
    }

    private static void ToNormalSpeed()
    {
        Time.timeScale = 1f;
    }
}
