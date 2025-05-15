using UnityEngine;
using UnityEngine.UI;

public class CustomUIManager : MonoBehaviour
{
    [Header("���� ���� ��ư")]
    [SerializeField] private Button[] hatButtons;
    [Header("�� ��ư�� ������ ���� ������")]
    [SerializeField] private GameObject[] hatPrefabs;
    [Header("���� ���� ��ư")]
    [SerializeField] private Button deleteButton;

    private void Awake()
    {
        // ���� ��ư ������ŭ �ݺ�
        for (int i = 0; i < hatButtons.Length; i++)
        {
            int idx = i;  // ���� �ε��� ����
            hatButtons[i].onClick.RemoveAllListeners(); // ���� ������ ����
            hatButtons[i].onClick.AddListener(() => // ���� ��ư Ŭ�� ��
            {
                JI_CustomManager.Instance.OnSelectHat(hatPrefabs[idx]);
            });
        }

        // 2) ���� ��ư ������
        if (deleteButton != null)
        {
            deleteButton.onClick.RemoveAllListeners(); // ���� ������ ����
            deleteButton.onClick.AddListener(() => // ���� ��ư Ŭ�� ��
            {
                JI_CustomManager.Instance.OnDeleteHat();
            });
        }

    }
}
