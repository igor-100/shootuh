using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponDisplay : MonoBehaviour
{
    private const string AmmoText = "Ammo Text";
    private const string WeaponText = "Weapon Text";
    private const string TrailComponent = "Trail";

    private TextMeshProUGUI ammoTextComponent;
    private TextMeshProUGUI weaponTextComponent;

    private WeaponHolder weaponHolder;
    private Weapon currentWeapon;
    private int currentAmmo;
    private Color currentColor;

    private void Start()
    {
        // GameManager.hasPlayer.hasWeapon?
        weaponHolder = FindObjectOfType<WeaponHolder>();
        ammoTextComponent = transform.Find(AmmoText).GetComponent<TextMeshProUGUI>();
        weaponTextComponent = transform.Find(WeaponText).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWeapon != weaponHolder.CurrentWeapon)
        {
            currentWeapon = weaponHolder.CurrentWeapon;
            UpdateColor();
            UpdateWeaponText();
        }
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        currentAmmo = currentWeapon.CurrentAmmo;
        string currentAmmoText = "0" + currentAmmo;
        if (currentAmmo < 10)
        {
            currentAmmoText = "0" + currentAmmoText;
        }
        ammoTextComponent.text = currentAmmoText;
    }

    private void UpdateColor()
    {
        currentColor = currentWeapon.PfProjectile.transform.Find(TrailComponent).GetComponent<TrailRenderer>().startColor;
        ammoTextComponent.color = currentColor;
        weaponTextComponent.color = currentColor;
    }

    private void UpdateWeaponText()
    {
        weaponTextComponent.text = currentWeapon.ModeName;
    }
}
