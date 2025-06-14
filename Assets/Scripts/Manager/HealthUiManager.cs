using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUiManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    [Header("事件监听")]
    public CharacterEventSO onHealthChange;

    private void OnEnable()
    {
        onHealthChange.OnEventRaised += ChangeHealth;
    }

    private void OnDisable()
    {
        onHealthChange.OnEventRaised -= ChangeHealth;
    }

    public void ChangeHealth(Character character)
    {
        text.text = "HP: " + character.currentHealth + "/" + character.maxHealth;
    }
}
