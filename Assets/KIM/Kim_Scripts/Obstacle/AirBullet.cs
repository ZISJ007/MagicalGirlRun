using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 10f;
    
    private PoolManager poolManager;
    private string bulletObjectPoolKey;
    private float timer;

    public void Launch(PoolManager _poolManager, string _bulletObjectPoolKey)
    {
        poolManager = _poolManager;
        bulletObjectPoolKey = _bulletObjectPoolKey;
        timer = 0f;
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.forward;
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            poolManager.ReturnPoolObject(gameObject, bulletObjectPoolKey);
        }
    }
}
