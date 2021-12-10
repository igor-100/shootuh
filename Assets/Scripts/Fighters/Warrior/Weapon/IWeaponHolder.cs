using System;

public interface IWeaponHolder
{
    public event Action<IWeapon> SelectedWeaponChanged;
    public IWeapon CurrentWeapon { get; }
}
