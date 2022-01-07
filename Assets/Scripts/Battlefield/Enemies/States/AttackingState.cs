using System;
using System.Timers;
using UnityEngine;

public class AttackingState : FightingState
{
    private Timer timer;

    private const string attackTrigger = "attack";

    public AttackingState(IEnemy character) : base(character)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.TriggerAnimation(attackTrigger);

        timer = new Timer(enemy.AttackTime * 1000);
        timer.Elapsed += OnAttackCompleted;
        timer.AutoReset = true;
        timer.Enabled = true;
    }

    private void OnAttackCompleted(object sender, ElapsedEventArgs e)
    {
        enemy.StateMachine.ChangeState(enemy.WalkingState);
    }

    public override void Exit()
    {
        base.Exit();
        timer.Elapsed -= OnAttackCompleted;
    }
}
