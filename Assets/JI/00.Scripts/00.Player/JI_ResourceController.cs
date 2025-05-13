using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JI_ResourceController : MonoBehaviour
{
    private JI_PlayerController playerController;
    private JI_PlayerStats playerStats;

    [Header("장애물 데미지")]
    public int obstacleDamage = 0;
    [Header("무적 지속시간(초)")]
    public float invincibilityDuration = 1.0f;
    [Header("플레이어 무적 상태")]
    private bool isInvincible = false;
    private Coroutine invincibilityCoroutine;
    

    private void Awake()
    {
        playerController = GetComponent<JI_PlayerController>();
        playerStats = GetComponent<JI_PlayerStats>();
    }
    private void Start()
    {

    }
    private void Update()
    {
        if (GameSystem.hasFinished)
        {
            isInvincible = true;
            transform.position += Vector3.right * GameSystem.speed * Time.deltaTime;
        }
    }

    public void Heal(int amount)
    {
        playerStats.Heal(amount);  // 플레이어 체력 회복
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return; // 무적 상태일 때는 return
        playerController.HandleDamage(invincibilityDuration);  // 플레이어 피해 애니메이션 매서드 호출
        playerStats.TakeDamage(amount);  // 플레이어 체력 감소
        StartInvincibility(invincibilityDuration); //데미지 받을 때 무적 상태 시작
        if (playerStats.CurrentHp <= 0)  // 체력이 0 이하일 때 사망
        {
            Death();
        }
    }
    public void TakeHeal(int amount) // 하트 회복
    {
        if (playerStats.CurrentHp >= playerStats.MaxHp) return; // 최대 체력 이상일 때는 return
        playerStats.Heal(amount);  // 플레이어 체력 회복
    }
    public void StartInvincibility(float duration)
    {
        // 기존 코루틴이 돌고 있으면 중지
        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine); //코루틴 중지 > 무적 시작 시 중복 방지
        }
        invincibilityCoroutine = StartCoroutine(InvincibilityCoroutine(duration)); //코루틴 시작
    }

    private IEnumerator InvincibilityCoroutine(float duration) //무적 코루틴
    {
        isInvincible = true; // 무적 상태
        yield return new WaitForSeconds(duration); // 지정한 초(duration) 만큼 대기 (무적 지속시간)
        isInvincible = false; // 무적 해제
        invincibilityCoroutine = null; // 코루틴 종료
    }
    private void Death()  // 플레이어 사망 
    {
        playerController.HandleDeath();  // 플레이어 사망 처리
        enabled = false;  // JI_ResourceController 비활성화
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Obstacle"))  // 장애물과 충돌했을 때
        {
            TakeDamage(obstacleDamage);  // 체력 감소
        }
    }
}