using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;        // 점프할 때 위로 가해지는 힘
    public float slideDuration = 3f;     // 슬라이드 상태 유지 시간

    private Rigidbody2D rb;              // 캐릭터 제어용
    private Animator animator;           // 캐릭터의 애니메이션을 제어
    private bool isGrounded = true;      // 캐릭터가 바닥에 닿아 있는지 여부
    private bool isSliding = false;      // 현재 슬라이드 중인지 여부

    private AnimationManager animationManager;

    void Awake()
    {
        // 컴포넌트 초기화
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // 자식 오브젝트(Model)에서 AnimationManager를 찾음
        animationManager = GetComponentInChildren<AnimationManager>();
        //animationManager = GetComponent<AnimationManager>();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isSliding)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded && !isSliding)
        {
            StartCoroutine(Slide());
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        isGrounded = false;

        animationManager.PlayJump();
    }

    System.Collections.IEnumerator Slide()
    {
        isSliding = true;

        animationManager.PlaySlide();

        yield return new WaitForSeconds(slideDuration);

        isSliding = false;

        // 달리기 애니메이션 재생
        animationManager.PlayRun();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥과 충돌했을 경우
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (!isSliding)
                animationManager.PlayRun();
        }

        // 장애물과 충돌했을 경우
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("장애물에 충돌!");
        }
    }

}