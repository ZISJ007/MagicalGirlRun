using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> items; // 아이템 리스트

    public Transform target; // 타겟 설정
    public float spawnOffset = 15f; // 타겟(플레이어)와 스폰 위치의 거리

    [SerializeField] private float coinInterval = 6f; // 코인 스폰 간격
    private float coinTimer = 0f; // 스폰 간격 측정용
    public int coinCount = 15; // 한번에 소환하는 코인 수

    [SerializeField] private float itemInterval = 30f; // 아이템 스폰 간격
    private float itemTimer = 0f; // 스폰 간격 측정용

    private void Start()
    {
        SpawnCoin();
    }
    void Update()
    {
        if (target == null) return;

        coinTimer += Time.deltaTime;
        itemTimer += Time.deltaTime;

        if (target != null)
        {
            if (coinTimer >= coinInterval) // 스폰 처리
            {
                SpawnCoin();
                coinTimer = 0f;
            }

            if (itemTimer >= itemInterval)
            {
                SpawnItem();
                itemTimer = 0f;
            }
        }
    }
    private void SpawnCoin() // 코인 스폰
    {
        for (int i = 0; i < coinCount; i++)
        {
            Vector3 spawnPosition = new Vector3
            ((target.position.x + (i * 2)) + spawnOffset, -2.5f, 0f);
            Instantiate(items[0], spawnPosition, Quaternion.identity);
        }
    }
    private void SpawnItem() // 아이템 스폰
    {
        Vector3 spawnPosition = new Vector3
            ((target.position.x ) + spawnOffset, -1f, 0f);

        int ran = Random.Range(1, items.Count);
        Instantiate(items[ran], spawnPosition, Quaternion.identity);
    }

}
