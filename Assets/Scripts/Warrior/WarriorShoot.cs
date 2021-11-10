using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private float projectileSpeed = 30f;

    private Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootProjectile();
        }

    }

    private void ShootProjectile()
    {
        InstantiateProjectile();
    }

    private void InstantiateProjectile()
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity);
        Debug.Log(firePoint.position);
        Debug.Log(transform.position);
        projectileObj.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
    }
}
