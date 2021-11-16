using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour, IAlive
{
    [SerializeField] private int health = 100;

    private int currentHealth;

    public int CurrentHealth { get => currentHealth; }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetHealthPercent()
    {
        return (float)currentHealth / health;
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        Die();
    }

    private void Die()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);
        }
    }
}
