using System;
using UnityEngine;

public class IdleState : BaseState
{

    private IPlayerInput PlayerInput;

    public IdleState(Warrior character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Idle State Enter");

        PlayerInput = CompositionRoot.GetPlayerInput();
        PlayerInput.Move += OnMove;
    }

    private void OnMove(Vector2 moveVector)
    {
        Debug.Log("Idle State Exit");
        PlayerInput.Move -= OnMove;
        stateMachine.ChangeState(warrior.running);
    }
}
