using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int maxAmmo = 20;
    [SerializeField] private float projectileSpeed = 30f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private float reloadTime = 1f;
    [SerializeField] private Projectile pfProjectile;
    [SerializeField] private string modeName;

    public event Action<int> CurrentAmmoChanged;

    private IPlayerInput PlayerInput;

    private float nextTimeToFire = 0f;
    private int currentAmmo;
    private bool isReloading;

    public int CurrentAmmo { get => currentAmmo; }
    public Projectile PfProjectile { get => pfProjectile; }
    public string ModeName { get => modeName; }

    private void Awake()
    {
        PlayerInput = CompositionRoot.GetPlayerInput();

        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        PlayerInput.Fire += OnFire;
        PlayerInput.Reload += OnReload;
    }

    private void OnDisable()
    {
        PlayerInput.Fire -= OnFire;
        PlayerInput.Reload -= OnReload;
    }

    private void OnFire()
    {
        if (!isReloading && currentAmmo != 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            ShootProjectile();
            CurrentAmmoChanged(--currentAmmo);
        }
    }

    private void ShootProjectile()
    {
        InstantiateProjectile();
    }

    private void InstantiateProjectile()
    {
        var projectileObj = Instantiate(pfProjectile, transform.parent.position, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody>().velocity = transform.parent.forward * projectileSpeed;
    }

    private void OnReload()
    {
        StartCoroutine(WaitForReloading());
    }

    private IEnumerator WaitForReloading()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        CurrentAmmoChanged(currentAmmo = maxAmmo);
        isReloading = false;
    }
}
