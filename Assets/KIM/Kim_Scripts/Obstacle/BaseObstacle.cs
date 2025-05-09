using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacleSpawner
{
    void Spawn();
    void Update();
    void YMove();
}

public abstract class BaseObstacle : MonoBehaviour
{
    
    [Header("Obstacle Info")]
    [SerializeField]private float xMoveSpeed; //좌측으로 이동하는 속도(맵의 이동속도를 가져와서 작업하면 될것)
    
    public abstract void Spawn();

    protected virtual void Update()
    {
        XMove();
    }

    private void XMove()
    {
        transform.Translate(Vector2.left * xMoveSpeed * Time.deltaTime);
    }
     protected abstract void YMove();
    
    
}
