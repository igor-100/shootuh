using UnityEngine;

public interface IWarrior : IAlive, IMovable
{
    IWeaponHolder WeaponHolder { get; }
    void SetCamera(Camera cam);
    void Move(Vector2 movement);
    void Rotate(Vector3 mousePos);
}
