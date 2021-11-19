using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreen : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        var sceneLoader = FindObjectOfType<SceneLoader>();
        startButton.onClick.AddListener(sceneLoader.LoadNextScene);
        quitButton.onClick.AddListener(sceneLoader.Quit);
    }
}
