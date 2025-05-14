using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Rigidbody2D rb;
    private float scrollSpeed;
    private GroundSpawner spawner;

    public void Init(float _scrollSpeed, GroundSpawner _groundSpawner)
    {
        scrollSpeed = _scrollSpeed;
        spawner = _groundSpawner;
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.left * scrollSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("ObjectDestoryZone"))
        {
            spawner.UnregisterGround(this);
            Debug.Log("Destory ground");
            Destroy(gameObject);
        }
    }
}