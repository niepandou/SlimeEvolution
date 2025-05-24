using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSlime : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        patrolState = new PatrolState();
        chaseState = new ChaseState();
    }
}
