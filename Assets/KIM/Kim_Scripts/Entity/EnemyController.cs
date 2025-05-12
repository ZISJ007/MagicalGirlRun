using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EnemyController : MonoBehaviour
{
    [FormerlySerializedAs("pivot")] [Header("공통 정보")] [SerializeField]
    private Transform attackPivot;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private bool isGroundEnemy = true;

    [Header("Ground Enemy")] 
    public float groundAttackInterval = 2f;
    [SerializeField] private GameObject groundAttackGameObjectPrefab;

    [Space(10), Header("Aerial Enemy")] 
    [SerializeField] private float aerialAttackInterval = 2f;
    [SerializeField] private string bulletObjectPoolKey = "AerialBullet";
    [SerializeField] private float angleRange = 30f; // 부채꼴의 각도
    [SerializeField] private float minYOffset = 1f; //최소 Y
    [SerializeField] private float maxYOffset = 3f; //최대 Y

    private PoolManager poolManager;

    private GameObject groundAttackObject;
    //애니메이션
    private static readonly int isAttack = Animator.StringToHash("IsAttack");
    private Animator animator;

    private void Awake()
    {
            animator = GetComponent<Animator>();
        poolManager = FindObjectOfType<PoolManager>();
        if (playerTransform == null) return;
        if (groundAttackObject == null) return;
        if (attackPivot == null)
        {
            Debug.Log("not found pivot");
        }
    }

    private void Start()
    {
        if (isGroundEnemy)
        {
            groundAttackObject = Instantiate(groundAttackGameObjectPrefab, transform);
            groundAttackObject.SetActive(false);
        }

        StartCoroutine(GroundAttackRoutine());
        StartCoroutine(AerialAttackRoutine());
    }

    private IEnumerator GroundAttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            GroundAttackFire();
            yield return new WaitForSeconds(groundAttackInterval);
        }
    }

    private IEnumerator AerialAttackRoutine()
    {
        while (true)
        {
            AerialAttack();
            yield return new WaitForSeconds(aerialAttackInterval);
        }
    }

    private void GroundAttackFire()
    {
        if (isGroundEnemy)
        {
            groundAttackObject.transform.position = attackPivot.position;
            groundAttackObject.transform.rotation = attackPivot.rotation;
            groundAttackObject.SetActive(true);

            AttackAnimation();
            GroundAttackObject script = groundAttackObject.GetComponent<GroundAttackObject>();
            script.StartMove();
        }
    }

    private void AerialAttack()
    {
        if (!isGroundEnemy)
        {
            Vector3 playerPos = playerTransform.position;
            Vector3 EnemyPos = attackPivot.position;

            Vector3 dirToPlayer = (playerPos - EnemyPos).normalized;

            float randomAngle = Random.Range(-angleRange, angleRange);
            Vector3 rotatedDic = Quaternion.Euler(0, randomAngle, 0) * dirToPlayer;

            float randomY = Random.Range(minYOffset, maxYOffset);

            Vector3 spawnPos = attackPivot.position + Vector3.up * randomY;

            GameObject bullet = poolManager.GetPoolObject(bulletObjectPoolKey, transform);
            if (bullet == null)
            {
                Debug.Log("bullet not found");
                return;
            }

            bullet.transform.position = spawnPos;
            bullet.transform.rotation = attackPivot.rotation;

            if (string.IsNullOrEmpty(bulletObjectPoolKey))
            {
                Debug.Log("bullet object pool key not set");
                return;
            }

            if (poolManager == null)
            {
                Debug.Log("poolManager not set");
                return;
            }

            AirBullet script = bullet.GetComponent<AirBullet>();
            script.Launch(poolManager, bulletObjectPoolKey, rotatedDic);
        }
    }

    public void AttackAnimation()
    {
        animator.SetTrigger(isAttack);
    }
}
