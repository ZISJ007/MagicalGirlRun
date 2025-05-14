using UnityEngine;
using UnityEngine.UI;

public class CustomUIManager : MonoBehaviour
{
    [Header("모자 선택 버튼")]
    [SerializeField] private Button[] hatButtons;
    [Header("각 버튼에 대응할 모자 프리팹")]
    [SerializeField] private GameObject[] hatPrefabs;
    [Header("모자 해제 버튼")]
    [SerializeField] private Button deleteButton;

    private void Awake()
    {
        // 모자 버튼 개수만큼 반복
        for (int i = 0; i < hatButtons.Length; i++)
        {
            int idx = i;  // 현재 인덱스 저장
            hatButtons[i].onClick.RemoveAllListeners(); // 기존 리스너 제거
            hatButtons[i].onClick.AddListener(() => // 모자 버튼 클릭 시
            {
                JI_CustomManager.Instance.OnSelectHat(hatPrefabs[idx]);
            });
        }

        // 2) 해제 버튼 리스너
        if (deleteButton != null)
        {
            deleteButton.onClick.RemoveAllListeners(); // 기존 리스너 제거
            deleteButton.onClick.AddListener(() => // 해제 버튼 클릭 시
            {
                JI_CustomManager.Instance.OnDeleteHat();
            });
        }

    }
}
