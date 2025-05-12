using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class StageInfo
{
    public string stageName;
    public string stageDescription;
}
public class StageSelectUI : MonoBehaviour
{

    [Header("스테이지 버튼")] [SerializeField] private List<Button> stageButtons;
    [Header("스테이지 정보(버튼 순서와동일하게)")]
    [SerializeField]private List<StageInfo> stageInfos;

    [Header("UI")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField]private TextMeshProUGUI stageInfoText;
    [SerializeField]private Button startButton;

    private string selectedStage = "";
    private int keysCollected = 0;

    void Start()
    {
        infoPanel.SetActive(false);

        //  메쉬프로라 폰트잇어야 한글가능

        if (stageButtons.Count != stageInfos.Count)
        {
            Debug.Log("버튼수와 정보의 수가 다름");
            return;
        }

        for (int i = 0; i < stageButtons.Count; i++)
        {
            int index = i;
            stageButtons[i].onClick.AddListener((() =>
            {
                selectedStage = stageInfos[index].stageName;
                stageInfoText.text = $"[{stageInfos[index].stageName} 정보]\n{stageInfos[index].stageDescription}";
                infoPanel.SetActive(true);
            }));
        }
        
        startButton.onClick.AddListener((() => StartSelectedStage()));
    }

    void StartSelectedStage()
    {
        if (selectedStage == "BossStage" && keysCollected < 3)
        {           
            return;
        }

        if (string.IsNullOrEmpty(selectedStage))
        {
            Debug.Log("no selected stage");
            return;
        }

        // 선택한 스테이지 이름 저장
        StageData.selectedStage = selectedStage;

        // 공통 씬으로 이동
        SceneManager.LoadScene(selectedStage);
    }

    public void StageCleared(string stageName)
    {
        // 외부에서 스테이지 클리어시 호출 
        keysCollected++;

        if (keysCollected >= 3)
        {
            
        }
    }
    public static class StageData
    {
        public static string selectedStage;
    }
}