using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour, IAlive
{
    private const float GameOverDelay = 2f;

    [SerializeField] private int health = 100;

    private int currentHealth;
    private GameManager gameManager;

    public float GetHealthPercent()
    {
        return (float)currentHealth / health;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            Invoke("EndGame", GameOverDelay);
        }
    }

    private void EndGame()
    {
        gameManager.GameOver();
    }

    private void Die()
    {
            currentHealth = 0;
            gameObject.SetActive(false);
    }
}
