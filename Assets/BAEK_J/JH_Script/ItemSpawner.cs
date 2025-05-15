
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Ÿ�� ����")]
    public Transform target; // Ÿ�� ����
    [SerializeField] private float spawnOffset = 15f; // Ÿ��(�÷��̾�)�� ���� ��ġ�� �Ÿ�

    [Header("������ ����Ʈ�� ���� ����")]
    public List<GameObject> items;
    [SerializeField] private float spawn_Y = -3f;
    [SerializeField] private int spawnCount = 15; // �ѹ��� ��ȯ�ϴ� ������ ��
    [SerializeField] private float spawnInterval = 0.3f; // ������ ���� ����

    public float spawnTimer = 0f; // 
    private int itemTurn = 0; // ������ ���� �ֱ� ����

    private void Update()
    {
        spawnTimer += GameSystem.speed * Time.deltaTime;

        if (spawnTimer >= spawnInterval && !GameSystem.hasFinished)
        {
            Spawnitem();
            spawnTimer = 0f; // ���� �Ÿ� �ʱ�ȭ
        }
    }

    private void Spawnitem() // ������ ����
    {
        itemTurn++;

        Vector3 spawnPosition = new Vector3
        ((target.position.x + spawnOffset), spawn_Y, 0f);

        if (itemTurn % 30 == 0) // �� 2��° ���� �ֱ� �������� Ư�� ������ ����
        {
            int random = Random.Range(1, items.Count);
            Instantiate(items[random], spawnPosition, Quaternion.identity);
        }
        else // �� �� ���� ����
        {
            Instantiate(items[0], spawnPosition, Quaternion.identity);
        }
    }
}
