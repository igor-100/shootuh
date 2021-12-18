using System;
using UnityEngine;

public interface IWarrior : IAlive, IMovable
{
    IWeaponHolder WeaponHolder { get; }

    event Action StartedDying;

    void Rotate(Vector3 rotationPoint);
}
