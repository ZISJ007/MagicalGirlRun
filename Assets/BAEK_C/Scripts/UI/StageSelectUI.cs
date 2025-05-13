using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
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
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField]private TextMeshProUGUI stageInfoText;
    [SerializeField]private Button startButton;

    [Header("사운드")]
    [SerializeField] private AudioSource audioSource;     // 효과음용
    [SerializeField] private AudioSource bgmSource;        // BGM용

    [SerializeField] private AudioClip stageSelectSound;   // 스테이지 선택 효과음
    [SerializeField] private AudioClip startButtonSound;   // 시작 버튼 효과음

    private string selectedStage = "";
    private int keysCollected = 0;

    void Start()
    {
        infoPanel.SetActive(false);
        if (bgmSource != null && !bgmSource.isPlaying)
        {
            bgmSource.Play();
        }
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
                AnimateButtonPress(stageButtons[index].transform);
                if (stageSelectSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(stageSelectSound);
                }
                selectedStage = stageInfos[index].stageName;
                stageInfoText.text = $"[{stageInfos[index].stageName} 정보]\n{stageInfos[index].stageDescription}";
                infoPanel.SetActive(true);

                ShowBestScore(index);
            }));
        }

        startButton.onClick.AddListener(() => {
            AnimateButtonPress(startButton.transform);
            if (startButtonSound != null && audioSource != null)
            {
                StartCoroutine(PlayStartSoundAndLoadScene());
            }
            else
            {
                // 효과음 없으면 그냥 씬 이동
                StartSelectedStage();
            }
        });
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

    public void AnimateButtonPress(Transform buttonTransform)
    {
        StartCoroutine(ButtonPressRoutine(buttonTransform));
    }

    private IEnumerator ButtonPressRoutine(Transform buttonTransform)
    {
        Vector3 originalPos = buttonTransform.localPosition;
        Vector3 targetPos = originalPos + new Vector3(0, 20f, 0); //위로 올라가는정도

        float duration = 0.1f;
        float elapsed = 0f;

        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            buttonTransform.localPosition = Vector3.Lerp(originalPos, targetPos, t);
            yield return null;
        }

        elapsed = 0f;

        
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            buttonTransform.localPosition = Vector3.Lerp(targetPos, originalPos, t);
            yield return null;
        }

       
        buttonTransform.localPosition = originalPos;
    }
    private IEnumerator PlayStartSoundAndLoadScene()
    {
        audioSource.PlayOneShot(startButtonSound);

        
        yield return new WaitForSeconds(startButtonSound.length);

        
        StartSelectedStage();
    }

    void ShowBestScore(int stageIndex)
    {
        string key = $"bestScore_{stageIndex + 1}";
        int best = PlayerPrefs.GetInt(key, 0);
        bestScoreText.text = $"최고 점수: {best}";
    }
}