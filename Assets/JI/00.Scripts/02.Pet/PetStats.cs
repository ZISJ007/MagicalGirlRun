using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStats : MonoBehaviour
{
    [Header("펫 플러스 스탯")]
    [SerializeField] private int plusJumpForce;
    [SerializeField] private int plusJumpCount;
    [Header("펫 마이너스 스탯")]
    [SerializeField] private int minusJumpForce;
    [SerializeField] private int minusJumpCount;
    [Header("펫 체력 증가 스탯")]
    [SerializeField] private int plusHp;
    private JI_PlayerController playerController;
    private JI_PlayerStats playerStats;
    private bool applied = false;
    private void Awake()
    {
        playerController = GetComponentInParent<JI_PlayerController>();
        playerStats = GetComponentInParent<JI_PlayerStats>();

    }
    private void Start()
    {
        if (playerController == null || applied) return; // 부모 오브젝트에 JI_PlayerController가 없거나 이미 적용된 경우 return
        PlusJump();
        MinusJump();
        PlusHp();
        applied = true;// 증가 적용 완료 
    }
    private void PlusJump()
    {
        playerController.AddMaxJumpCount(plusJumpCount);// 점프 횟수 증가
        playerController.AddJumpFoce(plusJumpForce);// 점프 힘 증가
    }
    private void MinusJump()
    {
        playerController.SubtractJumpCount(minusJumpCount);// 점프 횟수 증가
        playerController.SubtractJumpFoce(minusJumpForce);// 점프 힘 증가
    }
    private void PlusHp()
    {
        playerStats.SetMaxHp(plusHp); // 최대 체력 증가
        var heartsUI = FindObjectOfType<JI_HeartsUI>();
        if (heartsUI != null)
        {
            heartsUI.CreateHearts();
            heartsUI.UpdateHearts();
        }
    }
}
