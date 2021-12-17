using UnityEngine;

public class WalkingState : FightingState
{
    private const string walkTrigger = "walk";


    public WalkingState(IEnemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.TriggerAnimation(walkTrigger);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.RotateTowardsTheTarget();
        enemy.Walk();
        if (Vector3.Distance(enemy.Transform.position, enemy.TargetTransform.position) < enemy.AttackRange - 1.5f)
        {
            enemy.StateMachine.ChangeState(enemy.AttackingState);
        }
    }
}
