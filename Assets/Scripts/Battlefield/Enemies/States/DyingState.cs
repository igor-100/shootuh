public class DyingState : State
{
    public DyingState(IEnemy enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.StartDying();
    }
}
