using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ItemSpawner : MonoBehaviour
{
    [Header("타겟 세팅")]
    public Transform target; // 타겟 설정
    [SerializeField] private float spawnOffset = 15f; // 타겟(플레이어)와 스폰 위치의 거리

    [Header("아이템 리스트와 스폰 설정")]
    public List<GameObject> items;
    [SerializeField] private float spawn_Y = -3f;
    [SerializeField] private int spawnCount = 15; // 한번에 소환하는 아이템 수
    [SerializeField] private float spawnInterval = 0.3f; // 아이템 스폰 간격

    public float spawnTimer = 0f; // 
    private int itemTurn = 0; // 아이템 스폰 주기 저장

    private void Start()
    {
    }

    private void Update()
    {
        spawnTimer += GameSystem.speed * Time.deltaTime;

        if (spawnTimer >= spawnInterval && !GameSystem.hasFinished)
        {
            Spawnitem();
            spawnTimer = 0f; // 누적 거리 초기화
        }
    }

    private void Spawnitem() // 아이템 스폰
    {
        itemTurn++;

        Vector3 spawnPosition = new Vector3
        ((target.position.x + spawnOffset), spawn_Y, 0f);

        if (itemTurn % 30 == 0) // 매 2번째 생성 주기 마지막엔 특수 아이템 스폰
        {
            int random = Random.Range(1, items.Count);
            Instantiate(items[random], spawnPosition, Quaternion.identity);
        }
        else // 그 외 코인 스폰
        {
            Instantiate(items[0], spawnPosition, Quaternion.identity);
        }
    }
}
