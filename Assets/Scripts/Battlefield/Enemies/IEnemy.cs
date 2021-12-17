using System;
using UnityEngine;

public interface IEnemy : IAlive, IMovable, IStateable, IAnimatable, IDamageable
{
    Transform TargetTransform { get; set; }
    float AttackRange { get; }
    GameObject EnemyGameObject { get; }

    FightingState FightingState { get; }
    AttackingState AttackingState { get; }
    WalkingState WalkingState { get; }
    DyingState DyingState { get; }
    IdleState IdleState { get; }

    event Action AttackCompleted;

    void WaitForNextAttack();
    void RotateTowardsTheTarget();
    void Walk();
    void StartDying();
}
