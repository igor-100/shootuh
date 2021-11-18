using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " Points";
    }

    private void Start()
    {
        var gameManager = FindObjectOfType<GameManager>();
        restartButton.onClick.AddListener(gameManager.Restart);
        quitButton.onClick.AddListener(gameManager.Quit);
    }
}
