using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");
    protected Animator animator;

    protected void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Attack()
    {
        animator.SetBool(IsAttack, true);
    }

    public void EndAttack()
    {
        animator.SetBool(IsAttack, false);
    }
    
}
