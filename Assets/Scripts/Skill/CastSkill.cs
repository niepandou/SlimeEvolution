using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSkill : Skill
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //Cast作为护盾类型,有一定存在时间,时间一到播放消失动画后才销毁
    protected override void Start()
    {
        //TODO:护盾生成,减伤效果
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
     
        //TODO:护盾消失,减伤去除
        anim.SetTrigger("end");
    }
}
