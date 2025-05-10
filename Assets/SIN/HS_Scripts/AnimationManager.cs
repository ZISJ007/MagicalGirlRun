using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayRun()
    {
        animator.Play("Blue_Run");
        Debug.Log("달리기 애니메이션 실행");
    }

    public void PlayJump()
    {
        animator.Play("Blue_Jump");
        //animator.speed = 2f;
        Debug.Log("점프 애니메이션 실행");
    }

    public void PlaySlide()
    {
        animator.Play("Black_Sliding");
        Debug.Log("슬라이드 애니메이션 실행");
    }
}
