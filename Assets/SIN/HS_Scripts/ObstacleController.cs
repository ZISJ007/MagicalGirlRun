using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Vector2 moveDirection = Vector2.left;

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerHit();
        }
    }

    protected abstract void OnPlayerHit(); 
}