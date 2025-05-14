
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questUI;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private GameSystem gameSystem;

    [Header("현재 스테이지")]
    public static bool isQuestClear;
    public static bool onQuest;

    [Header("요구량, 보유량")]
    [SerializeField] private int demand = 5;
    [SerializeField] private int reserves = 0;

    void Start()
    {
        Debug.Log($"퀘스트 활성화");
        isQuestClear = false;
        GetQuest();
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
        questText.text = $"잼 수집율\n{reserves}/{demand}\n열쇠 획득 성공";
        isQuestClear = true;
        onQuest = false;
        reserves = 0;
    }

    private void OnQuestUI() // 퀘스트 현황 표시
    {
        if (!onQuest) return;

        questUI.text = $"잼 수집\n{reserves}/{demand}";
        questText.text = $"잼 수집율\n{reserves}/{demand}\n열쇠 획득 실패";
    }

    private void GetQuest() // 키를 가지고 있지 않을 때 퀘스트 활성화
    {
        int stageIndex = gameSystem.isStage - 1;

        if (stageIndex >= 0 && stageIndex < GameSystem.key.Length && GameSystem.key[stageIndex])
        {
            return; 
        }

        onQuest = true;
    }
}
