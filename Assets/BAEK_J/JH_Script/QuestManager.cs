using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Build.Content;
using UnityEditor.SceneManagement;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int isStage = 0; // 현재 스테이지(1~4)

    // 스테이지 퀘스트 상태
    public static bool[] isQuestClear = new bool[3];
    public static bool[] onQuest = new bool[3];

    // 퀘스트 아이템 요구량
    [SerializeField] private int demand = 5;
    // 아이템 보유량
    [SerializeField] private int reserves = 0;

    void Start()
    {
        GetQuest();
    }

    private void GetQuest() // 스테이지에 따른 퀘스트 아이템 요구량 설정
    {
        for (int i = 0; i < onQuest.Length; i++)
        {
            if (!GameSystem.key[i] && isStage == i + 1)
            {
                onQuest[i] = true;
                Debug.Log($"{i + 1}스테이지 퀘스트 활성화");
            }
        }
    }

    public void GetQuestItem() // 퀘스트 아이템 습득
    {
        reserves ++;

        Debug.Log($"퀘스트 아이템 획득 총 {reserves}개");

        for (int i = 0; i < onQuest.Length; i++)
        {
            if (onQuest[i] && demand <= reserves)
            {
                QuestComplete(i);
            }
        }
    }

    private void QuestComplete(int num) // 퀘스트 완료
    {
        Debug.Log($"퀘스트 완료");
        isQuestClear[num] = true;
        onQuest[num] = false;
        reserves = 0;
    }
}
