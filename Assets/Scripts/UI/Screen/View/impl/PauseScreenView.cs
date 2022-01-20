using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PauseScreenView : BaseView, IPauseScreenView
{
    public event Action ResumeClicked;
    public event Action RestartClicked;
    public event Action SaveClicked;
    public event Action QuitClicked;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(OnResumeClicked);
        restartButton.onClick.AddListener(OnRestartClicked);
        saveButton.onClick.AddListener(OnSaveClicked);
        quitButton.onClick.AddListener(OnQuitClcked);
    }

    public void OnResumeClicked()
    {
        ResumeClicked();
    }

    public void OnRestartClicked()
    {
        RestartClicked();
    }

    public void OnSaveClicked()
    {
        SaveClicked();
    }

    public void OnQuitClcked()
    {
        QuitClicked();
    }
}
