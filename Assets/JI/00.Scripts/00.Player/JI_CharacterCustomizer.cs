using UnityEngine;

public class JI_CharacterCustomizer : MonoBehaviour
{
    [Header("초기 장착할 모자 프리팹")]
    [Tooltip("이 프리팹을 Spawn할 때 자동으로 EquipHat 호출")]
    [SerializeField] private GameObject HatPrefab;

    [Header("머리 소켓")]
    [SerializeField] private Transform headSocket;

    private GameObject currentHat;

    private void Start()
    {
        // 1) 커스텀 씬에서 선택된 모자 정보가 있으면 장착
        if (JI_CustomManager.Instance != null && JI_CustomManager.Instance.SelectedHatPrefab != null)
        {
            EquipHat(JI_CustomManager.Instance.SelectedHatPrefab);
            return;
        }
        // 2) 선택 정보가 없으면 초기 지정값으로 장착
        if (HatPrefab != null)
            EquipHat(HatPrefab);
    }

    /// <summary>
    /// 모자 장착 메서드 (UI나 코드에서 호출해도 OK)
    /// </summary>
    public void EquipHat(GameObject hatPrefab)
    {
        if (currentHat != null)
            Destroy(currentHat);

        currentHat = Instantiate(hatPrefab, headSocket);
        currentHat.transform.localPosition = Vector3.zero;
        currentHat.transform.localRotation = Quaternion.identity;
    }
}
