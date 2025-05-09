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

    [SerializeField]private float spawnInterval = 9f; // 스폰 간격
    private float spawnTimer = 0f;


    void Start()
    {
        SpawnItem();
    }

    void Update()
    {
        if (target == null) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval) // 스폰 처리
        {
            SpawnItem();
            spawnTimer = 0f;
        }
    }

    private void SpawnItem() // 아이템 스폰
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 spawnPosition = new Vector3
            (3 + (target.position.x + i)+ spawnOffset, -3.5f, 0f);
            Instantiate(items[0], spawnPosition, Quaternion.identity);
        }
    }
}
