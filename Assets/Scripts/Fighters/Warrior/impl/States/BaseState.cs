using System;
using UnityEngine;

public class BaseState : State
{
    public BaseState(Warrior character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        var playerInput = CompositionRoot.GetPlayerInput();
        playerInput.MousePos += OnRotate;
    }

    public override void Exit()
    {
        base.Exit();
        //character.ResetMoveParams();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        //warrior.Move(movement);
    }

    private void OnRotate(Vector3 mousePos)
    {
        warrior.Rotate(mousePos);
    }
}
