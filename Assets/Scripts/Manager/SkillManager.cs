using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

//原小火球生成
public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public SkillData[] skillSOs;
    public SkillData activeSkill;
    [Header("UI")] 
    public Image skillImage;
    public TextMeshProUGUI skillName, skillLevel, skillDes;
    [Header("Skill Point")] 
    [SerializeField] private int skillPoint;
    public TextMeshProUGUI pointText;
    
    public SkillButton[] skillButtons;
    [Header("技能装备槽")] 
    public Image skillEquipLine;
    public Image skillEquipExplosion;
    public Image skillEquipCast;
    
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
        
        //TODO:目前没有存档,每次打开时需要重置技能树
        foreach (var skill in skillSOs)
        {
            skill.skillLevel = 0;
            skill.isUnlocked = false;
        }
    }

    private void Start()
    {
        UpdateSkillPoint();
    }

    /// <summary>
    /// 技能信息展示
    /// </summary>
    public void DisplaySkillInfo()
    {
        skillImage.sprite = activeSkill.skillSprite;
        skillName.text = "SkillName: " + activeSkill.skillName;
        skillLevel.text = "SkillLevel: " + activeSkill.skillLevel;
        skillDes.text = "Description:\n" + activeSkill.skillDes;
    }

    public void UpdateSkillPoint()
    {
        pointText.text = "Points: " + skillPoint + "/20";
    }
    /// <summary>
    /// 技能升级
    /// </summary>
    public void UpgradeButton()
    {
        //可能会开局没有选择技能时就进行升级
        if (activeSkill == null) return;
        
        if (skillPoint > 0)
        {
            if (activeSkill.preSkills.Length == 0)
            {
                SkillUpdate();
            }
            else
            {
                for (int i = 0; i < activeSkill.preSkills.Length; ++i)
                {
                    if (activeSkill.preSkills[i].isUnlocked)
                    {
                        SkillUpdate();
                        break;
                    }
                }
            }
        }
        
    }
    /// <summary>
    /// 技能装备
    /// </summary>
    public void EquipButton()
    {
        if(!activeSkill) return;

        if (activeSkill.isUnlocked)
        {
            switch (activeSkill.skillType)
            {
                case SkillType.Line:
                    skillEquipLine.sprite = activeSkill.skillSprite;
                    skillEquipLine.color = Color.white;
                    PlayerController.instance.skill1 = activeSkill.skillGameObject;
                    break;
                case SkillType.Explosion:
                    skillEquipExplosion.sprite = activeSkill.skillSprite;
                    skillEquipExplosion.color = Color.white;
                    PlayerController.instance.skill2 = activeSkill.skillGameObject;
                    break;
                case SkillType.Cast:
                    skillEquipCast.sprite = activeSkill.skillSprite;
                    skillEquipCast.color = Color.white;
                    PlayerController.instance.skill3 = activeSkill.skillGameObject;
                    break;
            }
        }
       
    }

    public void SkillUpdate()
    {
        //LeftPanel
        //无前置条件可以直接激活或升级
        skillButtons[activeSkill.skillID].GetComponent<Image>().color = Color.white;
        //技能升级，展示等级
        activeSkill.skillLevel++;
        skillButtons[activeSkill.skillID].transform.GetChild(1).gameObject.SetActive(true);
        skillButtons[activeSkill.skillID].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text =
            activeSkill.skillLevel.ToString();
        //RightPanel
        //Points更新
        skillPoint--;
        UpdateSkillPoint();
        //SkillLevel更新
        DisplaySkillInfo();

        activeSkill.isUnlocked = true;
    }
}
