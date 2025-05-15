using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItemSpawner : MonoBehaviour
{
    [Header("Ÿ�� ����")]
    public Transform target; // Ÿ�� ����
    [SerializeField] private float spawnOffset = 15f; // Ÿ��(�÷��̾�)�� ���� ��ġ�� �Ÿ�

    [Header("����Ʈ ������ ����Ʈ�� ���� ����")]
    public GameObject questItems;
    [SerializeField] private float spawn_Y = -1f;
    [SerializeField] private float spawnInterval = 10f; // ������ ���� ����

    private float spawnTimer = 0f; // ���� ���� ������

    void Update()
    {
        if (!QuestManager.onQuest) return;

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && !GameSystem.hasFinished) // ���� ó��
        {
            SpawnQuestItem();
            spawnTimer = 0f;
        }
    }

    private void SpawnQuestItem() // ����Ʈ�� �ش��ϴ� ������ ����
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
