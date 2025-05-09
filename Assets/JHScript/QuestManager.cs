using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int isQuestClear = 0; // 1~3스테이지 퀘스트 클리어 상태

    // 퀘스트 요구량
    private int demand = 0;
    // 아이템 보유량
    private int reserves = 0;

    // 키 소유량
    public int keyCount;

    // 스테이지 매니저 참조
    StageManager stageManager;

    void Start()
    {

    }

    private void GetQuest()
    {
        for (int i = 0; i <.Length; i++)
        {

        }
    } 
}
