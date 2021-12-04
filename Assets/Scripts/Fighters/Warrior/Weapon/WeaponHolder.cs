using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private int selectedWeaponId = 0;

    private Weapon currentWeapon;
    private int currentAmmo;

    public Weapon CurrentWeapon { get => currentWeapon; }
    public int CurrentAmmo { get => currentAmmo; }

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weaponTransform in transform)
        {
            if (i == selectedWeaponId)
            {
                weaponTransform.gameObject.SetActive(true);
                currentWeapon = weaponTransform.GetComponent<Weapon>();
            }
            else
            {
                weaponTransform.gameObject.SetActive(false);
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeaponId = selectedWeaponId;

        MouseWheelInput();
        KeyAlphaInput();

        if (previousSelectedWeaponId != selectedWeaponId)
        {
            SelectWeapon();
        }
        currentAmmo = currentWeapon.CurrentAmmo;
    }

    private void MouseWheelInput()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeaponId >= transform.childCount - 1)
            {
                selectedWeaponId = 0;
            }
            else
            {
                selectedWeaponId++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeaponId <= 0)
            {
                selectedWeaponId = transform.childCount - 1;
            }
            else
            {
                selectedWeaponId--;
            }
        }
    }


    private void KeyAlphaInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeaponId = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeaponId = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeaponId = 2;
        }
    }
}
