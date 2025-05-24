using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        currentEnemy.isWait = false;
        currentEnemy.waitTimeCounter  = 0;
        currentEnemy.lostTimeCounter = 0;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
    }

    public override void LogicUpdate()
    {
        //丢失玩家计时判断
        if (currentEnemy.lostTimeCounter >= currentEnemy.lostTime)
        {
            currentEnemy.SwitchState(NpcState.Patrol);
        }
        //发现玩家,不需要进行撞墙等待,撞墙会直接转向
        if (currentEnemy.physicsCheck.isOnEdge || currentEnemy.physicsCheck.headTouchCheck)
        {
            currentEnemy.transform.localScale = new Vector3(-currentEnemy.faceDir, 1, 1);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void OnExit()
    {
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
    }
}
