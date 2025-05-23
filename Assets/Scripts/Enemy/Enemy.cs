using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("组件")]
    private Rigidbody2D rb;

    private PhysicsCheck physicsCheck;
    private Animator anim;
    [Header("属性")] 
    public float currentSpeed;

    public float waitTime;
    private float waitTimeCounter;
    public float faceDir;
    [Header("状态")] 
    public bool isWait;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    private void FixedUpdate()
    {
        if(!isWait)
            Move();
        
        TimeCounter();
    }

    private void Move()
    {
        faceDir = Mathf.Sign(transform.localScale.x);
        
        //撞墙等待等操作
        CheckState();

        rb.velocity = new Vector2(currentSpeed * transform.localScale.x * Time.deltaTime,
            rb.velocity.y);
        
    }

    private void CheckState()
    {
        if (physicsCheck.headTouchCheck || physicsCheck.isOnEdge)
        {
            isWait = true;
            rb.velocity = Vector2.zero;
        }
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
