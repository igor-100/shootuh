﻿using UnityEngine;

public class Configuration : IConfiguration
{
    private readonly WeaponProperties standardWeaponProperties;
    private readonly WeaponProperties rifleWeaponProperties;
    private readonly WeaponProperties rapidWeaponProperties;

    public Configuration()
    {
        standardWeaponProperties = new WeaponProperties()
        {
            MaxAmmo = 30,
            ProjectileSpeed = 30f,
            FireRate = 5f,
            ReloadTime = 1f,
            ProjectileType = EComponents.Projectile_Blue,
            ModeName = "Standard",
            Color = new Color32(4, 90, 255, 255)
        };

        rifleWeaponProperties = new WeaponProperties()
        {
            MaxAmmo = 20,
            ProjectileSpeed = 25f,
            FireRate = 3f,
            ReloadTime = 1f,
            ProjectileType = EComponents.Projectile_Red,
            ModeName = "Rifle",
            Color = new Color32(255, 9, 0, 255)
        };

        rapidWeaponProperties = new WeaponProperties()
        {
            MaxAmmo = 40,
            ProjectileSpeed = 35f,
            FireRate = 7f,
            ReloadTime = 1f,
            ProjectileType = EComponents.Projectile_Purple,
            ModeName = "Rapid",
            Color = new Color32(211, 31, 122, 255)
        };
    }

    public WeaponProperties GetStandardWeaponProperties()
    {
        return standardWeaponProperties;
    }

    public WeaponProperties GetRifleWeaponProperties()
    {
        return rifleWeaponProperties;
    }

    public WeaponProperties GetRapidWeaponProperties()
    {
        return rapidWeaponProperties;
    }
}