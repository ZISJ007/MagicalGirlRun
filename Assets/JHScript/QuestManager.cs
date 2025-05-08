using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    // 스테이지 별 퀘스트 존재

    public bool[] stageQuest = new bool[3]; // 1~3스테이지 퀘스트 배열
    public bool[] isQuestClear = new bool[3]; // 1~3스테이지 퀘스트 클리어 상태

    // 퀘스트 조건에 부합하는 아이템을 일정 수량 획득하면 퀘스트 클리어
    // ( 필요한 변수 선언 )

    private int demand; // 퀘스트 요구량
    private int reserves; // 아이템 보유량

    public int keyCount; // 키 소유량

    // 스테이지 매니저 참조
    StageManager stageManager;

    void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
    }

    public void GetQuest()
    {
        for (int i = 0; i < stageManager.isStage.Length; i++)
        {
            if (stageManager.isStage[i] == true)
            {
                stageQuest[i] = true;
            }
        }
    }

    private void Quest_1()
    {
        //if ()
        {

        }
    }

    private void Quest_2()
    {
        //if ()
        {

        }
    }

    private void Quest_3()
    {
        //if ()
        {

        }
    }
}
