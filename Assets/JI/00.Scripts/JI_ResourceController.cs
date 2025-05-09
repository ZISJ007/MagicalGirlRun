using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JI_ResourceController : MonoBehaviour
{
    private JI_PlayerController playerController;
    private JI_PlayerStats playerStats;

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
        if(Input.GetKeyDown(KeyCode.H))  // H키를 눌렀을 때 체력 회복
        {
            TakeDamage(1f);
        }
    }

    public void Heal(float amount)
    {
        playerStats.Heal(amount);  // 플레이어 체력 회복
    }

    public void TakeDamage(float amount)
    {
        playerStats.TakeDamage(amount);  // 플레이어 체력 감소
        if (playerStats.CurrentHp <= 0)  // 체력이 0 이하일 때 사망
        {
            Death();
        }
    }

    private void Death()  // 플레이어 사망 
    {
        playerController.HandleDeath();  // 플레이어 사망 처리
        enabled = false;  // JI_ResourceController 비활성화
    }
}