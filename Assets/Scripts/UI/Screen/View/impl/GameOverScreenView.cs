using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverScreenView : BaseView, IGameOverScreenView
{
    public event Action RestartClicked;
    public event Action QuitClicked;

    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        restartButton.onClick.AddListener(OnRestartClicked);
        quitButton.onClick.AddListener(OnQuitClcked);
    }

    public void OnRestartClicked()
    {
        RestartClicked();
    }

    public void OnQuitClcked()
    {
        QuitClicked();
    }
}
