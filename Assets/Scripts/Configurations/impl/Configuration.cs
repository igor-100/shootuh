using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Configuration : IConfiguration
{
    private readonly WeaponProperties standardWeaponProperties;
    private readonly WeaponProperties rifleWeaponProperties;
    private readonly WeaponProperties rapidWeaponProperties;

    private readonly WarriorProperties warriorProperties;

    private readonly EnemyProperties enemyHMProperties;
    private readonly EnemyProperties enemyCMProperties;

    private readonly EnemySpawnerProperties enemySpawnerProperties;

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

        enemySpawnerProperties = new EnemySpawnerProperties()
        {
            Waves = new List<Wave>()
            {
            new Wave()
            {
                Id = 1,
                DelayAfterWave = 5f,
                MinSpawnDelay = 1f,
                MaxSpawnDelay = 3f,
                IsParallelSpawning = false,
                SpawnPoints = new List<SpawnPoint>()
                {

                    new SpawnPoint()
                    {
                        PointPosition = new Vector3(60f, 0, 60f),
                        EnemiesByNumber = new Dictionary<EComponents, int>()
                        {
                            {EComponents.Enemy_HM, 5}
                        }
                    },
                    new SpawnPoint()
                    {
                        PointPosition = new Vector3(-60f, 0, 60f),
                        EnemiesByNumber = new Dictionary<EComponents, int>()
                        {
                            {EComponents.Enemy_HM, 5}
                        }
                    },
                }
            },
            new Wave()
            {
                Id = 2,
                DelayAfterWave = 5f,
                MinSpawnDelay = 0.5f,
                MaxSpawnDelay = 2f,
                IsParallelSpawning = true,
                SpawnPoints = new List<SpawnPoint>()
                {
                    new SpawnPoint()
                    {
                        PointPosition = new Vector3(-60f, 0, -60f),
                        EnemiesByNumber = new Dictionary<EComponents, int>()
                        {
                            {EComponents.Enemy_CM, 10}
                        }
                    },
                    new SpawnPoint()
                    {
                        PointPosition = new Vector3(60f, 0, 60f),
                        EnemiesByNumber = new Dictionary<EComponents, int>()
                        {
                            {EComponents.Enemy_CM, 10}
                        }
                    },
                }
            },
            new Wave()
            {
                Id = 3,
                DelayAfterWave = 10f,
                MinSpawnDelay = 0.5f,
                MaxSpawnDelay = 2f,
                IsParallelSpawning = false,
                SpawnPoints = new List<SpawnPoint>()
                {
                    new SpawnPoint()
                    {
                        PointPosition = new Vector3(60f, 0, -60f),
                        EnemiesByNumber = new Dictionary<EComponents, int>()
                        {
                            {EComponents.Enemy_CM, 20},
                            {EComponents.Enemy_HM, 20},
                        }
                    },
                    new SpawnPoint()
                    {
                        PointPosition = new Vector3(-60f, 0, 60f),
                        EnemiesByNumber = new Dictionary<EComponents, int>()
                        {
                            {EComponents.Enemy_CM, 20},
                            {EComponents.Enemy_HM, 20},
                        }
                    },
                }
            },
            new Wave()
            {
                Id = 4,
                DelayAfterWave = 5f,
                MinSpawnDelay = 0.2f,
                MaxSpawnDelay = 0.8f,
                IsParallelSpawning = true,
                SpawnPoints = new List<SpawnPoint>()
                {
                    new SpawnPoint()
                    {
                        PointPosition = new Vector3(60f, 0, -60f),
                        EnemiesByNumber = new Dictionary<EComponents, int>()
                        {
                            {EComponents.Enemy_CM, 50},
                            {EComponents.Enemy_HM, 50},
                        }
                    },
                    new SpawnPoint()
                    {
                        PointPosition = new Vector3(-60f, 0, 60f),
                        EnemiesByNumber = new Dictionary<EComponents, int>()
                        {
                            {EComponents.Enemy_CM, 50},
                            {EComponents.Enemy_HM, 50},
                        }
                    },
                }
            }
            }
        };
    }

    public WeaponProperties GetStandardWeaponProperties() => standardWeaponProperties;
    public WeaponProperties GetRifleWeaponProperties() => rifleWeaponProperties;
    public WeaponProperties GetRapidWeaponProperties() => rapidWeaponProperties;

    public WarriorProperties GetWarriorProperties() => warriorProperties;

    public EnemyProperties GetEnemyHMProperties() => enemyHMProperties;
    public EnemyProperties GetEnemyCMProperties() => enemyCMProperties;

    public EnemySpawnerProperties GetEnemySpawnerProperties() => enemySpawnerProperties;
}