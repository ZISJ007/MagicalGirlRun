using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class JHPlayer : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    // 플레이어 이동 거리
    public float moveDistance;
    // 달리기 시작 위치
    public Vector2 startPosition;

    // 플레이어 체력
    public int life = 3;
    // 보유 중인 열쇠
    public bool key_1 = false;
    public bool key_2 = false;
    public bool key_3 = false;

    public float speed = 3; // 이동 속도
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
}
