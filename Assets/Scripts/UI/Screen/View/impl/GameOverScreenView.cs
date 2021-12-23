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
        var levelSystem = CompositionRoot.GetLevelSystem();

        restartButton.onClick.AddListener(OnRestartClicked);
        quitButton.onClick.AddListener(OnQuitClcked);
        pointsText.text = "Level: " + levelSystem.GetLevel();
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
