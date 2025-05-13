
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questUI;

    [Header("현재 스테이지")]
    // 스테이지 퀘스트 상태
    public static bool isQuestClear;
    public static bool onQuest;

    [Header("요구량, 보유량")]
    [SerializeField] private int demand = 5;
    [SerializeField] private int reserves = 0;

    void Start()
    {
        Debug.Log($"퀘스트 활성화");
        onQuest = true;
        isQuestClear = false;
        OnQuestUI();
    }

    public void GetQuestItem() // 퀘스트 아이템 습득
    {
        reserves ++;

        OnQuestUI();

        if (onQuest && demand <= reserves)
        {
            QuestComplete();
        }
    }

    private void QuestComplete() // 퀘스트 완료
    {
        questUI.text = $"퀘스트 아이템 수집\n-수집 완료-";
        isQuestClear = true;
        onQuest = false;
        reserves = 0;
    }

    private void OnQuestUI() // 퀘스트 현황 표시
    {
        if (onQuest == false) { return; }

        questUI.text = $"퀘스트 아이템 수집\n{reserves}/{demand}";
    }
}
