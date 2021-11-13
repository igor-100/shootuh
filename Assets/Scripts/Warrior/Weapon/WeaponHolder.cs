using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private int selectedWeaponId = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeaponId)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
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
