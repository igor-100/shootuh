using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage = 20f;

    public float Damage { get => damage; }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
