using System;
using UnityEngine;

public interface IWarrior : IAlive
{
    Transform Transform { get; }
    WeaponHolder WeaponHolder { get; }
    event Action Died;
    void Hit(float damage);
}
