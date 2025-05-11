using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 5f;
    
    private PoolManager poolManager;
    private string bulletObjectPoolKey;
    private Vector3 rotatedDic;
    private float timer;

    public void Launch(PoolManager _poolManager, string _bulletObjectPoolKey,  Vector3 _rotatedDic)
    {
        poolManager = _poolManager;
        bulletObjectPoolKey = _bulletObjectPoolKey;
        rotatedDic = _rotatedDic.normalized;
        timer = 0f;
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * rotatedDic;
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            poolManager.ReturnPoolObject(gameObject, bulletObjectPoolKey);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("총알 충돌");
    }
}
