using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    //TODO:技能管理
    public GameObject fireball;
    public bool isSkilling;
    
    private void Update()
    {
        //TODO:小火球生成
        if (!isSkilling)
        {
            isSkilling = true;
            StartCoroutine(GenerateFireBall());
        }
    }

    IEnumerator GenerateFireBall()
    {
        yield return new WaitForSeconds(2f);
        GameObject currentFireBall = Instantiate(fireball);
        currentFireBall.layer = LayerMask.NameToLayer("Enemy");
        currentFireBall.transform.position = transform.position;
        currentFireBall.transform.localScale = transform.localScale;

        isSkilling = false;
    }
}
