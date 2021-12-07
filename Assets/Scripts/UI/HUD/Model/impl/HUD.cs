using UnityEngine;

public class HUD : MonoBehaviour, IHUD
{
    private const string TrailComponent = "Trail";

    private IWarrior Warrior;

    private IHUDView View;

    private void Awake()
    {
        Warrior = CompositionRoot.GetWarrior();
        var viewFactory = CompositionRoot.GetViewFactory();
        View = viewFactory.CreateHUD();
    }

    private void Start()
    {
        var weaponHolder = Warrior.WeaponHolder;
        weaponHolder.SelectedWeaponChanged += OnSelectedWeaponChange;

        var currentWeapon = weaponHolder.CurrentWeapon;
        currentWeapon.CurrentAmmoChanged += OnAmmoChange;

        UpdateColor(currentWeapon);
        UpdateWeaponText(currentWeapon);
        UpdateAmmoText(currentWeapon.CurrentAmmo);
    }

    private void OnSelectedWeaponChange(Weapon currentWeapon)
    {
        currentWeapon.CurrentAmmoChanged += OnAmmoChange;
        UpdateAmmoText(currentWeapon.CurrentAmmo);
        UpdateColor(currentWeapon);
        UpdateWeaponText(currentWeapon);
    }

    private void OnAmmoChange(int currentAmmo)
    {
        UpdateAmmoText(currentAmmo);
    }

    private void UpdateAmmoText(int currentAmmo)
    {
        string currentAmmoText = "0" + currentAmmo;
        if (currentAmmo < 10)
        {
            currentAmmoText = "0" + currentAmmoText;
        }
        View.SetAmmoText(currentAmmoText);
    }

    private void UpdateColor(Weapon weapon)
    {
        // Add color configuration for projectile
        var color = weapon.PfProjectile.transform.Find(TrailComponent).GetComponent<TrailRenderer>().startColor;
        View.SetAmmoAndWeaponTextColor(color);
    }

    private void UpdateWeaponText(Weapon weapon)
    {
        View.SetWeaponText(weapon.ModeName);
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
