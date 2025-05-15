using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoObstacle : ObstacleController
{
    private Animator animator;



    // �ִϸ��̼� �̸� �迭
    public string[] animationClips = { "TestVol_0", "TestVol_1", "TestVol_2" };

    // �� �ִϸ��̼� ��� �ð� ���� (�� ����)
    public float[] clipDurations = { 1f, 1f, 1f }; //�ش� ���ϸ��̼� Ŭ���� ���� �ð�
    public float[] speedMultipliers = { 1f, 0.2f, 1f }; //���ϸ��̼� Ŭ���� �ӵ� ����

    private int currentIndex = 0;     // ���� ��� ���� �ִϸ��̼� �ε���
    private float timer = 0f;         // ���� �ִϸ��̼� ��� �ð�

    void Start()
    {
        animator = GetComponent<Animator>();

        // ù �ִϸ��̼� ���
        if (animationClips.Length > 0)
        {
            animator.Play(animationClips[currentIndex]);
        }
    }

    void Update()
    {
        //base.Update();  // Move() ��� ����

        if (animationClips.Length == 0 || clipDurations.Length != animationClips.Length)
            return;

        timer += Time.deltaTime;

        if (timer >= clipDurations[currentIndex])
        {
            // ���� �ִϸ��̼����� �Ѿ��
            currentIndex = (currentIndex + 1) % animationClips.Length;
            animator.Play(animationClips[currentIndex]);

            timer = 0f;  // Ÿ�̸� �ʱ�ȭ
        }
    }

    protected override void OnPlayerHit()
    {
        Debug.Log("�÷��̾ ȭ�꿡 ��ҽ��ϴ�!");
    }
}