using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skill/Cast Skill Data")]
public class CastSkillData : ScriptableObject
{
    public float playerDamagePercent;//免伤比例
    public float enemyDamagePercent;
    public float startDamagePercent;
}
