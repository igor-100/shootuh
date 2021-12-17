public class AttackingState : FightingState
{
    private const string attackTrigger = "attack";

    public AttackingState(IEnemy character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.TriggerAnimation(attackTrigger);
        enemy.WaitForNextAttack();
        enemy.AttackCompleted += OnAttackCompleted;
    }

    private void OnAttackCompleted()
    {
        enemy.StateMachine.ChangeState(enemy.WalkingState);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.AttackCompleted -= OnAttackCompleted;
    }
}
