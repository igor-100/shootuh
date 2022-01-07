public interface IConfiguration
{
    WeaponProperties GetStandardWeaponProperties();
    WeaponProperties GetRifleWeaponProperties();
    WeaponProperties GetRapidWeaponProperties();
    WarriorProperties GetWarriorProperties();
    EnemyProperties GetEnemyHMProperties();
    EnemyProperties GetEnemyCMProperties();
}
