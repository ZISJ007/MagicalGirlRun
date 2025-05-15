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
    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;

    private void Start()
    {
        if (groundPoolKey != null)
        {
            StartCoroutine(SpawnObstacle(groundPoolKey, new Vector3(15, -5, 0)));
        }

        if (aerialPoolKey != null)
        {
            StartCoroutine(SpawnObstacle(aerialPoolKey, new Vector3(15, 5, 0)));
        }
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

            if (GameSystem.hasFinished)
                break;
        }
    }
}