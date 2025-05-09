using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> items; // 아이템 리스트

    public Transform target; // 타겟 설정
    private float spawnOffset = 15f; // 타겟(플레이어)와 스폰 위치의 거리

    private float spawnInterval = 1f; // 스폰 간격
    private float spawnTimer = 0f;

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
        Vector3 spawnPosition = new Vector3
        (target.position.x + spawnOffset, -2f, 0f);

        int num = Random.Range(0, 4);
        Instantiate(items[num], spawnPosition, Quaternion.identity);
    }

}
