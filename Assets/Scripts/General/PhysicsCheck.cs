using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PhysicsCheck : MonoBehaviour
{
    [Header("组件")] 
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    [Header("属性")] 
    //地面采用盒子检测,墙体采用圆点检测
    public float checkRadius;
    public Vector2 checkBox;
    private float faceDir;
    private Vector2 backPoint;
    private Vector2 headPoint;
    private Vector2 edgePoint;
    public float groundOffset;
    private int groundLayer;
    

    [Header("状态")]
    public bool isGround;
    //判断是否在悬崖
    public bool isOnEdge;
    //左右撞墙检测
    public bool backTouchCheck;
    public bool headTouchCheck;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        Check();
    }

    private void Check()
    {
        isGround = Physics2D.BoxCast(transform.position, checkBox, 0, Vector2.down
        ,groundOffset,groundLayer);
        //本次项目的所有角色中心点都在图像中心
        //TODO:转向后的潜在问题
        faceDir = transform.localScale.x;
        
        backPoint = new Vector2(transform.position.x - coll.size.x/2 * faceDir,transform.position.y);
        headPoint = new Vector2(transform.position.x + coll.size.x/2 * faceDir,transform.position.y);
        edgePoint = new Vector2(transform.position.x + coll.size.x / 2 * faceDir, transform.position.y - coll.size.y / 2);

        backTouchCheck = Physics2D.OverlapCircle(backPoint, checkRadius, groundLayer);
        headTouchCheck = Physics2D.OverlapCircle(headPoint, checkRadius, groundLayer);
        isOnEdge = !Physics2D.OverlapCircle(edgePoint,checkRadius,groundLayer);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(transform.position + Vector3.down * groundOffset,checkBox);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(backPoint,checkRadius);
        Gizmos.DrawSphere(headPoint,checkRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(edgePoint,checkRadius);
    }
}
