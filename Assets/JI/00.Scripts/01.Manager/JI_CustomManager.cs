using UnityEngine;

/// <summary>
/// CustomManager는 커스텀 씬과 게임 플레이 씬 간의
/// 캐릭터 커스텀 선택 정보를 관리합니다.
/// </summary>
public class JI_CustomManager : MonoBehaviour
{
    public static JI_CustomManager Instance { get; private set; }

    /// <summary>
    /// 플레이어가 선택한 모자 프리팹
    /// </summary>
    public GameObject SelectedHatPrefab { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// 모자 선택 정보를 저장합니다.
    /// </summary>
    public void ChooseHat(GameObject hatPrefab)
    {
        SelectedHatPrefab = hatPrefab;
    }

    /// <summary>
    /// 선택 정보를 초기화합니다.
    /// </summary>
    public void ResetCustomization()
    {
        SelectedHatPrefab = null;
    }

    /// <summary>
    /// UI 버튼 OnClick 이벤트에 연결하여 모자를 선택하고,
    /// 현재 씬의 Customizer에 즉시 반영합니다.
    /// </summary>
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
