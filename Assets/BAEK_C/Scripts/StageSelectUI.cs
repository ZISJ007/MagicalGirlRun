using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StageSelectUI : MonoBehaviour
{
    public Button stage1Button;
    public Button stage2Button;
    public Button stage3Button;
    public Button bossStageButton;
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button startButton;

    private string selectedStage = "";
    private int keysCollected = 0;

    void Start()
    {
        bossStageButton.gameObject.SetActive(false);
        infoPanel.SetActive(false);

        // 버튼 이벤트 등록 텍스트 메쉬프로라 폰트잇어야 한글가능
        stage1Button.onClick.AddListener(() => SelectStage("Stage1", "난이도: 쉬움\n보스:? "));
        stage2Button.onClick.AddListener(() => SelectStage("Stage2", "난이도: 중간\n보스: ?"));
        stage3Button.onClick.AddListener(() => SelectStage("Stage3", "난이도: 어려움\n보스:? "));
        bossStageButton.onClick.AddListener(() => SelectStage("BossStage", "난이도: ???"));

        startButton.onClick.AddListener(() => StartSelectedStage());
    }

    void SelectStage(string stageName, string info)
    {
       
        selectedStage = stageName;
        infoText.text = $"[{stageName} 정보]\n{info}";
        infoPanel.SetActive(true);
 

    }

    void StartSelectedStage()
    {
        if (selectedStage == "BossStage" && keysCollected < 3)
        {           
            return;
        }

        // 선택한 스테이지 이름 저장
        StageData.selectedStage = selectedStage;

        // 공통 씬으로 이동
        SceneManager.LoadScene("StageScene");
    }

    public void StageCleared(string stageName)
    {
        // 외부에서 스테이지 클리어시 호출 
        keysCollected++;
        

        if (keysCollected >= 3)
        {
            bossStageButton.gameObject.SetActive(true);
        }
    }
    public static class StageData
    {
        public static string selectedStage;
    }
}