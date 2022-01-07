using UnityEngine;

public class Configuration : IConfiguration
{
    private readonly WeaponProperties standardWeaponProperties;
    private readonly WeaponProperties rifleWeaponProperties;
    private readonly WeaponProperties rapidWeaponProperties;

    private readonly WarriorProperties warriorProperties;

    private readonly EnemyProperties enemyHMProperties;
    private readonly EnemyProperties enemyCMProperties;

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
            ProjectileType = EComponents.Projectile_Green,
            ModeName = "Rifle",
            Color = new Color32(0, 231, 28, 255)
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

        warriorProperties = new WarriorProperties()
        {
            HealthStat = new CharacterStat(100f),
            MoveSpeedStat = new CharacterStat(5f)
        };

        enemyHMProperties = new EnemyProperties()
        {
            HealthStat = new CharacterStat(100f),
            DamageStat = new CharacterStat(20f),
            MoveSpeedStat = new CharacterStat(5f),

            HitHeight = 0.5f,
            DeathTime = 2f,
            AttackTime = 1f,
            AttackRange = 5f
        };

        enemyCMProperties = new EnemyProperties()
        {
            HealthStat = new CharacterStat(50f),
            DamageStat = new CharacterStat(20f),
            MoveSpeedStat = new CharacterStat(4f),

            HitHeight = 0.5f,
            DeathTime = 3f,
            AttackTime = 2f,
            AttackRange = 12f
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

    public WarriorProperties GetWarriorProperties()
    {
        return warriorProperties;
    }

    public EnemyProperties GetEnemyHMProperties()
    {
        return enemyHMProperties;
    }

    public EnemyProperties GetEnemyCMProperties()
    {
        return enemyCMProperties;
    }
}
