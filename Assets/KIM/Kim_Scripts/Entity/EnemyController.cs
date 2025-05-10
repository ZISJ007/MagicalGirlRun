using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private State currentState;
    
    public IAttackType AttackType {get; set; }
    
    [Header("Attack Settings")]
    private PoolManager poolManager;
    public string attackPoolKey="GroundAttack";
    [SerializeField] private Transform groundAttackPivot;
    [SerializeField] private float attackInterval = 2f;

    private void Awake()
    {
        poolManager=FindObjectOfType<PoolManager>();
    }

    private void Start()
    {
        AttackType = new GroundAttack();
        currentState = new AttackState(this);
    }

    private void Update()
    {
        currentState.Execute();
    }

    public void SetState(State _newstate)
    {
        currentState = _newstate;
    }

    public void ChangeAttackType(IAttackType _newattacktype)
    {
        AttackType=_newattacktype;
    }
}
