using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    // 현재 스테이지를 구분
    public bool[] isStage = new bool[5]; // 현재 스테이지(1~4) 구분용 배열

    // 스테이지 클리어 거리
    public int clearDistance;

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

    public void StageClear(int Distance) 
    {
        if (clearDistance <= player.moveDistance)
        {
            // GameManager.Instance.Clear();
        }
    }

    public void StageCheck()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < isStage.Length; i++)
        {
            isStage[i] = false;
        }

        if (sceneName == "Stage_1")
        {
            isStage[0] = true;
        }
        else if (sceneName == "Stage_2")
        {
            isStage[1] = true;
        }
        else if (sceneName == "Stage_3")
        {
            isStage[2] = true;
        }
        else if (sceneName == "Stage_3")
        {
            isStage[4] = true;
        }
        else
        {
            return;
        }
    }
}
