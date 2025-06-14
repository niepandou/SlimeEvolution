using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CinemachineManager : MonoBehaviour
{
    public static CinemachineManager instance;
    private CinemachineConfiner2D confiner;
    public VoidEventSo getHurtEvent;
    private CinemachineImpulseSource impulseSourcesource;

    private void OnEnable()
    {
        getHurtEvent.onEventRaised += CameraShake;
    }

    private void OnDisable()
    {
        getHurtEvent.onEventRaised -= CameraShake;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        confiner = GetComponent<CinemachineConfiner2D>();
        impulseSourcesource = GetComponent<CinemachineImpulseSource>();
    }

    public void ChangeBounds(PolygonCollider2D collider2D)
    {
        confiner.m_BoundingShape2D = collider2D;
    }

    public void CameraShake()
    {
        impulseSourcesource.GenerateImpulse();
    }
}
