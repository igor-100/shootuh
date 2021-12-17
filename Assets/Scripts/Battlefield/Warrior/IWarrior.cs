using System;
using UnityEngine;

public interface IWarrior : IAlive, IMovable
{
    IWeaponHolder WeaponHolder { get; }

    event Action StartedDying;

    void SetCamera(Camera cam);
}
