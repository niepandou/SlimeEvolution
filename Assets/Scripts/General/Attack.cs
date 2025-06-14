using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Attack : MonoBehaviour
{
    public AttackData attackData;

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((this.CompareTag("PlayerAttack") && other.gameObject.CompareTag("Enemy"))
            || (this.CompareTag("EnemyAttack") && other.gameObject.CompareTag("Player"))
            || this.CompareTag("Attack"))
        {
            other.GetComponent<Character>()?.GetHurt(this);
        }
    }
}
