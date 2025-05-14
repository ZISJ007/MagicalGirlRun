using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject backPrefab; // 스폰할 배경 프리팹
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private float spawnY;
    [SerializeField] private float spawnX;
    [SerializeField] private int backCount = 3;

    private float backWidth;
    private List<Back> backList = new List<Back>();


    private void Start()
    {
        backWidth = backPrefab.GetComponent<SpriteRenderer>().bounds.size.x;

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0);
        for (int i = 0; i < backCount; i++)
        {
            var back = SpawnBack(spawnPos);
            spawnPos.x += backWidth;
        }
    }

    private void Update()
    {
        while (backList.Count < backCount)
        {
            Vector3 spawnPos = GetRightMostBackPos() + Vector3.right * backWidth;
            SpawnBack(spawnPos);
        }

        CleanupOffscreenBack();
    }

    private Back SpawnBack(Vector3 pos)
    {
        GameObject newBack = Instantiate(backPrefab, pos, Quaternion.identity);
        Back back = newBack.GetComponent<Back>();
        back.Init(scrollSpeed);
        backList.Add(back);
        return back;
    }

    private Vector3 GetRightMostBackPos()
    {
        Vector3 rightMost = backList[0].transform.position;
        foreach (Back back in backList)
        {
            if (back.transform.position.x > rightMost.x)
            {
                rightMost = back.transform.position;
            }
        }

        return rightMost;
    }

    private void CleanupOffscreenBack()
    {
        for (int i = backList.Count - 1; i >= 0; i--)
        {
            if (backList[i].transform.position.x <= -30f)
            {
                Destroy(backList[i].gameObject);
                backList.RemoveAt(i);
            }
        }
    }
}