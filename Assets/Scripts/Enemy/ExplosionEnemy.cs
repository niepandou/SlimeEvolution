using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEnemy : Enemy
{

    public float boomTime;
    public float boomTimeCounter;
    public bool booming;
    
    protected override void Awake()
    {
        base.Awake();
        patrolState = new ExplosionEnemyPatrolState();
        attackState = new ExplosionEnemyAttackState();
        foundState = new ExplosionEnemyFoundState();
    }

    public override void Die()
    {
        AudioManager.instance.PlayFxSound(AudioManager.instance.audioData.sfxExplosion);
        //偏移量
        float x = -1.5f, y = 1.5f;
        
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                Vector3 skillPosition = new Vector3(transform.position.x + x, transform.position.y + y,
                    transform.position.z);
                GameObject skill = Instantiate(base.skill,skillPosition,this.transform.rotation);
                skill.tag = "Attack";
                for (int k = 0; k < 3; ++k)
                {
                    skill.transform.GetChild(k).gameObject.tag = "Attack";
                }
                    
                skill.transform.localScale = this.transform.localScale;
                x += 1.5f;
            }

            x = -1.5f;
            y -= 1.5f;
        }
        
        base.Die();
    }

    protected override void FixedUpdate()
    {
        faceDir = Mathf.Sign(transform.localScale.x);
        
        if(!isWait && !character.isHurt && !isDead && !booming)
            Move();
        
        //状态机
        currentState.LogicUpdate();
        //撞墙计时器
        TimeCounter();
        
        BoomTimeCounter();
    }
    
    public void BoomTimeCounter()
    {
        if (booming)
        {
            boomTimeCounter += Time.deltaTime;
            if (boomTimeCounter >= boomTime)
            {
                Die();
            }
        }
    }
}
