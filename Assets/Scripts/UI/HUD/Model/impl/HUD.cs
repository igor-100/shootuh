using UnityEngine;

public class HUD : MonoBehaviour, IHUD
{
    private const string TrailComponent = "Trail";

    private Weapon currentWeapon;
    private WeaponHolder weaponHolder;
    private int currentAmmo;
    private Color currentColor;

    private IHUDView View;

    private void Awake()
    {
        var warrior = CompositionRoot.GetWarrior();
        weaponHolder = warrior.WeaponHolder;
        currentWeapon = weaponHolder.CurrentWeapon;

        var viewFactory = CompositionRoot.GetViewFactory();
        View = viewFactory.CreateHUD();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: On change event
        if (currentWeapon != weaponHolder.CurrentWeapon)
        {
            currentWeapon = weaponHolder.CurrentWeapon;
            UpdateColor();
            UpdateWeaponText();
        }
        // TODO: On change event
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
        View.SetAmmoText(currentAmmoText);
    }

    private void UpdateColor()
    {
        // Add configuration for projectile
        currentColor = currentWeapon.PfProjectile.transform.Find(TrailComponent).GetComponent<TrailRenderer>().startColor;
        View.SetAmmoAndWeaponTextColor(currentColor);
    }

    private void UpdateWeaponText()
    {
        View.SetWeaponText(currentWeapon.ModeName);
    }

    public void Hide()
    {
        View.Hide();
    }

    public void Show()
    {
        View.Show();
    }
}
