using UnityEngine;

public class JI_CharacterCustomizer : MonoBehaviour
{
    [Header("초기 장착할 모자 프리팹")]
    [SerializeField] private GameObject HatPrefab;

    [Header("머리 소켓")]
    [SerializeField] private Transform headSocket;

    private GameObject currentHat;

    private void Start()
    {
        if (JI_CustomManager.Instance != null && JI_CustomManager.Instance.SelectedHatPrefab != null)
        {
            EquipHat(JI_CustomManager.Instance.SelectedHatPrefab);
            return;
        }
        if (HatPrefab != null)
            EquipHat(HatPrefab);
    }
    public void EquipHat(GameObject hatPrefab)
    {
        if (hatPrefab == null)  // 선택된 모자가 없다면
        {
            if (currentHat != null) // 현재 장착된 모자가 있다면 제거
            {
                Destroy(currentHat);
                currentHat = null;
            }
            return;
        }

        if (currentHat != null) // 현재 장착된 모자가 있다면 제거
            Destroy(currentHat);

        currentHat = Instantiate(hatPrefab, headSocket);
        currentHat.transform.localPosition = Vector3.zero;
        currentHat.transform.localRotation = Quaternion.identity;
    }
}
