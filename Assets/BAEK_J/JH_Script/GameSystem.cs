using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public int life = 3;
    private float beforeSpeed; // 증감 전 속도 저장
    private Coroutine speedChange; // 지속시간 코루틴
    public int stageScore;

    [Header("속도와 목적지 설정")]
    [SerializeField] private float speed = 3;
    [SerializeField] private float moveDistance;
    [SerializeField] private float destination = 100;

    bool hasFinished = false;

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
            Finish();
            moveDistance = 0;
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

    public void ChangeLife(int amount) // 체력 증감
    {
        life += amount;
        Debug.Log($"체력 변동 {life}+{amount}");
    }

    public void AddScore(int amount) // 점수 증감
    {
        stageScore += amount;
        //Debug.Log($"획득 점수 {stageScore}");
        //Debug.Log($"이동 거리 {moveDistance:N1}m");
    }

    private void Finish() // 퀘스트를 클리어 했다면 키 제공
    {
        for (int i = 0; i <= key.Length; i++)
        {
            if (QuestManager.isQuestClear[i] == true)
            {
                GameSystem.key[i] = true;
                Debug.Log($"{i + 1}번째 키 획득");
            }
        }
    }
}
