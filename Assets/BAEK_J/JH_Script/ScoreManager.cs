using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("현재 스테이지")]
    [SerializeField] private int isStage = 0;
    [SerializeField] private int score; // 게임 점수

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nowScoreTxt;
    [SerializeField] private TextMeshProUGUI bestScoreTxt;

    void Start()
    {
        score = 0;
    }

    public void AddScore(int amount) // 점수 증감
    {
        if (scoreText == null)
        {
            Debug.Log("텍스트 오브젝트를 할당 해주세요");
            return;
        }

        score += amount;
        scoreText.text = score.ToString();
    }

    public void SetScore()
    {
        Debug.Log($"SetScore 호출됨 | 현재 점수: {score}");

        if (nowScoreTxt != null)
            nowScoreTxt.text = score.ToString();

        string key = $"bestScore_{isStage}";
        int best = PlayerPrefs.GetInt(key, 0);

        Debug.Log($"저장된 최고 점수: {best}");

        if (score > best) // 현재 점수와 비교
        {
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save(); 
            bestScoreTxt.text = score.ToString();
        }
        else
        {
            bestScoreTxt.text = best.ToString();
        }

        PlayerPrefs.Save();
    }
}
