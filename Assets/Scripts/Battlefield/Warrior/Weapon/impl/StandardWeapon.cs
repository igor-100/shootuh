public class StandardWeapon : Weapon
{
    protected override WeaponProperties InitProperties()
    {
        return CompositionRoot.GetConfiguration().GetStandardWeaponProperties();
    }
}
