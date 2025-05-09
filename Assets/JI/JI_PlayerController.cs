using UnityEngine;

public class JI_PlayerController : MonoBehaviour
{
    [Header("점프 설정")]
    [SerializeField] private float jumpForce = 5f;
    [Header("점프 횟수 설정")]
    [SerializeField] private int maxJumpCount = 2; // 최대 점프 횟수
    private int jumpCount = 0;  // 현재까지 사용한 점프 횟수
    [Header("지면 감지 설정")]
    [SerializeField] private Transform groundCheck; // 지면 감지 위치
    [SerializeField] private float groundCheckRadius = 0.1f; // 지면 감지 반경
    [SerializeField] private LayerMask groundLayer; // 지면 레이어

    private Rigidbody2D rb;
    private bool isJump = false;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 점프 입력 감지
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            isJump = true;
        }

        if (IsGrounded())
        {
            jumpCount = 0;
        }
    }

    void FixedUpdate()
    {
        HandleJump();
    }
    private void HandleJump()
    {
        if (isJump && jumpCount < maxJumpCount)//isJump가 true일때 실행
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++; // 현재 사용한 점프 횟수 증가
            isJump = false;
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(
            groundCheck.position,   // 검사할 원의 중심 위치
            groundCheckRadius,      // 원의 반지름
            groundLayer             // 체크할 레이어 마스크
        ) != null;                 // 겹치는 콜라이더가 있으면 true, 없으면 false
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
