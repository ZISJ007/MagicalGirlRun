using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Build.Content;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // 스테이지 퀘스트 클리어 상태
    public static bool[] isQuestClear = new bool[3];
    // 현재 스테이지 값
    private int stage = StageManager.isStage;
    // 퀘스트 아이템 요구량
    private int demand = 0;
    // 아이템 보유량
    private int reserves = 0;

    void Start()
    {
        GiveQuest();
    }

    private void Update()
    {

    }

    private void GiveQuest() // 스테이지에 따른 퀘스트 아이템 요구량 설정
    {
        demand = 0;
        reserves = 0;

        if (stage == 1)
        {
            Quest_1();
        }
        else if (stage == 2)
        {
            Quest_2();
        }
        else if (stage == 3)
        {
            Quest_3();
        }
        else
        {
            Debug.Log("존재하지 않는 스테이지 입니다.");
        }
    }

    private void Quest_1()
    {
        demand = 10;
    }
    private void Quest_2()
    {
        demand = 15;
    }
    private void Quest_3()
    {
        demand = 20;
    }

    private void QuestClear() // 퀘스트 클리어 상태 활성화
    {
        if (stage == 1)
        {
            isQuestClear[0] = true;
        }
        else if (stage == 2)
        {
            isQuestClear[1] = true;
        }
        else if (stage == 3)
        {
            isQuestClear[2] = true;
        }
        else
        {
            return;
        }
    }
}
