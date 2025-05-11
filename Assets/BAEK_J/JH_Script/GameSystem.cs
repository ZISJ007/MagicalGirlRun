using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public int life = 3;

    private int stageScore = 0;

    [Header("속도와 목적지")]
    [SerializeField] private float speed = 3;
    [SerializeField] private float destination = 100;
    private float moveDistance;

    // 보유 중인 열쇠
    public bool key_1 = false;
    public bool key_2 = false;
    public bool key_3 = false;

    void Start()
    {
    }

    void Update()
    {
        moveDistance += speed * Time.deltaTime;

        if (moveDistance >= destination)
        {
            Finish();
            moveDistance = 0;
        }
    }

    public void ChangeSpeed(float amount) // 속도 증감
    {
        speed += amount;
        Debug.Log($"속도 {amount}");
    }

    public void ChangeLife(int amount) // 체력 증감
    {
        life += amount;
        Debug.Log($"체력 {amount}");
    }

    public void AddScore(int amount) // 점수 증감
    {
        stageScore += amount;
        Debug.Log($"획득 점수 {stageScore}");
        Debug.Log($"획득 점수 {moveDistance}");
    }

    public void GetKey() // 키 1~3 지급
    {
        if (StageManager.isStage == 1)
        {
            key_1 = true;
            Debug.Log("key_1 획득");
        }
        else if (StageManager.isStage == 2)
        {
            key_2 = true;
            Debug.Log("key_2 획득");
        }
        else if (StageManager.isStage == 3)
        {
            key_3 = true;
            Debug.Log($"key_3 획득");
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
}
