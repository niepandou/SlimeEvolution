using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(PhysicsCheck))]
[RequireComponent(typeof(Animator)), RequireComponent(typeof(Character), typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    [Header("组件")]
    protected Rigidbody2D rb;
    protected Character character;
    public PhysicsCheck physicsCheck;
    public Animator anim;
    [Header("属性")] 
    public float currentSpeed;
    public float patrolSpeed;
    public float attackSpeed;
    public float foundSpeed;
    public Vector2 foundSize;
    public float foundDistance;
    public int playerLayer;
    public float hurtForce;
    [Header("技能")]
    public GameObject skill;
    public float skillFrozenTime;
    public bool skillFrozen;
    
    [Header("计时器")]
    public float waitTime;
    public float waitTimeCounter;
    public float lostTime;
    public float lostTimeCounter;
    
    public float faceDir;
    [Header("状态")] 
    public bool isWait;
    public bool isDead;
    [Header("状态机")] 
    protected BaseState currentState;
    protected BaseState patrolState;
    protected BaseState attackState;
    protected BaseState foundState;
    
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();

        playerLayer = LayerMask.GetMask("Player");
    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    protected virtual void FixedUpdate()
    {
        faceDir = Mathf.Sign(transform.localScale.x);
        
        if(!isWait && !character.isHurt && !isDead)
            Move();
        
        //状态机
        currentState.LogicUpdate();
        //撞墙计时器
        TimeCounter();
    }

    protected void Move()
    {
        rb.velocity = new Vector2(currentSpeed * transform.localScale.x * Time.deltaTime,
            rb.velocity.y);
    }


    public void GetHurt(Transform attacker)
    {
        if (attacker.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        
        anim.SetTrigger("hurt");
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(-faceDir,0.3f) * hurtForce,ForceMode2D.Impulse);
    }

    public void OnDie()
    {
        //TODO:敌人死亡动画,进入死亡状态,结束销毁
        anim.SetBool("dead", true);
        isDead = true;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
    public bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position, foundSize, 0, new Vector2(faceDir,0), foundDistance, playerLayer);
    }
    
    protected void TimeCounter()
    {
        //撞墙等待
        if (isWait)
        {
            waitTimeCounter += Time.deltaTime;
            if (waitTimeCounter >= waitTime)
            {
                isWait = false;
                waitTimeCounter = 0;
                transform.localScale = new Vector3(-faceDir, 1, 1);
            }
        }
        //玩家丢失
        if (!FoundPlayer() && lostTimeCounter < lostTime)
        {
            lostTimeCounter += Time.deltaTime;
        }
    }

    public void SwitchState(NpcState state)
    {
        BaseState newState = state switch
        {
            NpcState.Patrol => patrolState,
            NpcState.Attack => attackState,
            NpcState.Found => foundState,
            _ => null
        };
        
        currentState.OnExit();
        currentState = newState;
        newState.OnEnter(this);
    }

    public void UseSkill()
    {
        if (!skillFrozen && !isDead)
        {
            StartCoroutine(SkillCounter(skillFrozenTime,(value)=>skillFrozen = value));
            skillRelease();
        }
    }

    protected virtual void skillRelease()
    {
        GameObject currentSkill = Instantiate(skill,transform.position,transform.rotation);
        currentSkill.transform.localScale = transform.localScale;
        currentSkill.tag = "EnemyAttack";
    }


    IEnumerator SkillCounter(float time,Action<bool> setFrozen)
    {
        setFrozen(true);
        yield return new WaitForSeconds(time);
        setFrozen(false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.right * (foundSize.x + foundDistance) * faceDir/2, 
            foundSize + new Vector2(foundDistance,0));
    }
}
