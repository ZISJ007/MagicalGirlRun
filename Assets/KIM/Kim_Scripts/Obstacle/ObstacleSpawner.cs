using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    private PoolManager poolManager;
    [SerializeField] private string groundPoolKey = "Ground";
    [SerializeField] private string aerialPoolKey = "Aerial";
    [SerializeField] private string testPoolKey = "test";
    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;

    private void Start()
    {
        StartCoroutine(SpawnObstacle(groundPoolKey, new Vector3(11, -5, 0)));
        StartCoroutine(SpawnObstacle(aerialPoolKey, new Vector3(10, 5, 0)));
        StartCoroutine(SpawnObstacle(testPoolKey, new Vector3(10, 5, 0)));
    }

    private void Awake()
    {
        poolManager = FindObjectOfType<PoolManager>();
        if (poolManager == null)
        {
            Debug.LogError("PoolManager not found");
        }
        poolManager.InitializePool(this.transform);
    }

    private IEnumerator SpawnObstacle(string _poolKey, Vector3 _spawnPos)
    {
        float firstSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(firstSpawnDelay);

        while (true) //게임 종료 조건 가져외서 false
        {
            GameObject obj = poolManager.GetPoolObject(_poolKey, this.transform);
            if (obj != null)
            {
                obj.transform.localPosition = _spawnPos;
            }
            else
            {
                Debug.Log("Pool object not found");
            }

            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}