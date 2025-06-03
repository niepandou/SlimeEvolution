using System;
using System.Collections;
using System.Collections.Generic;
using Frame;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public GameObject SkillTreeUI;
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
    [Header("技能模板")] 
    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    private void Awake()
    {
        if (!instance )
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
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
        playerInput.GamePlay.Skill1.started += PlayerSKill1;
        playerInput.GamePlay.Skill2.started += PlayerSkill2;
        //暂停面板
        playerInput.UI.Pause.started += PausePanel;
    }

    

    //TODO:后续需将该方法放到指定的Manager当中
    private void PausePanel(InputAction.CallbackContext obj)
    {
        if (!SkillTreeUI.activeInHierarchy)
        {
            SkillTreeUI.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            SkillTreeUI.SetActive(false);
            Time.timeScale = 1;
        }
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
        Instantiate(skill1,transform);
        anim.SetTrigger("attack");
    }
    private void PlayerSKill1(InputAction.CallbackContext obj)
    {
        GameObject skill = Instantiate(skill2,transform.position,this.transform.rotation);
        skill.transform.localScale = this.transform.localScale;
    }
    private void PlayerSkill2(InputAction.CallbackContext obj)
    {
        GameObject skill = Instantiate(skill3,transform.position,this.transform.rotation);
        skill.transform.localScale = this.transform.localScale;

    }

}
