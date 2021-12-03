using System;
using UnityEngine;

public interface IWarrior : IAlive
{
    Transform Transform { get; }
    event Action Died;
    void Hit(float damage);
}
