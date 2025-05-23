using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Header("组件")]
    private PlayerInputControl playerInput;
    private Rigidbody2D rb;
    private Animator anim;
    private PhysicsCheck physicsCheck;
    [Header("属性")] 
    [SerializeField] private Vector2 moveInput;
    public float speed;
    public float moveDir;
    public float jumpForce;
    private void Awake()
    {
        playerInput = new PlayerInputControl();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.GamePlay.Jump.started += Jump;
        playerInput.GamePlay.Attack.started += PlayerAttack;
    }

    private void Update()
    {
        //得到输入值
        moveInput = playerInput.GamePlay.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //判断转身
        moveDir = moveInput.x;
        if (moveDir != 0)
        {
            transform.localScale = new Vector3(moveDir, 1, 1);
        }
        
        //施加速度
        rb.velocity = new Vector2(moveInput.x * speed * Time.deltaTime, rb.velocity.y);
    }

    private void Jump(InputAction.CallbackContext callbackContext)
    {
        if(physicsCheck.isGround)
            rb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
    }

    private void PlayerAttack(InputAction.CallbackContext callbackContext)
    {
        anim.SetTrigger("attack");
    }
    

    
}
