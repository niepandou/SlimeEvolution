using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AutoMoveUpAndDestroyText : MonoBehaviour
{
    private TextMeshPro text;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        StartCoroutine(AutoDestroy());
        text.DOFade(0, 2f);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y + Time.deltaTime,transform.position.z);
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
