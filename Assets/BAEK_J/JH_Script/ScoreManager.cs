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
        if (nowScoreTxt != null)
            nowScoreTxt.text = score.ToString();

        string key = $"bestScore_{isStage}";

        // 기존 점수 가져오기
        int best = PlayerPrefs.GetInt(key, 0);

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
