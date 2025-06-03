using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public bool wudi;
    public float wudiTimeCounter;
    public float wudiTime;

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
            if (currentHealth - attacker.damage > 0)
            {
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
