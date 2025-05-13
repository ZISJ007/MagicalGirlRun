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
    private JI_PlayerController playerController;
    private bool applied = false;
    private void Awake()
    {
        playerController = GetComponentInParent<JI_PlayerController>();
    }
    private void Start()
    {
        if (playerController == null || applied) return; // 부모 오브젝트에 JI_PlayerController가 없거나 이미 적용된 경우 return
        PlusJump();
        MinusJump();
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
}
