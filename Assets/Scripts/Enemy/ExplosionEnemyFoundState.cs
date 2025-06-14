using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ExplosionEnemyFoundState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.isWait = true;
        currentEnemy.waitTimeCounter  = 0;
        currentEnemy.lostTimeCounter = 0;
        currentEnemy.currentSpeed = currentEnemy.foundSpeed;
        currentEnemy.anim.SetBool("founding",true);
    }

    public override void LogicUpdate()
    {
        currentEnemy.isWait = true;
        
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NpcState.Attack);
        }else if (currentEnemy.lostTimeCounter >= currentEnemy.lostTime)
        {
            currentEnemy.SwitchState(NpcState.Patrol);
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
        currentEnemy.anim.SetBool("founding",false);
        currentEnemy.lostTimeCounter = 0;
    }
}
