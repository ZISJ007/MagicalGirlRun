using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public PoolManager PoolManager{get;set;}
    public string PoolKey{get;set;}

    public void returnPool()
    {
        PoolManager.ReturnPoolObject(this.gameObject, PoolKey);
    }
}
