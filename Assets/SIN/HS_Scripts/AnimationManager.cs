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
        animator.Play("Black_Run");
        Debug.Log("�޸��� �ִϸ��̼� ����");
    }

    public void PlayJump()
    {
        animator.Play("Black_Jump");
        animator.speed = 2f;
        Debug.Log("���� �ִϸ��̼� ����");
    }

    public void PlaySlide()
    {
        animator.Play("Black_Sliding");
        Debug.Log("�����̵� �ִϸ��̼� ����");
    }
}
