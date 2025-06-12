using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Data")]
public class AttackData : ScriptableObject
{
    public float playerDamage;
    public float enemyDamage;
    public float startDamage;
}
