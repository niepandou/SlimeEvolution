using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Sign : MonoBehaviour
{
    public PlayerInputControl playerInput;
    private Animator anim;
    public GameObject signSprite;
    private IInteractable targetItem;
    private bool canPress;
    public AudioClip interactSound;
    
    private void Awake()
    {
        anim = signSprite.GetComponent<Animator>();
        playerInput = PlayerController.instance.playerInput;
        playerInput.Enable();
    }

    private void OnEnable()
    { 
        InputSystem.onActionChange += OnActionChange;
        playerInput.GamePlay.Confirm.started += OnConfirm;
    }
    
    private void OnDisable()
    {
        InputSystem.onActionChange -= OnActionChange;
        playerInput.GamePlay.Confirm.started -= OnConfirm;
        canPress = false;
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
    }
    
    private void OnActionChange(object arg1, InputActionChange actionChange)
    {
        if (actionChange == InputActionChange.ActionStarted)
            anim.Play("Sign Idel");
    }
    private void Update()
    {
        signSprite.transform.localScale = PlayerController.instance.transform.localScale; 
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("Interactable"))
        {
            canPress = true;
            targetItem = other.GetComponent<IInteractable>();
            signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canPress = false;
        signSprite.GetComponent<SpriteRenderer>().enabled = canPress;
    }
    
    private void OnConfirm(InputAction.CallbackContext obj)
    {
        if (canPress)
        {
            targetItem.TriggerAction();
            AudioManager.instance.PlayFxSound(interactSound);
        }
    }
}