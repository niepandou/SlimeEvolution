using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void GetHurt(Attack attacker)
    {
        if (currentHealth - attacker.damage > 0)
        {
            currentHealth -= attacker.damage;
        }
        else
        {
            currentHealth = 0;
            //TODO:死亡
        }
        
        //TODO:受伤反馈
    }
}
