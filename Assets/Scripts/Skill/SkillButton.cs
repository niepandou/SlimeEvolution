using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//用于SkillTree当中点击某个技能触发事件
public class SkillButton : MonoBehaviour,IPointerClickHandler
{
    public SkillData skillData;

    public void OnPointerClick(PointerEventData eventData)
    {
        SkillManager.instance.activeSkill = skillData;
        SkillManager.instance.DisplaySkillInfo();
    }
}
