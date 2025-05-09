using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public Scrollbar scrollbar;          //스크롤바 
    public Transform character;          
    public float gameLength = 100f;      //게임 진행 길이 (스크롤바 전체 길이 일단 100으로 근데 땡기는건 70까지 가능?>?)
    public float moveSpeed = 1f;   //게임 진행 속도 (캐릭터의 이동 속도에따라 빨라질거같아요)
    private float characterProgress = 0f; //캐릭터 진행도?

    void Update()
    {
        //게임 진행에 맞춰 캐릭터가 이동
        //characterProgress = ?? // 진행 속도에 따라 이동

        //게임 길이를 넘어가지 않게
       //characterProgress = 

        //캐릭터 진행도에 맞춰 설정
        //scrollbar.value = characterProgress;

       
    }
}

