using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Build.Content;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // 스테이지 퀘스트 상태
    public int onQuest = 0;
    public static bool[] isQuestClear = new bool[3];

    // 현재 스테이지 값
    private int stage = StageManager.isStage;
    // 퀘스트 아이템 요구량
    private int demand = 0;
    // 아이템 보유량
    private int reserves = 0;

    void Start()
    {
    }

    private void Update()
    {

    }
    private void GiveQuest() // 스테이지에 따른 퀘스트 아이템 요구량 설정
    {

    }
}
