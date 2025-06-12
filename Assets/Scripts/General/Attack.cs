using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public float startDamage;
    public float attackRate;

    private void Awake()
    {
        damage = startDamage;
    }

    public float GetDamage()
    {
        return damage;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((this.CompareTag("PlayerAttack") && other.gameObject.CompareTag("Enemy"))
            || (this.CompareTag("EnemyAttack") && other.gameObject.CompareTag("Player")))
        {
            other.GetComponent<Character>()?.GetHurt(this);
        }
    }
}
