using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorShoot : MonoBehaviour
{
    private const string FireButton = "Fire1";
    private const string WeaponHolder = "WeaponHolder";

    private Vector3 destination;
    private WeaponHolder weaponHolder;
    private Weapon selectedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        weaponHolder = transform.Find(WeaponHolder).GetComponent<WeaponHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        selectedWeapon = weaponHolder.SelectedWeapon;

        if (Input.GetButtonDown(FireButton))
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
        var projectileObj = Instantiate(selectedWeapon.PfProjectile, weaponHolder.transform.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = transform.forward * selectedWeapon.ProjectileSpeed;
    }
}
