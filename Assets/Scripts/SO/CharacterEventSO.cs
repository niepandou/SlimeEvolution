using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/Character Event")]
public class CharacterEventSO : ScriptableObject
{
    public UnityAction<Character> OnEventRaised;
    
    public void OnRaisedEvent(Character character)
    {
        OnEventRaised.Invoke(character);
    }
}
