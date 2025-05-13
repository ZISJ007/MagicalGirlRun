using UnityEngine;

public class BackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject backPrefab;     // 스폰할 배경 프리팹
    [SerializeField] private float spawnInterval = 3f;  // 스폰 주기
    [SerializeField] private Vector3 spawnPosition = new Vector3(30f, 0f, 0f); // 스폰 위치
    [SerializeField] private float scrollSpeed = 2f;    // 스크롤 속도 (스폰된 객체에게 전달)

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnBack();
            timer = 0f;
        }
    }

    void SpawnBack()
    {
        GameObject newBack = Instantiate(backPrefab, spawnPosition, Quaternion.identity);

        // 스크롤 속도 전달
        var remover = newBack.GetComponent<BackRemover>();
        if (remover != null)
        {
            remover.ScrollSpeed = scrollSpeed;
        }
    }
}