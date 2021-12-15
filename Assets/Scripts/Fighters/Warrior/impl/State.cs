public abstract class State
{
    protected Warrior warrior;
    protected StateMachine stateMachine;

    protected State(Warrior warrior, StateMachine stateMachine)
    {
        this.warrior = warrior;
        this.stateMachine = stateMachine;
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