using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;        // ������ �� ���� �������� ��
    public float slideDuration = 3f;     // �����̵� ���� ���� �ð�

    private Rigidbody2D rb;              // ĳ���� �����
    private Animator animator;           // ĳ������ �ִϸ��̼��� ����
    private bool isGrounded = true;      // ĳ���Ͱ� �ٴڿ� ��� �ִ��� ����
    private bool isSliding = false;      // ���� �����̵� ������ ����

    private AnimationManager animationManager;

    void Awake()
    {
        // ������Ʈ �ʱ�ȭ
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // �ڽ� ������Ʈ(Model)���� AnimationManager�� ã��
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

        // �޸��� �ִϸ��̼� ���
        animationManager.PlayRun();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڰ� �浹���� ���
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            if (!isSliding)
                animationManager.PlayRun();
        }

        // ��ֹ��� �浹���� ���
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("��ֹ��� �浹!");
        }
    }

}