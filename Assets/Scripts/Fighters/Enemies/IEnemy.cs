using UnityEngine;
public interface IEnemy : IAlive
{
    float Damage { get; }
    Transform TargetTransform { get; set; }
    void Attack();
}
