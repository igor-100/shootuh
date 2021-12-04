using System;
using TMPro;
using UnityEngine;

public class HUDView : BaseView, IHUDView
{
    [SerializeField] private TextMeshProUGUI ammoTextComponent;
    [SerializeField] private TextMeshProUGUI weaponTextComponent;

    public void SetAmmoAndWeaponTextColor(Color color)
    {
        ammoTextComponent.color = color;
        weaponTextComponent.color = color;
    }

    public void SetAmmoText(string ammoText)
    {
        ammoTextComponent.text = ammoText;
    }

    public void SetWeaponText(string weaponText)
    {
        weaponTextComponent.text = weaponText;
    }
}
