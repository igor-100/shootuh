using UnityEngine;

public interface IWarrior : IAlive
{
    Transform Transform { get; }

    void Hit(float damage);
}
