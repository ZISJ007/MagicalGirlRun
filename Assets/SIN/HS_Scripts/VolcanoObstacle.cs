using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoObstacle : ObstacleController
{
    private Animator animator;



    // 애니메이션 이름 배열
    public string[] animationClips = { "TestVol_0", "TestVol_1", "TestVol_2" };

    // 각 애니메이션 재생 시간 설정 (초 단위)
    public float[] clipDurations = { 1f, 1f, 1f }; //해당 에니메이션 클립의 유지 시간
    public float[] speedMultipliers = { 1f, 0.2f, 1f }; //에니메이션 클립의 속도 조절

    private int currentIndex = 0;     // 현재 재생 중인 애니메이션 인덱스
    private float timer = 0f;         // 현재 애니메이션 경과 시간

    void Start()
    {
        animator = GetComponent<Animator>();

        // 첫 애니메이션 재생
        if (animationClips.Length > 0)
        {
            animator.Play(animationClips[currentIndex]);
        }
    }

    void Update()
    {
        //base.Update();  // Move() 기능 유지

        if (animationClips.Length == 0 || clipDurations.Length != animationClips.Length)
            return;

        timer += Time.deltaTime;

        if (timer >= clipDurations[currentIndex])
        {
            // 다음 애니메이션으로 넘어가기
            currentIndex = (currentIndex + 1) % animationClips.Length;
            animator.Play(animationClips[currentIndex]);

            timer = 0f;  // 타이머 초기화
        }
    }

    protected override void OnPlayerHit()
    {
        Debug.Log("플레이어가 화산에 닿았습니다!");
    }
}