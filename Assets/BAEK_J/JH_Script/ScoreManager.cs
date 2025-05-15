
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("���� ��������")]
    [SerializeField] private int isStage = 0;
    [SerializeField] private int score; // ���� ����

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nowScoreTxt;
    [SerializeField] private TextMeshProUGUI bestScoreTxt;

    void Start()
    {
        score = 0;
    }

    public void AddScore(int amount) // ���� ����
    {
        if (scoreText == null)
        {
            Debug.Log("�ؽ�Ʈ ������Ʈ�� �Ҵ� ���ּ���");
            return;
        }

        score += amount;
        scoreText.text = $"���� ����:{score}";
    }

    public void SetScore()
    {
        if (nowScoreTxt != null)
            nowScoreTxt.text = $"���� ����: {score}";

        string key = $"bestScore_{isStage}";
        int best = PlayerPrefs.GetInt(key, 0);

        if (score > best) // ���� ������ ��
        {
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save(); 
            bestScoreTxt.text = $"�ְ� ����: {score}";
        }
        else
        {
            bestScoreTxt.text = $"�ְ� ����: {best}";
        }

        PlayerPrefs.Save();
    }
}
