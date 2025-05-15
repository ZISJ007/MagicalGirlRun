using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStats : MonoBehaviour
{
    [Header("�� �÷��� ����")]
    [SerializeField] private int plusJumpForce;
    [SerializeField] private int plusJumpCount;
    [Header("�� ���̳ʽ� ����")]
    [SerializeField] private int minusJumpForce;
    [SerializeField] private int minusJumpCount;
    [Header("�� ü�� ���� ����")]
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
        if (playerController == null || applied) return; // �θ� ������Ʈ�� JI_PlayerController�� ���ų� �̹� ����� ��� return
        PlusJump();
        MinusJump();
        PlusHp();
        applied = true;// ���� ���� �Ϸ� 
    }
    private void PlusJump()
    {
        playerController.AddMaxJumpCount(plusJumpCount);// ���� Ƚ�� ����
        playerController.AddJumpFoce(plusJumpForce);// ���� �� ����
    }
    private void MinusJump()
    {
        playerController.SubtractJumpCount(minusJumpCount);// ���� Ƚ�� ����
        playerController.SubtractJumpFoce(minusJumpForce);// ���� �� ����
    }
    private void PlusHp()
    {
        playerStats.SetMaxHp(plusHp); // �ִ� ü�� ����
        var heartsUI = FindObjectOfType<HeartsUI>();
        if (heartsUI != null)
        {
            heartsUI.CreateHearts();
            heartsUI.UpdateHearts();
        }
    }
}
