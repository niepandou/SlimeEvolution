using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private PlayerController player;
    private Animator anim;
    private PhysicsCheck physicsCheck;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
    }

    private void Update()
    {
        anim.SetBool("isGround",physicsCheck.isGround);
        anim.SetBool("down",player.down);
        anim.SetBool("dead",player.isDead);
    }
}
