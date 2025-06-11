using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public float attackRate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((this.CompareTag("PlayerAttack") && other.gameObject.CompareTag("Enemy"))
            || (this.CompareTag("EnemyAttack") && other.gameObject.CompareTag("Player")))
        {
            other.GetComponent<Character>()?.GetHurt(this);
        }
    }
}
