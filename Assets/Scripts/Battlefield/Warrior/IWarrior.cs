using System;
using UnityEngine;

public interface IWarrior : IAlive, IMovable, ISaveable
{
    CharacterStat HealthStat { get; }
    CharacterStat MoveSpeedStat { get; }
    IWeaponHolder WeaponHolder { get; }

    event Action StartedDying;

    void Init(string jsonProperties);
    void Rotate(Vector3 rotationPoint);
    void Heal(float healValue);
}
