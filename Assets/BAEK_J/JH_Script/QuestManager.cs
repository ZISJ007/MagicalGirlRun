
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questUI;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private GameSystem gameSystem;

    [Header("���� ��������")]
    public static bool isQuestClear;
    public static bool onQuest;

    [Header("�䱸��, ������")]
    [SerializeField] private int demand = 5;
    [SerializeField] private int reserves = 0;

    void Start()
    {
        Debug.Log($"����Ʈ Ȱ��ȭ");
        isQuestClear = false;
        GetQuest();
        OnQuestUI();
    }

    public void GetQuestItem() // ����Ʈ ������ ����
    {
        reserves ++;

        OnQuestUI();

        if (onQuest && demand <= reserves)
        {
            QuestComplete();
        }
    }

    private void QuestComplete() // ����Ʈ �Ϸ�
    {
        questUI.text = $"�������� �� ����\n-���� �Ϸ�-";
        questText.text = $"�������� �� ������\n{reserves}/{demand}\n���� ȹ�� ����";
        isQuestClear = true;
        onQuest = false;
        reserves = 0;
    }

    private void OnQuestUI() // ����Ʈ ��Ȳ ǥ��
    {
        if (!onQuest) return;

        questUI.text = $"�������� �� ����\n{reserves}/{demand}";
        questText.text = $"�������� �� ������\n{reserves}/{demand}\n���� ȹ�� ����";
    }

    private void GetQuest() // Ű�� ������ ���� ���� �� ����Ʈ Ȱ��ȭ
    {
        int stageIndex = gameSystem.isStage - 1;

        if (stageIndex >= 0 && stageIndex < GameSystem.key.Length && GameSystem.key[stageIndex])
        {
            return; 
        }

        onQuest = true;
    }
}
