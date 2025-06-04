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

    //Line
    private void PlayerAttack(InputAction.CallbackContext callbackContext)
    {
        GameObject skill = Instantiate(skill1,transform.position,this.transform.rotation);
        skill.transform.localScale = this.transform.localScale;
        anim.SetTrigger("attack");
    }
    //Explosion
    private void PlayerSKill1(InputAction.CallbackContext obj)
    {
        //偏移量
        float x = -1.5f, y = 1.5f;
        
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                Vector3 skillPosition = new Vector3(transform.position.x + x, transform.position.y + y,
                    transform.position.z);
                GameObject skill = Instantiate(skill2,skillPosition,this.transform.rotation);
                skill.transform.localScale = this.transform.localScale;
                x += 1.5f;
            }

            x = -1.5f;
            y -= 1.5f;
        }
        
        anim.SetTrigger("attack");
    }
    //Cast
    private void PlayerSkill2(InputAction.CallbackContext obj)
    {
        Instantiate(skill3, transform);
        anim.SetTrigger("attack");
    }

}
