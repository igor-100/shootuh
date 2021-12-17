using System;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private Color color = Color.white;

    public float Damage { get => damage; }
    public Color Color { get => color; }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
