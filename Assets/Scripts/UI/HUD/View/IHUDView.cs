using UnityEngine;

public interface IHUDView : IView
{
    void SetAmmoText(string ammoText);
    void SetWeaponText(string weaponText);
    void SetAmmoAndWeaponTextColor(Color color);
}
