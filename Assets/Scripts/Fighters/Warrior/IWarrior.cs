using System;
using UnityEngine;

public interface IWarrior : IAlive, IMovable, IAttack
{
    WeaponHolder WeaponHolder { get; }
    void Hit(float damage);
}
