using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected EnemyController enemyController;

    public State(EnemyController enemyController)
    {
        
    }
    public abstract void Execute();
}
