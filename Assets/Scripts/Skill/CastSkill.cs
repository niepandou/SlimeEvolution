using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSkill : Skill
{
    private Animator anim;
    public CastSkillData castSkillData;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //Cast作为护盾类型,有一定存在时间,时间一到播放消失动画后才销毁
    protected override void Start()
    {
        //护盾生成时规定了要作为挂载了Character组件的物体的子物体,因此护盾的免伤和去除免伤可以通过控制父物体Character组件的方式去实现
        GetComponentInParent<Character>().hurtPercent = this.CompareTag("PlayerAttack")
            ? castSkillData.playerDamagePercent
            : castSkillData.enemyDamagePercent;
        StartCoroutine(WaitForLiveEnd());
    }

    public override void SkillMove()
    {
        ;
    }

    /// <summary>
    /// 等待护盾存在时间结束
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForLiveEnd()
    {
        yield return new WaitForSeconds(skillLifeTime);

        GetComponentInParent<Character>().hurtPercent = 0;
        anim.SetTrigger("end");
    }
}
