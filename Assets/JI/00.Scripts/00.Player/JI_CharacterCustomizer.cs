using UnityEngine;

public class JI_CharacterCustomizer : MonoBehaviour
{
    [Header("�ʱ� ������ ���� ������")]
    [SerializeField] private GameObject HatPrefab;

    [Header("�Ӹ� ����")]
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
        if (hatPrefab == null)  // ���õ� ���ڰ� ���ٸ�
        {
            if (currentHat != null) // ���� ������ ���ڰ� �ִٸ� ����
            {
                Destroy(currentHat);
                currentHat = null;
            }
            return;
        }

        if (currentHat != null) // ���� ������ ���ڰ� �ִٸ� ����
            Destroy(currentHat);

        currentHat = Instantiate(hatPrefab, headSocket);
        currentHat.transform.localPosition = Vector3.zero;
        currentHat.transform.localRotation = Quaternion.identity;
    }
}
