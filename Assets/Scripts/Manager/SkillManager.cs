using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//原小火球生成
public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public SkillData activeSkill;
    [Header("UI")] 
    public Image skillImage;
    public TextMeshProUGUI skillName, skillLevel, skillDes;
    [Header("Skill Point")] 
    [SerializeField] private int skillPoint;
    public TextMeshProUGUI pointText;
    
    public SkillButton[] skillButtons;

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
