using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacleSpawner
{
    void Spawn();
    void Update();
}

public abstract class BaseObstacleSpawner : MonoBehaviour
{
    
    [Header("Obstacle Info")]
    protected float moveSpeed; //좌측으로 이동하는 속도(맵의 이동속도를 가져와서 작업하면 될것)
    protected float spawnDelay; //오브젝트가 스폰하는 주기
    protected GameObject obstaclePrefab;
    
    public abstract void Spawn();
    public abstract void Update();
}
