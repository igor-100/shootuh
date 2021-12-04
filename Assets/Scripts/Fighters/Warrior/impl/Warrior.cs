using System;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour, IWarrior
{
    private const float GameOverDelay = 2f;

    public event Action Died = () => { };

    [SerializeField] private CharacterStat health;
    [SerializeField] private WeaponHolder weaponHolder;

    private float currentHealth;

    public Transform Transform => transform;
    public WeaponHolder WeaponHolder => weaponHolder;
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
