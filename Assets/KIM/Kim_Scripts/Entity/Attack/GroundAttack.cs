using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAttack : IAttackType
{
    PoolManager poolManager;
    private string poolkey;
    private Transform pivot;
    public GroundAttack(PoolManager _poolManager, string _key, Transform _pivot)
    {
        poolManager = _poolManager;
        poolkey = _key;
        pivot = _pivot;
    }

    public void ExecuteAttack()
    {
        GameObject obj = poolManager.GetPoolObject(poolkey, null);
        obj.transform.position = pivot.position;
        obj.transform.rotation = pivot.rotation;
        
        
    }
}
