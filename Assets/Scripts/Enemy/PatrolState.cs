using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.patrolSpeed;
    }

    public override void LogicUpdate()
    {
        //发现玩家
        if (currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NpcState.Attack);
        }
        //巡逻逻辑
        
        //撞墙
        if (currentEnemy.physicsCheck.isOnEdge || currentEnemy.physicsCheck.headTouchCheck)
        {
            currentEnemy.isWait = true;
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}
