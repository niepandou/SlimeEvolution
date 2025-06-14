using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class NormalChest : Chest
{
    public CharacterEventSO onHealthChange;

    protected override void OpenChest()
    {
        base.OpenChest();
        PlayerController.instance.character.currentHealth = PlayerController.instance.character.maxHealth;
        onHealthChange.OnRaisedEvent(PlayerController.instance.character);
    }
}
