public class RifleWeapon : Weapon
{
    protected override WeaponProperties InitProperties()
    {
        return CompositionRoot.GetConfiguration().GetRifleWeaponProperties();
    }
}
