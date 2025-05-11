using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterController : MonoBehaviour
{
    public float moveSpeed = 2f;           // 이동 속도
    public Transform target;              // 추적 대상

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    protected virtual void MoveTowardsTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnPlayerCatch();
        }
    }

    protected abstract void OnPlayerCatch(); // 각 몬스터마다 정의
}
