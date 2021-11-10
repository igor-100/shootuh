using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int clipSize = 20;
    [SerializeField] private GameObject pfProjectile;
    [SerializeField] private float projectileSpeed = 30f;

    public int ClipSize { get => clipSize; }
    public GameObject PfProjectile { get => pfProjectile; }
    public float ProjectileSpeed { get => projectileSpeed; }
}
