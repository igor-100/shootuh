using System;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour, IWarrior
{
    private const float GameOverDelay = 2f;

    [SerializeField] private CharacterStat health;

    private float currentHealth;
    private GameManager gameManager;

    public event Action Died = () => { };
    public Transform Transform => transform;
    public float HealthPercent => (float)currentHealth / health.BaseValue;

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
        currentHealth = health.BaseValue;
    }

    private void EndGame()
    {
        Died();
    }

    private void Die()
    {
        currentHealth = 0;
        gameObject.SetActive(false);
    }
}
