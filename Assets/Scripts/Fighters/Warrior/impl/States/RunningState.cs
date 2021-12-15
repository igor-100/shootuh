using UnityEngine;

public class RunningState : BaseState
{
    private IPlayerInput PlayerInput;
    private Vector3 playerVelocity;

    public RunningState(Warrior character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Running State Enter");

        playerVelocity = warrior.rb.velocity;

        PlayerInput = CompositionRoot.GetPlayerInput();
        PlayerInput.Move += OnMove;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (playerVelocity == Vector3.zero)
        {
            stateMachine.ChangeState(warrior.idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
        PlayerInput.Move -= OnMove;
        Debug.Log("Running State Exit");
    }

    private void OnMove(Vector2 moveVector)
    {
        warrior.Move(moveVector);
    }
}
