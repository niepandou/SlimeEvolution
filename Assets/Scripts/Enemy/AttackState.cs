using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class AttackState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.isWait = false;
        currentEnemy.waitTimeCounter = 0;
        currentEnemy.lostTimeCounter = 0;
        currentEnemy.anim.SetTrigger("founded");
        currentEnemy.currentSpeed = currentEnemy.attackSpeed;
    }

    public override void LogicUpdate()
    {
        currentEnemy.UseSkill();
        if (!currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NpcState.Found);
        }
    }
    
    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
    }
}
