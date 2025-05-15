using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GroundSpawner : MonoBehaviour
{
    [Header("크라운드 스폰 설정")] [SerializeField]
    private GameObject groundPrefab;

    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private int groundCount = 3;
    [SerializeField] private float spawnY;

    private List<Ground> groundList = new List<Ground>();
    private float groundWidth;

    private void Start()
    {
        groundWidth = groundPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        Vector3 spawnPos = new Vector3(transform.position.x - 3, spawnY, 0);
        for (int i = 0; i < groundCount; i++)
        {
            var ground = SpawnGround(spawnPos);
            spawnPos.x += groundWidth;
        }
    }

    private void Update()
    {
        while (groundList.Count < groundCount)
        {
            Vector3 spawnPos = GetRightMostGroundPos() + Vector3.right * groundWidth;
            SpawnGround(spawnPos);
        }
    }

    private Ground SpawnGround(Vector3 _pos)
    {
        GameObject newGround = Instantiate(groundPrefab, _pos, Quaternion.identity);
        Ground ground = newGround.GetComponent<Ground>();
        ground.Init(scrollSpeed, this);
        groundList.Add(ground);
        return ground;
    }

    private Vector3 GetRightMostGroundPos()
    {
        Vector3 rightMost = groundList[0].transform.position;
        foreach (var ground in groundList)
        {
            if (ground.transform.position.x > rightMost.x)
            {
                rightMost = ground.transform.position;
            }
        }

        return rightMost;
    }

    public void UnregisterGround(Ground ground)
    {
        if (groundList.Contains(ground))
            groundList.Remove(ground);
    }
}