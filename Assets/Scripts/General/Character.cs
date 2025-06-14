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
    public float hurtPercent;//减伤比例
    public bool wudi;
    public float wudiTimeCounter;
    public float wudiTime;
    public bool isHurt;

    public UnityEvent<Transform> hurtEvent;
    public UnityEvent onDieEvent;
    [Header("信号传递")]
    public CharacterEventSO onHealthChange;
    public VoidEventSo getHurtEvent;
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
            if (attacker.CompareTag("Attack")) damage = attacker.attackData.enemyDamage;
            
            damage = damage*(1-this.hurtPercent/100);
            if (currentHealth - damage > 0)
            {
                isHurt = true;
                currentHealth -= damage;
                wudi = true;
                hurtEvent.Invoke(attacker.transform);
                getHurtEvent.RaiseEvent();
            }
            else
            {
                currentHealth = 0;
                onDieEvent.Invoke();
            }

            if (this.CompareTag("Player"))
            {
                onHealthChange?.OnRaisedEvent(this);
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
