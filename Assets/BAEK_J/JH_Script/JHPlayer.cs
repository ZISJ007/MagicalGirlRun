using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class JHPlayer : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    // 점프 설정
    bool isJump = false;
    public float jumpForce = 10; // 점프 높이

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isJump = true;
        }
    }
    private void FixedUpdate()
    {

        Vector3 velocity = _rigidbody.velocity;

        if (isJump)
        {
            velocity.y += jumpForce;
            isJump = false;
        }
        _rigidbody.velocity = velocity;
    }
}
