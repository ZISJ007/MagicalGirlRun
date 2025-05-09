using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]private GameObject groundObstaclePrefab; 
    [SerializeField]private GameObject aerialObstaclePrefab; 
    [SerializeField]private float minSpawnDelay;
    [SerializeField]private float maxSpawnDelay;
    private void Start()
    {
        StartCoroutine(SpawnAerialObstacle());
        StartCoroutine(SpawnGroundObstacle());
    }

    private IEnumerator SpawnGroundObstacle()
    {
        float firstSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(firstSpawnDelay);
        
        while (true) //게임 종료 조건 가져외서 false
        {
            Instantiate(groundObstaclePrefab, new Vector3(11, -5, 0), Quaternion.identity);
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private IEnumerator SpawnAerialObstacle()
    {
        float firstSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
        yield return new WaitForSeconds(firstSpawnDelay);
        
        while (true) //게임 종료 조건 가져외서 false
        {
            Instantiate(aerialObstaclePrefab, new Vector3(11, 5, 0), Quaternion.identity);
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);
            
        }
    }
}
