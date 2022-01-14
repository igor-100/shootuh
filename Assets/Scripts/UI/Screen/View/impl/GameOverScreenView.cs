using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverScreenView : BaseView, IGameOverScreenView
{
    private ILevelSystem LevelSystem;

    public event Action RestartClicked;
    public event Action QuitClicked;

    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        LevelSystem = CompositionRoot.GetLevelSystem();

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

    public override void Show()
    {
        gameObject.SetActive(true);
        pointsText.text = "Level: " + LevelSystem.GetLevel();
    }
}
