using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemSpawner : MonoBehaviour
{
    [Header("타겟 세팅")]
    public Transform target; // 타겟 설정
    [SerializeField] private float spawnOffset = 15f; // 타겟(플레이어)와 스폰 위치의 거리

    [Header("퀘스트 아이템 리스트와 스폰 설정")]
    public GameObject questItems;
    [SerializeField] private float spawn_Y = -1f;
    [SerializeField] private float spawnInterval = 10f; // 아이템 스폰 간격

    private float spawnTimer = 0f; // 스폰 간격 측정용

    void Update()
    {
        if (!QuestManager.onQuest) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && !GameSystem.hasFinished) // 스폰 처리
        {
            SpawnQuestItem();
            spawnTimer = 0f;
        }
    }

    private void SpawnQuestItem() // 퀘스트에 해당하는 아이템 스폰
    {
        if (questItems == null) return;

        if (QuestManager.onQuest)
        {
            Vector3 spawnPosition = new Vector3
            ((target.position.x) + spawnOffset, spawn_Y, 0f);
            Instantiate(questItems, spawnPosition, Quaternion.identity);
        }
    }
}
