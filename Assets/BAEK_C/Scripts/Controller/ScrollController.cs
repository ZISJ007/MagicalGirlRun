using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public Scrollbar scrollbar;          //��ũ�ѹ� 
    public Transform character;          
    public float gameLength = 100f;      //���� ���� ���� (��ũ�ѹ� ��ü ���� �ϴ� 
    public float moveSpeed = 1f;   //���� ���� �ӵ� (ĳ������ �̵� �ӵ������� �������Ű��ƿ�)
    private float characterProgress = 0f; //ĳ���� ���൵?

    void Update()
    {
        //���� ���࿡ ���� ĳ���Ͱ� �̵�
        //characterProgress = ?? // ���� �ӵ��� ���� �̵�

        //���� ���̸� �Ѿ�� �ʰ�
       //characterProgress = 

        //ĳ���� ���൵�� ���� ����
        //scrollbar.value = characterProgress;

       
    }
}

