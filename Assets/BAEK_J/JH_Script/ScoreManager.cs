
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
        scoreText.text = $"현재 점수:{score}";
    }

    public void SetScore()
    {
        if (nowScoreTxt != null)
            nowScoreTxt.text = $"현재 점수: {score}";

        string key = $"bestScore_{isStage}";
        int best = PlayerPrefs.GetInt(key, 0);

        if (score > best) // 현재 점수와 비교
        {
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save(); 
            bestScoreTxt.text = $"최고 점수: {score}";
        }
        else
        {
            bestScoreTxt.text = $"최고 점수: {best}";
        }

        PlayerPrefs.Save();
    }
}
