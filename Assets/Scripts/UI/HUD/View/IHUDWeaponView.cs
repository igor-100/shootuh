using UnityEngine;

public interface IHUDWeaponView : IView
{
    void SetAmmoText(string ammoText);
    void SetWeaponText(string weaponText);
    void SetAmmoAndWeaponTextColor(Color color);
}
