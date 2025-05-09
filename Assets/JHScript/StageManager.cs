using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    // 현재 스테이지를 구분
    public static int isStage = 0; // 현재 스테이지(1~4) 구분용 배열

    // 스테이지 점수
    public int stageScore = 0;

    // 게임이 진행중인지 구분
    public bool isrunnig = false;

    // 플레이어 참조
    private Player player;

    // 퀘스트 클리어 상태로 Stage 클리어시 열쇠 지급
    // 열쇠가 3개 모이면 Final Boss Stage 해금
    public int keyCount = 0;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        StageCheck();
    }

    void Update()
    {

    }

    public void StageClear(int Distance)  // Stage Clear 조건
    {
        for (int i = 0; i < isStage.Length; i++)
        {
            if (isStage[i] == true)
            {

            }
        }
    }

    public void StageCheck() // 현재 스테이지 체크
    {
        string sceneName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < isStage; i++)
        {
            if (sceneName == $"Stage_{i}")
            {
                isStage = i;
                break;
            }
        }
    }
}
