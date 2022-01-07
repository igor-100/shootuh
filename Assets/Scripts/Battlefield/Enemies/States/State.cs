public abstract class State
{
    protected IEnemy enemy;
    protected StateMachine stateMachine;

    protected State(IEnemy enemy)
    {
        this.enemy = enemy;
        this.stateMachine = enemy.StateMachine;
    }

    public virtual void Enter()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}