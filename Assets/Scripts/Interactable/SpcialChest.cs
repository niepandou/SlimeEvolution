using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SpcialChest : Chest
{
    public GameObject skillDes;//获得特殊能力的上浮文本预制体
    public GameObject wallDes;//获得特殊能力后显示的help文本
    public UnityEvent openEvent;
    protected override void OpenChest()
    {
        base.OpenChest();
        openEvent?.Invoke();
    }

    public void GetJumpTwiceSkill()
    {
        PlayerController.instance.couldJumpTwice = true;
        GameObject currentGameObject = Instantiate(skillDes,transform.position + Vector3.up * 1f,Quaternion.identity);
        currentGameObject.GetComponent<TextMeshPro>().text = "Get Jump Twice Now!!!";
        wallDes.SetActive(true);
    }

    public void GetSlamSkill()
    {
        PlayerController.instance.couldSmashDown = true;
        GameObject currentGameObject = Instantiate(skillDes,transform.position + Vector3.up * 1f,Quaternion.identity);
        currentGameObject.GetComponent<TextMeshPro>().text = "Get Ground Slam Now!!!";
        wallDes.SetActive(true);
    }
}
