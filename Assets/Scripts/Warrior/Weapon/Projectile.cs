using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 20;

    public int Damage { get => damage; }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
