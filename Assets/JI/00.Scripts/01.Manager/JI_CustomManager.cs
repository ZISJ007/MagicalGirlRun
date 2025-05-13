using UnityEngine;
public class JI_CustomManager : MonoBehaviour
{
    public static JI_CustomManager Instance { get; private set; }

    public GameObject SelectedHatPrefab { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void ChooseHat(GameObject hatPrefab)
    {
        SelectedHatPrefab = hatPrefab;
    }
    public void ResetCustomization()
    {
        SelectedHatPrefab = null;
    }
    public void OnSelectHat(GameObject hatPrefab)
    {
        // 선택 정보 저장
        ChooseHat(hatPrefab);

        // 커스터마이저가 존재하면 즉시 장착
        var customizer = FindObjectOfType<JI_CharacterCustomizer>();
        if (customizer != null)
        {
            customizer.EquipHat(hatPrefab);
        }
    }
}
