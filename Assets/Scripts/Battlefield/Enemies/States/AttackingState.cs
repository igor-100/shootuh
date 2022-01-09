using System;
using System.Timers;
using UnityEngine;

public class AttackingState : FightingState
{
    private const string attackTrigger = "attack";
    //private Timer timer;
    private float attackTimeLeft;

    public AttackingState(IEnemy character) : base(character)
    {
    }

    public override void Enter()
    {
        base.Enter();

        attackTimeLeft = enemy.AttackTime;
        enemy.TriggerAnimation(attackTrigger);

        //timer = new Timer(enemy.AttackTime * 1000);
        //timer.Elapsed += OnAttackCompleted;
        //timer.AutoReset = true;
        //timer.Enabled = true;
    }

    //private void OnAttackCompleted(object sender, ElapsedEventArgs e)
    //{
    //    enemy.StateMachine.ChangeState(enemy.WalkingState);
    //}

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        attackTimeLeft -= Time.deltaTime;
        if (attackTimeLeft < 0)
        {
            enemy.StateMachine.ChangeState(enemy.WalkingState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        //timer.Elapsed -= OnAttackCompleted;
    }
}
