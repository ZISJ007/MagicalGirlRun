using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> items; // 아이템 리스트

    public Transform target; // 타겟 설정
    public float spawnOffset = 5f; // 타겟(플레이어)와 스폰 위치의 거리
    public int spawnCount = 15;

    [SerializeField] private float coinInterval = 9f; // 스폰 간격
    [SerializeField] private float spawnTimer = 0f;

    void Update()
    {
        if (target == null) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= coinInterval) // 스폰 처리
        {
            SpawnCoin();
            spawnTimer = 0f;
        }
    }

    private void SpawnCoin() // 코인 스폰
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = new Vector3
            ((target.position.x + (i * 2)) + spawnOffset, -3.5f, 0f);
            Instantiate(items[0], spawnPosition, Quaternion.identity);
        }
    }
}
