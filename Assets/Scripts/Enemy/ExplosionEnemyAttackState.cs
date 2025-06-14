using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ExplosionEnemyAttackState : BaseState
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
        //发现玩家,追随玩家
        //发现了玩家,说明他是一直在我的搜素范围内,这样的话就保持移动即可,所以不用写
        
        //和玩家在一定距离以内,就可以进行自爆的动作
        if (Mathf.Abs(currentEnemy.transform.position.x - PlayerController.instance.transform.position.x) < 2f
            && Mathf.Abs(currentEnemy.transform.position.y - PlayerController.instance.transform.position.y) < 2f)
        {
            if (!((ExplosionEnemy)currentEnemy).booming)
            {
                currentEnemy.anim.SetBool("boom",true);
                //开始自爆计时
                ((ExplosionEnemy)currentEnemy).booming = true;
            }
        }

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
        currentEnemy.anim.SetBool("boom",false);
        ((ExplosionEnemy)currentEnemy).booming = false;
        ((ExplosionEnemy)currentEnemy).boomTimeCounter = 0;
    }
}
