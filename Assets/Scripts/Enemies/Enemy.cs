using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int damage = 20;

    private int currentHealth;

    public int Damage { get => damage; }

    // Start is called before the first frame update
    void Start()
    {
        initParameters();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        var projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile)
        {
            currentHealth -= projectile.Damage;
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                initParameters();
            }
        }
    }

    private void initParameters()
    {
        currentHealth = health;
    }
}
