using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public bool wudi;
    public float wudiTimeCounter;
    public float wudiTime;
    public bool isHurt;

    public UnityEvent<Transform> hurtEvent;
    public UnityEvent onDieEvent;
    private void Update()
    {
        WuDiTimeCounter();
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void GetHurt(Attack attacker)
    {
        if (!wudi)
        {
            float damage = this.CompareTag("Enemy")?attacker.attackData.playerDamage:attacker.attackData.enemyDamage;
            if (currentHealth -damage > 0)
            {
                isHurt = true;
                currentHealth -= damage;
                wudi = true;
                hurtEvent.Invoke(attacker.transform);
            }
            else
            {
                currentHealth = 0;
                onDieEvent.Invoke();
            }
        }
    }

    public void WuDiTimeCounter()
    {
        if (wudi)
        {
            wudiTimeCounter += Time.deltaTime;
            if (wudiTimeCounter >= wudiTime)
            {
                wudiTimeCounter = 0;
                wudi = false;
            }
        }
    }
}
