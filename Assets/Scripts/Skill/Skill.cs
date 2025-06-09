using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//暂时用不着
//技能模板
public abstract class Skill : MonoBehaviour
{
    //技能通用移动速度
    public float speed;
    //技能存在时间
    public float skillLifeTime;

    protected virtual void Start()
    {
        StartCoroutine(SkillLifeStart());
    }

    private void FixedUpdate()
    {
        SkillMove();
    }

    //技能移动逻辑
    public virtual void SkillMove()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime * transform.localScale.x, transform.position.y,
            transform.position.z);
    }
    //技能生命周期
    protected IEnumerator SkillLifeStart()
    {
        yield return new WaitForSeconds(skillLifeTime);
        
        Destroy(gameObject);
    }
    //动画结束控制销毁
    public void OnAnimationDestory()
    {
        Destroy(this.gameObject);
    }
}
