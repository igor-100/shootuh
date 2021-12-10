public class RapidWeapon : Weapon
{
    protected override WeaponProperties InitProperties()
    {
        return CompositionRoot.GetConfiguration().GetRapidWeaponProperties();
    }
}
