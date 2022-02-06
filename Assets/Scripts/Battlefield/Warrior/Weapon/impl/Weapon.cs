using Assets.Scripts.Core.Audio;
using System;
using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public event Action<int> CurrentAmmoChanged;

    private IPlayerInput PlayerInput;
    private IResourceManager ResourceManager;
    private IAudioManager AudioManager;

    private WeaponProperties weaponProperties;

    private float nextTimeToFire = 0f;
    private int currentAmmo;
    private bool isReloading;

    public Color Color { get => weaponProperties.Color; }
    public int CurrentAmmo { get => currentAmmo; }
    public string ModeName { get => weaponProperties.ModeName; }

    protected abstract WeaponProperties InitProperties();

    private void Awake()
    {
        ResourceManager = CompositionRoot.GetResourceManager();
        PlayerInput = CompositionRoot.GetPlayerInput();
        AudioManager = CompositionRoot.GetAudioManager();

        weaponProperties = InitProperties();

        currentAmmo = weaponProperties.MaxAmmo;
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
            nextTimeToFire = Time.time + 1f / weaponProperties.FireRate;
            ShootProjectile();
            CurrentAmmoChanged(--currentAmmo);
        }
    }

    private void ShootProjectile()
    {
        AudioManager.PlayEffect(EAudio.Laser_Shot);
        InstantiateProjectile();
    }

    private void InstantiateProjectile()
    {
        var projectileObj = ResourceManager.GetPooledObject<IProjectile, EComponents>(weaponProperties.ProjectileType);
        projectileObj.transform.position = transform.parent.position;
        projectileObj.GetComponent<Rigidbody>().velocity = transform.parent.forward * weaponProperties.ProjectileSpeed;
    }

    private void OnReload()
    {
        StartCoroutine(WaitForReloading());
    }

    private IEnumerator WaitForReloading()
    {
        isReloading = true;
        AudioManager.PlayEffect(EAudio.Laser_Reload);
        yield return new WaitForSeconds(weaponProperties.ReloadTime);
        CurrentAmmoChanged(currentAmmo = weaponProperties.MaxAmmo);
        isReloading = false;
    }
}
