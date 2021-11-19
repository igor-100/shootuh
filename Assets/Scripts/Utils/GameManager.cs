using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOverScreen gameOverScreen;

    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void GameOver()
    {
        gameOverScreen.Setup(0);
    }

    public void Restart()
    {
        sceneLoader.RestartScene();
    }

    public void Quit()
    {
        sceneLoader.LoadScene("StartScene");
    }
}
