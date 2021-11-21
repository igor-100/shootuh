using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterStats;

public class Warrior : MonoBehaviour, IAlive
{
    private const float GameOverDelay = 2f;

    public CharacterStat Health;

    private float currentHealth;
    private GameManager gameManager;

    public float GetHealthPercent()
    {
        return (float)currentHealth / Health.BaseValue;
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            Invoke("EndGame", GameOverDelay);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentHealth = Health.BaseValue;
    }

    // Update is called once per frame
    private void Update()
    {

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
