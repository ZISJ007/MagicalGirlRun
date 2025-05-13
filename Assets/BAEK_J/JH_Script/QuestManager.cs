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
    [Header("현재 스테이지")]
    // 스테이지 퀘스트 상태
    public static bool isQuestClear;
    public static bool onQuest;

    [Header("요구량, 보유량")]
    [SerializeField] private int demand = 5;
    [SerializeField] private int reserves = 0;

    void Start()
    {
        Debug.Log($"퀘스트 활성화");
        onQuest = true;
        isQuestClear = false;
    }

    public void GetQuestItem() // 퀘스트 아이템 습득
    {
        reserves ++;

        Debug.Log($"퀘스트 아이템 획득 총 {reserves}개");

        if (onQuest && demand <= reserves)
        {
            QuestComplete();
        }
    }

    private void QuestComplete() // 퀘스트 완료
    {
        Debug.Log($"퀘스트 완료");
        isQuestClear = true;
        onQuest = false;
        reserves = 0;
    }
}
