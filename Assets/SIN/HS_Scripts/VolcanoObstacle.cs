using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class VolcanoObstacle : ObstacleController
{
    public float eruptionDelay = 3f; // 몇 초 뒤에 분출할지
    private Animator animator;
    private bool hasErupted = false;
    private float timer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (hasErupted) return;

        timer += Time.deltaTime;

        if (timer >= eruptionDelay)
        {
            TriggerEruption();
        }
    }

    void TriggerEruption()
    {
        animator.SetTrigger("Explode");
        hasErupted = true;
    }

    protected override void OnPlayerHit()
    {
        // 플레이어가 화산과 충돌했을 때의 반응
        Debug.Log("플레이어가 화산에 닿았습니다!");
    }
}