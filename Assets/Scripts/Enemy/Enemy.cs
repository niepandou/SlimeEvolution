using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("组件")]
    private Rigidbody2D rb;

    public PhysicsCheck physicsCheck;
    private Animator anim;
    [Header("属性")] 
    public float currentSpeed;
    public float patrolSpeed;
    public float chaseSpeed;
    
    public float waitTime;
    private float waitTimeCounter;
    public float faceDir;
    [Header("状态")] 
    public bool isWait;

    [Header("状态机")] 
    public BaseState currentState;
    public PatrolState patrolState;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();

    }

    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }

    private void FixedUpdate()
    {
        faceDir = Mathf.Sign(transform.localScale.x);

        
        if(!isWait)
            Move();
        
        //状态机
        currentState.LogicUpdate();
        //撞墙计时器
        TimeCounter();
    }

    private void Move()
    {

        rb.velocity = new Vector2(currentSpeed * transform.localScale.x * Time.deltaTime,
            rb.velocity.y);
        
    }
    

    private void TimeCounter()
    {
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
    }
}
