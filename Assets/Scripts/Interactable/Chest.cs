using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Chest : MonoBehaviour,IInteractable
{
    public Animator anim;
    public bool isDone;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerAction()
    {
        if (!isDone)
        {
            OpenChest();
            isDone = true;
            gameObject.tag = "Untagged";
        }
    }

    protected virtual void OpenChest()
    {
        anim.SetBool("open",true);
    }
}