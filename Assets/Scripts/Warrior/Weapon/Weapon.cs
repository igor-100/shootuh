using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private const string FireButton = "Fire1";

    [SerializeField] private int maxAmmo = 20;
    [SerializeField] private float projectileSpeed = 30f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private GameObject pfProjectile;

    private float nextTimeToFire = 0f;
    private int currentAmmo;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        Fire();
        Reload();
    }

    private void Fire()
    {
        if (Input.GetButton(FireButton) && currentAmmo != 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            ShootProjectile();
            currentAmmo--;
        }
    }

    private void ShootProjectile()
    {
        InstantiateProjectile();
    }

    private void InstantiateProjectile()
    {
        var projectileObj = Instantiate(pfProjectile, transform.parent.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = transform.parent.forward * projectileSpeed;
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(WaitForReloading());
            
        }
    }

    private IEnumerator WaitForReloading()
    {
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
    }
}
