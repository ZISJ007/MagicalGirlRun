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
    [SerializeField] private float coin_Y = -1.5f;
    [SerializeField] private int spawnCount = 15; // 한번에 소환하는 아이템 수
    [SerializeField] private float spawnInterval = 6; // 아이템 스폰 간격

    private float spawnTimer = 0f; // 스폰 간격 측정용
    private int itemTurn = 1; // 아이템 스폰 주기 저장

    private void Start()
    {
        Spawnitem();
    }

    void Update()
    {


        spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval) // 스폰 처리
            {
                Spawnitem();
                spawnTimer = 0f;
            }
    }

    private void Spawnitem() // 아이템 스폰
    {
        if (items == null) return;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = new Vector3
            ((target.position.x + (i * 2)) + spawnOffset, -2.5f, 0f);

            if (i == spawnCount - 1 && itemTurn == 2) // 매 2번째 생성 주기 마지막엔 특수 아이템 스폰
            {
                int random = Random.Range(1, items.Count);
                Instantiate(items[random], spawnPosition, Quaternion.identity);
            }
            else // 그 외 코인 스폰
            {
                Instantiate(items[0], spawnPosition, Quaternion.identity);
            }
        }

        if (itemTurn != 2) // 생성 주기 체크
        {
            itemTurn = 2;
        }
        else if (itemTurn != 1)
        {
            itemTurn = 1;
        }
    }
}
