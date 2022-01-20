using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuScreenView : BaseView, IMainMenuScreenView
{
    public event Action StartClicked = () => { };
    public event Action LoadClicked = () => { };
    public event Action QuitClicked = () => { };

    [SerializeField] private Button startButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        startButton.onClick.AddListener(OnStartClicked);
        loadButton.onClick.AddListener(OnLoadClicked);
        quitButton.onClick.AddListener(OnQuitClcked);
    }

    public void OnStartClicked()
    {
        StartClicked();
    }

    public void OnLoadClicked()
    {
        LoadClicked();
    }

    public void OnQuitClcked()
    {
        QuitClicked();
    }
}
