public class DyingState : State
{
    public DyingState(IEnemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.StartDying();
    }
}
