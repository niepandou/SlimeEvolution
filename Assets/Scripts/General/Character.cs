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
            hurtEvent.Invoke(attacker.transform);
            if (currentHealth - attacker.damage > 0)
            {
                isHurt = true;
                currentHealth -= attacker.damage;
                wudi = true;
            }
            else
            {
                currentHealth = 0;
                //TODO:死亡
            }
            
            //TODO:受伤反馈

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
