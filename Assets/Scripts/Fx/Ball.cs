using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float fadeTime;
    public float currentSpeed;
    private float faceDir;
    private void Start()
    {
        faceDir = transform.localScale.x;
        StartCoroutine(Disappear());
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + currentSpeed * Time.deltaTime * faceDir,
            transform.position.y,
            transform.position.z);
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(fadeTime);
        
        Destroy(this.gameObject);
    }
}
