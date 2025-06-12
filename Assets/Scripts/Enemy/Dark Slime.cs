using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSlime : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        patrolState = new PatrolState();
        attackState = new AttackState();
        foundState = new FoundState();
    }
}
