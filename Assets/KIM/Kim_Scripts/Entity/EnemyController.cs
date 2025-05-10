using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("공통 정보")]
    [SerializeField] private Transform pivot;
    [SerializeField] private float attackInterval = 2f;
    [SerializeField] private bool isGroundEnemy = true;

    [Header("Ground Enemy")] 
    [SerializeField] private GameObject groundAttackGameObjectPrefab;
    
    [Space(10),Header("Aerial Enemy")]
    [SerializeField] private string bulletObjectPoolKey = "AerialBullet";
    private PoolManager poolManager;
    
    private GameObject groundAttackObject;

    private void Start()
    {
        if (isGroundEnemy)
        {
            groundAttackObject = Instantiate(groundAttackGameObjectPrefab);
            groundAttackObject.SetActive(false);
        }
        
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(attackInterval);
        }
    }

    private void Fire()
    {
        if (isGroundEnemy)
        {
            groundAttackObject.transform.position = pivot.position;
            groundAttackObject.transform.rotation = pivot.rotation;
            groundAttackObject.SetActive(true);

            GroundAttackObject script = groundAttackObject.GetComponent<GroundAttackObject>();
            script.StartMove();
        }
        else
        {
            GameObject bullet= poolManager.GetPoolObject(bulletObjectPoolKey,null);
            bullet.transform.position = pivot.position;
            bullet.transform.rotation = pivot.rotation;
            
            AirBullet script = bullet.GetComponent<AirBullet>();
            script.Launch(poolManager, bulletObjectPoolKey);
        }
    }

}
