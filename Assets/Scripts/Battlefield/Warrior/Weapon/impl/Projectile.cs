using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float damage = 20f;
    [SerializeField] private GameObject trail;

    public float Damage { get => damage; }

    private void OnEnable()
    {
        StartCoroutine(WaitingForTrailToEnable());
    }

    private IEnumerator WaitingForTrailToEnable()
    {
        yield return new WaitForSeconds(0.01f);
        trail.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        trail.SetActive(false);
        gameObject.SetActive(false);
    }
}
