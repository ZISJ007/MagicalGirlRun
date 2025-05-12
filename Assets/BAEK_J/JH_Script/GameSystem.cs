using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameSystem : MonoBehaviour
{
   [SerializeField] private int stageScore; // 게임 점수
   [SerializeField] private float beforeSpeed; // 증감 전 속도 저장
    private Coroutine speedChange; // 지속시간 코루틴

    [Header("속도와 목적지 설정")]
    [SerializeField] public static float speed = 5;
    [SerializeField] private float moveDistance;
    [SerializeField] private float destination = 100;

    private bool hasFinished = false; // Finish 메서드 반복 실행 방지

    // 보유 중인 열쇠
    public static bool[] key = new bool[3];

    void Update()
    {
        if (hasFinished) return;

        moveDistance += speed * Time.deltaTime;

        if (moveDistance >= destination)
        {
            hasFinished = true;
            Time.timeScale = 0f;
            moveDistance = 0;
            stageScore = 0;
            Finish();
        }
    }

    public void ChangeSpeed(float amount, float duration) // 1) 지속 시간동안 속도 증감
    {
        beforeSpeed = speed;
        speed += amount;
        Debug.Log($"속도 변동 {speed} (지속 시간: {duration}초)");
        speedChange = StartCoroutine(RevertSpeed(duration));
    }
    private IEnumerator RevertSpeed(float duration) // 2) 지속시간 종료시 복구
    {
        yield return new WaitForSeconds(duration);
        speed = beforeSpeed;
        Debug.Log($"속도 복원 {speed}");
    }

    public void AddScore(int amount) // 점수 증감
    {
        stageScore += amount;
    }

    private void Finish() // 퀘스트를 클리어 했다면 키 제공
    {
        for (int i = 0; i < key.Length; i++)
        {
            if (QuestManager.isQuestClear[i] == true)
            {
                GameSystem.key[i] = true;
                Debug.Log($"{i + 1}번째 키 획득");
            }
        }
    }
}
