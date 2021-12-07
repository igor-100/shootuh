using System;
using UnityEngine;

public class Warrior : MonoBehaviour, IWarrior
{
    private const float GameOverDelay = 2f;

    public event Action Died = () => { };
    public event Action<float> HealthPercentChanged;

    [SerializeField] private CharacterStat health;
    [SerializeField] private WeaponHolder weaponHolder;

    private float currentHealth;

    public Transform Transform => transform;
    public WeaponHolder WeaponHolder => weaponHolder;

    private void Start()
    {
        currentHealth = health.BaseValue;
    }

    public void Hit(float damage)
    {
        currentHealth -= damage;
        HealthPercentChanged((float)currentHealth / health.BaseValue);
        if (currentHealth <= 0)
        {
            Die();
            Invoke("EndGame", GameOverDelay);
        }
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
