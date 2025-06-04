using UnityEngine;
using Utils;

[CreateAssetMenu(menuName = "New Skill",fileName = "Skill")]
public class SkillData : ScriptableObject
{
    public int skillID;
    public Sprite skillSprite;
    public string skillName;
    public int skillLevel;
    
    [TextArea(1, 8)] public string skillDes;

    public bool isUnlocked;
    public SkillData[] preSkills;

    public GameObject skillGameObject;
    public SkillType skillType;
}
