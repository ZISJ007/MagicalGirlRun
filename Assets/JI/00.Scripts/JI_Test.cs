using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JI_Test : MonoBehaviour
{
    [Header("캐릭터 커스터마이저 컴포넌트")]
    private JI_CharacterCustomizer customizer;

    private JI_PlayerController playerController;
    private void Awake()
    {
        // JI_CharacterCustomizer 컴포넌트 가져오기
        customizer = FindObjectOfType<JI_CharacterCustomizer>();
        // JI_PlayerController 컴포넌트 가져오기
        playerController = FindObjectOfType<JI_PlayerController>();
    }
    private void Start()
    {
        playerController.IsStop();
    }
    public void OnClickEquipHat(GameObject hatPrefab)
    {
        JI_GM.Instance.SelectedHatPrefab = hatPrefab;
        customizer.EquipHat(hatPrefab);
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene("JI_Scene");
    }

}
