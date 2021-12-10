using UnityEngine;

public interface IWarrior : IAlive, IMovable
{
    IWeaponHolder WeaponHolder { get; }
    void SetCamera(Camera cam);
}
