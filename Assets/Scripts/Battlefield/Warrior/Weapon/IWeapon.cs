using System;
using UnityEngine;

public interface IWeapon
{
    event Action<int> CurrentAmmoChanged;
    int CurrentAmmo { get; }
    string ModeName { get; }
    Color Color { get; }
}
