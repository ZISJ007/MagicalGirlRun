using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    // 현재 스테이지를 구분
    public static int isStage = 0; // 현재 스테이지(1~4)

    // 스테이지 점수
    public static int stageScore = 0;
    // 목적지까지의 거리
    private float destination = 0; 
    // 플레이어 참조
    private JHPlayer player;

    // 퀘스트 클리어 상태로 Stage 클리어시 보스 등장

    private void Start()
    {
        Time.timeScale = 1f;
        player = FindObjectOfType<JHPlayer>();
        DestinationSetting();
    }

    void Update()
    {
        if (player.moveDistance >= destination) // 목적지까지 도달
        {
            Finish();
        }
    }

    private void Finish() // 퀘스트를 클리어 했다면 보스 소환
    {
        if (QuestManager.isQuestClear[0] == true)
        {
            //SpawnBoss_1();
        }
        else if (QuestManager.isQuestClear[1] == true)
        {
            //SpawnBoss_2();
        }
        else if (QuestManager.isQuestClear[2] == true)
        {
            //SpawnBoss_3();
        }
        else
        {
            StageClear();
        }
    }

    private void StageClear() // 결과창 
    {
        Time.timeScale = 0f;
    }

    private void DestinationSetting() // 목적지 거리 설정
    {
        if (isStage == 1)
        {
            destination = 5000;
        }
        else if (isStage == 2)
        {
            destination = 6000;
        }
        else if (isStage == 3)
        {
            destination = 7000;
        }
        else
        {
            Debug.Log("존재하지 않는 스테이지 입니다.");
            return;
        }
    }
}
