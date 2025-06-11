using System;
using System.Collections;
using System.Collections.Generic;
using Frame;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public GameObject SkillTreeUI;
    [Header("组件")]
    private PlayerInputControl playerInput;
    private Rigidbody2D rb;
    private Animator anim;
    private PhysicsCheck physicsCheck;
    private Character character;
    [Header("属性")] 
    [SerializeField] private Vector2 moveInput;
    public float speed;
    public float moveDir;
    public float jumpForce;
    public float hurtForce;
    [Header("技能模板")] 
    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    [Header("特殊技能")] 
    public bool couldJumpTwice;
    private int jumpCounter;
    public bool couldSmashDown;
    [Header("技能cd")] 
    private bool skill1Frozen;
    private bool skill2Frozen;
    private bool skill3Frozen;
    [SerializeField] private float skill1CD;
    [SerializeField] private float skill2CD;
    [SerializeField] private float skill3CD;
    [Header("状态")]
    public bool down;

    public bool isDead;
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
        character = GetComponent<Character>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.GamePlay.Jump.started += Jump;
        playerInput.GamePlay.Attack.started += PlayerAttack;
        playerInput.GamePlay.Skill1.started += PlayerSKill1;
        playerInput.GamePlay.Skill2.started += PlayerSkill2;
        playerInput.GamePlay.SmashDown.started += PlayerSmashDown;
    }

    private void Update()
    {
        //得到输入值
        moveInput = playerInput.GamePlay.Move.ReadValue<Vector2>();

    }

    private void FixedUpdate()
    {
        if(!down && !character.isHurt && !isDead)
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
        if (isDead) return;
        
        if (physicsCheck.isGround)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            jumpCounter = 1;
            AudioManager.instance.PlayFxSound(AudioManager.instance.audioData.sfxJump);
        }else if (jumpCounter == 1 && couldJumpTwice)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            jumpCounter = 2;
            AudioManager.instance.PlayFxSound(AudioManager.instance.audioData.sfxJump);
        }
    }
    /// <summary>
    /// 下砸动作
    /// </summary>
    /// <param name="obj"></param>
    private void PlayerSmashDown(InputAction.CallbackContext obj)
    {
        if (!physicsCheck.isGround && couldSmashDown)
        {
            AudioManager.instance.PlayFxSound(AudioManager.instance.audioData.down);
            down = true;
            rb.AddForce(Vector2.down * jumpForce,ForceMode2D.Impulse);
        }
    }

    //Line
    private void PlayerAttack(InputAction.CallbackContext callbackContext)
    {
        if (!skill1 || isDead) return;

        if (!skill1Frozen)
        {
            AudioManager.instance.PlayFxSound(AudioManager.instance.audioData.sfxShoot);
            StartCoroutine(SkillCounter(skill1CD,(value)=>skill1Frozen = value));
            GameObject skill = Instantiate(skill1,transform.position,this.transform.rotation);
            skill.tag = "PlayerAttack";
            skill.transform.localScale = this.transform.localScale;
            anim.SetTrigger("attack");
        }
        
    }
    //Explosion
    private void PlayerSKill1(InputAction.CallbackContext obj)
    {
        if (!skill2 || isDead) return;
        if (!skill2Frozen)
        {
            AudioManager.instance.PlayFxSound(AudioManager.instance.audioData.sfxExplosion);
            StartCoroutine(SkillCounter(skill2CD,(value)=>skill2Frozen = value));
            //偏移量
            float x = -1.5f, y = 1.5f;
        
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    Vector3 skillPosition = new Vector3(transform.position.x + x, transform.position.y + y,
                        transform.position.z);
                    GameObject skill = Instantiate(skill2,skillPosition,this.transform.rotation);
                    skill.tag = "PlayerAttack";
                    for (int k = 0; k < 3; ++k)
                    {
                        skill.transform.GetChild(k).gameObject.tag = "PlayerAttack";
                    }
                    
                    skill.transform.localScale = this.transform.localScale;
                    x += 1.5f;
                }

                x = -1.5f;
                y -= 1.5f;
            }
        
            anim.SetTrigger("attack");
        }
    }
    //Cast
    private void PlayerSkill2(InputAction.CallbackContext obj)
    {
        if (!skill3 || isDead) return;
        
        if (!skill3Frozen)
        {
            AudioManager.instance.PlayFxSound(AudioManager.instance.audioData.sfxCast);
            StartCoroutine(SkillCounter(skill3CD,(value)=>skill3Frozen = value));
            GameObject skill = Instantiate(skill3, transform);
            skill.tag = "PlayerAttack";
            anim.SetTrigger("attack");
        }
        
    }

    /// <summary>
    /// 技能内置Cd冷却
    /// </summary>
    /// <param name="time">内置cd</param>
    /// <param name="setFrozen">设置技能冷却状态</param>
    /// <returns></returns>
    IEnumerator SkillCounter(float time,Action<bool> setFrozen)
    {
        setFrozen(true);
        yield return new WaitForSeconds(time);
        setFrozen(false);
    }

    public void GetHurt(Transform attacker)
    {
        anim.SetTrigger("hurt");
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(attacker.localScale.x * 0.1f,1f) * hurtForce,ForceMode2D.Impulse);
    }

    public void OnDie()
    {
        isDead = true;
    }

    public void Dead()
    {
        SceneManager.LoadScene(0);
    }
}
