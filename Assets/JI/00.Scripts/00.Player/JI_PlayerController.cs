using System.Collections;
using UnityEngine;

public class JI_PlayerController : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] private float jumpForce = 10;

    [Header("���� Ƚ�� ����")]
    [SerializeField] private int maxJumpCount = 2;    // �ִ� ���� Ƚ��
    private int jumpCount = 0;                        // ������� ����� ���� Ƚ��

    [Header("���� ���� ����")]
    [SerializeField] private Transform groundCheck;   // �ٴ� üũ�� EmptyObject
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;   // "Ground" ���̾� ����
    private bool wasGrounded; // �ٴڿ� ��� �־����� ����
    [Header("���� ���� ����")]
    private bool isLanding = false; // ���� ����
    [Header("�����̵� ����")]
    public GameObject slideObject;               // �����̵� ������Ʈ

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������ ������Ʈ
    private Coroutine damageFlashCoroutine; //���� �ִϸ��̼� �ڷ�ƾ
    public bool isJump = false;
    public bool isStopped = false;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>(); // �ִϸ����� ������Ʈ ��������
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // ��������Ʈ ������ ������Ʈ ��������
        //DontDestroyOnLoad(this.gameObject);
        IsResume(); // ��ũ��Ʈ Ȱ��ȭ
    }
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            isJump = true;
            anim.SetBool("IsJump", true); // ���� �ִϸ��̼� Ʈ���� ����
        }

        bool nowGrounded = IsGrounded(); // �ٴڿ� ��� �ִ��� Ȯ��
        if (!wasGrounded && nowGrounded) //�ٴڿ� ����� ��
        {
            
            jumpCount = 0;//���� ī��Ʈ ����
        }
        wasGrounded = nowGrounded; // �ٴڿ� ��� �ִ��� ���� ������Ʈ

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
            float gameJumpForce = jumpForce + GameSystem.speed;

            rb.velocity = new Vector2(rb.velocity.x, gameJumpForce);
            jumpCount++;
            isJump = false;
            anim.SetBool("IsJump", false); // ���� �ִϸ��̼� Ʈ���� ����
        }
    }
    public void AddMaxJumpCount(int amount)
    {
        maxJumpCount += amount; // ���� Ƚ�� ����
    }
    public void AddJumpFoce(int amount)
    {
        jumpForce += amount; // ���� Ƚ�� ����
    }

    public void SubtractJumpFoce(int amount)
    {
        if(amount > jumpForce) return; // ���� ���� 0���� �۾����� �ʵ��� ����
        jumpForce -= amount; // ���� Ƚ�� ����
    }
    public void SubtractJumpCount(int amount)
    {
        if (amount > maxJumpCount) return; // ���� Ƚ���� 0���� �۾����� �ʵ��� ����
        maxJumpCount -= amount; // ���� Ƚ�� ����
    }
    private bool IsGrounded()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            (Vector2)groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
        return hit != null; //hit�� null�� �ƴϸ� ture�� ��ȯ�� �ٴڿ� ������� �׷��� �ʴٸ� null�̸� false�� ���� �� ������  
    }
    private bool IsLanding()
    {
        if (!IsGrounded() && !isLanding)
        {
            isLanding = true;
            anim.SetBool("IsLanding", true); // ���� �ִϸ��̼� Ʈ���� ����
        }
        else if (IsGrounded())
        {
            isLanding = false;
            anim.SetBool("IsLanding", false); // ���� �ִϸ��̼� Ʈ���� ����
        }
        return false;
    }
    private void HandleSlide()
    {
        if (Input.GetKey(KeyCode.LeftShift) && IsGrounded() && !isJump && !isLanding)
        {
            anim.SetBool("IsSlide", true); // �����̵� �ִϸ��̼� Ʈ���� ����         
        }
        else
        {
            anim.SetBool("IsSlide", false); // �����̵� �ִϸ��̼� Ʈ���� ����
        }
    }
    public void HandleDeath()
    {
        anim.SetTrigger("IsDead"); // ��� �ִϸ��̼� Ʈ���� ����   
        enabled = false;
    }
    public void HandleDamage(float duration)
    {
        if (damageFlashCoroutine != null) // �ڷ�ƾ�� �̹� ���� ���̶��(�ߺ� ����)
        {
            StopCoroutine(damageFlashCoroutine);  // ���� �ڷ�ƾ ����
            damageFlashCoroutine = null;          // ���� �ʱ�ȭ
        }
        // ���ο� �ڷ�ƾ ����
        damageFlashCoroutine = StartCoroutine(DamageFlashCoroutine(duration));
    }

    private IEnumerator DamageFlashCoroutine(float duration)
    {
        float endTime = Time.time + duration;// 0 + duration > endTime = 0 + duration
        bool IsDamged = false;
        while (Time.time < endTime) //(Time.time �� 0�������� ��� �ö�  <  endTime = 0 + duration)
        {
            //�� �ݺ����� IsDamged ���� ����
            IsDamged = !IsDamged;
            //isRed�� true�� clear, false�� ���� ������ ����
            spriteRenderer.color = IsDamged ? Color.clear : Color.white ; 
            yield return new WaitForSeconds(0.2f); //0.2�� ��� �������� ������
        }

        
        spriteRenderer.color = Color.white;//  ���� ������ ����
        damageFlashCoroutine = null;// �ڷ�ƾ ����
    }
    public void IsStop()
    {
        if (isStopped) return;
        isStopped = true;

        // �Է� ó�� ��ũ��Ʈ ��Ȱ��ȭ
        this.enabled = false;

        // �ִϸ����� �Ͻ� ����
        anim.enabled = false;

        // ������ٵ� ��ȣ �ۿ� ����
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
