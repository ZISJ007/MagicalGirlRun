using UnityEngine;

public class JI_PlayerController : MonoBehaviour
{
    [Header("점프 설정")]
    [SerializeField] private float jumpForce = 5f;

    [Header("점프 횟수 설정")]
    [SerializeField] private int maxJumpCount = 2;    // 최대 점프 횟수
    private int jumpCount = 0;                        // 현재까지 사용한 점프 횟수

    [Header("지면 감지 설정")]
    [SerializeField] private Transform groundCheck;   // 바닥 체크용 EmptyObject
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;   // "Ground" 레이어 지정

    [Header("슬라이드 설정")]
    public GameObject slideObject;               // 슬라이드 오브젝트
    private bool isSlide = false;                    // 슬라이드 요청 플래그

    private Rigidbody2D rb;
    private bool isJump = false;                     // 점프 요청 플래그

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            isJump = true;
        }

        if (IsGrounded())
        {
            jumpCount = 0;
        }
        HandleSlide();
    }

    void FixedUpdate()
    {
        HandleJump();
    }

    private void HandleJump()
    {
        if (isJump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            isJump = false;
        }
    }

    private bool IsGrounded()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            (Vector2)groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
        return hit != null; //hit가 null이 아니면 ture로 반환해 바닥에 닿아있음 그렇지 않다면 null이면 false로 공중 에 떠있음  
    }

    private void HandleSlide()
    {
        if (Input.GetKey(KeyCode.A) && IsGrounded() && !isJump)
        {
            slideObject.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else
        {
            slideObject.transform.rotation = Quaternion.identity;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
