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
   
    
    public abstract void Spawn();

    protected virtual void Update()
    {
        XMove();
    }

    private void XMove()
    {
        transform.Translate(Vector2.left * GameSystem.speed * Time.deltaTime);
    }
     protected abstract void YMove();
    
    
}
