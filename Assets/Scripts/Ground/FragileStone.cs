using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FragileStone : MonoBehaviour,IBreakable
{
    private int attackLayer;
    private bool hasGetHurt;
    private void Awake()
    {
        attackLayer = LayerMask.NameToLayer("Attack");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == attackLayer && !hasGetHurt)
        {
            GetHurt();
            hasGetHurt = true;
        }
        //Debug.Log(LayerMask.LayerToName(other.gameObject.layer));
    }
    
    
    public void GetHurt()
    {
        StartCoroutine(Destroy());
        GetComponent<SpriteRenderer>().DOFade(0.3f, 0.1f);

    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
