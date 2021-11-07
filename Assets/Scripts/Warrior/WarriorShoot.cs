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
        // Bit shift the index of the layer (6) to get a bit mask
        int layerMask = 1 << 6;

        // This would cast rays only against colliders in layer 6.
        // But instead we want to collide against everything except layer 6. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        RaycastHit objectHit;
        if (Physics.Raycast(firePoint.position, transform.forward, out objectHit, 50f, layerMask))
        {
            Debug.DrawRay(firePoint.position, transform.TransformDirection(Vector3.forward) * objectHit.distance, Color.yellow);
            Debug.Log("Raycast hitted to: " + objectHit.collider);
            destination = objectHit.point;
        }
        else
        {
            Debug.DrawRay(firePoint.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Raycast doesn't hit");
            destination = transform.forward * 50;
        }
        InstantiateProjectile();
    }

    private void InstantiateProjectile()
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
    }
}
