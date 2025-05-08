using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어 물리
    Rigidbody2D _rigidbody;

    // 플레이어 이동 거리
    public float moveDistance;
    private Vector2 startPosition;

    // 플레이어 이동 속도, 점프 높이
    public int speed = 3; // 이동 속도
    public float jumpForce = 5; // 점프 높이

    void Start()
    {
        startPosition = transform.position; // 이동 시작 위치 기억
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        moveDistance = Vector2.Distance(startPosition, transform.position); // 이동 거리 측정
    }
    private void FixedUpdate()
    {

    }
}
