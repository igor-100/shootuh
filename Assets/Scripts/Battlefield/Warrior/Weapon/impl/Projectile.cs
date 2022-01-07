using System;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float damage = 20f;

    public float Damage { get => damage; }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
