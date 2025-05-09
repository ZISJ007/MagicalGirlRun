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
    [Header("공중 감지 설정")]
    private bool isLanding = false; // 착지 여부
    [Header("슬라이드 설정")]
    public GameObject slideObject;               // 슬라이드 오브젝트

    private Rigidbody2D rb;
    private Animator anim;
    private bool isJump = false;
    private bool isStopped = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // 애니메이터 컴포넌트 가져오기
        //DontDestroyOnLoad(this.gameObject);
        IsResume(); // 스크립트 활성화
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            isJump = true;
            anim.SetBool("IsJump", true); // 점프 애니메이션 트리거 설정
        }

        if (IsGrounded())
        {
            jumpCount = 0;
        }
        HandleSlide();
        IsLanding();
    }

    void FixedUpdate()
    {
        HandleJump();
    }

    private void HandleJump()
    {
        if (isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            isJump = false;
            anim.SetBool("IsJump", false); // 점프 애니메이션 트리거 해제
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
    private bool IsLanding()
    {
        if (!IsGrounded() && !isLanding)
        {
            isLanding = true;
            anim.SetBool("IsLanding", true); // 착지 애니메이션 트리거 설정
        }
        else if (IsGrounded())
        {
            isLanding = false;
            anim.SetBool("IsLanding", false); // 착지 애니메이션 트리거 설정
        }
        return false;
    }
    private void HandleSlide()
    {
        if (Input.GetKey(KeyCode.LeftShift) && IsGrounded() && !isJump && !isLanding)
        {
            anim.SetBool("IsSlide", true); // 슬라이드 애니메이션 트리거 설정
            slideObject.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else
        {
            anim.SetBool("IsSlide", false); // 슬라이드 애니메이션 트리거 설정
            slideObject.transform.rotation = Quaternion.identity;
        }
    }
    public void HandleDeath()
    {
        anim.SetTrigger("IsDead"); // 사망 애니메이션 트리거 설정   
        enabled = false;
    }
    public void IsStop()
    {
        if (isStopped) return;
        isStopped = true;

        // 입력 처리 스크립트 비활성화
        this.enabled = false;

        // 애니메이터 일시 정지
        anim.enabled = false;

        // 리지드바디 상호 작용 정지
        rb.simulated = false;
    }

    public void IsResume()
    {
        if (!isStopped) return;
        isStopped = false;

        this.enabled = true;
        anim.enabled = true;
        rb.simulated = true;
    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
