using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;

[System.Serializable]
public class StageInfo
{
    public string stageName;
    public string stageDescription;
    public bool stageLock;
}

public class StageSelectUI : MonoBehaviour
{
    [Header("�������� ��ư")] 
    [SerializeField] private List<Button> stageButtons;

    [Header("�������� ����(��ư �����͵����ϰ�)")] [SerializeField]
    private List<StageInfo> stageInfos;

    [Header("UI")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI stageInfoText;
    [SerializeField] private Button startButton;

    [Header("Boss ��ư")]
    [SerializeField] private BossButton bossButton;

    [Header("����")] 
    [SerializeField] private AudioSource audioSource; // ȿ������

    [SerializeField] private AudioClip stageSelectSound; // �������� ���� ȿ����
    [SerializeField] private AudioClip startButtonSound; // ���� ��ư ȿ����

    [Header("��ư �ִϸ��̼�")] 
    [SerializeField] private Transform buttonParent;
    [SerializeField] private float focusScale;
    [SerializeField] private float offsetZ;
    [SerializeField] private float animDuration;

    private bool isAnimating = false;

    private string selectedStage = "";
    private int keysCollected = 0;

    private Vector3[] originalPosition;
    private Vector3[] originalScale;
    private int[] originalSiblingIndexs;
    private Button currentFocusButton = null;

    void Start()
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlayBgm("selectedStage");
        }
        
        int trueCount = GameSystem.key.Count(k => k);
        {
            Debug.Log(trueCount);
        }

        if (trueCount == 3)
            stageInfos[5].stageLock = true;

        Time.timeScale = 1;
        infoPanel.SetActive(false);
        //  �޽����ζ� ��Ʈ�վ�� �ѱ۰���

        if (stageButtons.Count != stageInfos.Count)
        {
            Debug.Log("��ư���� ������ ���� �ٸ�");
            return;
        }

        originalPosition = new Vector3[stageButtons.Count];
        originalScale = new Vector3[stageButtons.Count];


        originalSiblingIndexs = new int[stageButtons.Count];
        for (int i = 0; i < stageButtons.Count; i++)
        {
            originalSiblingIndexs[i]=stageButtons[i].transform.GetSiblingIndex();
            originalPosition[i] = stageButtons[i].transform.localPosition;
            originalScale[i] = stageButtons[i].transform.localScale;

            int index = i;
            stageButtons[i].onClick.AddListener((() =>
            {
                HandleStageButtonClick(stageButtons[index], index);
            }));
        }

//---------------------------------------------------------------------------
        startButton.onClick.AddListener(() =>
        {
            AnimateButtonPress(startButton.transform);
            if (startButtonSound != null && audioSource != null)
            {
                StartCoroutine(PlayStartSoundAndLoadScene());
            }
            else
            {
                // ȿ���� ������ �׳� �� �̵�
                StartSelectedStage();
            }
        });
    }

    void HandleStageButtonClick(Button _clickedButton, int _index)
    {
        if (stageInfos[_index].stageLock || isAnimating) return;

        if (_index == 5)
        {
            bossButton.UnlockButton();
        }

        if (currentFocusButton == _clickedButton)
        {
            ResetButton();
            currentFocusButton = null;
            return;
        }
        else
        {
            AnimStageSelect(_clickedButton);
            currentFocusButton = _clickedButton;

            _clickedButton.transform.SetAsLastSibling();
            _clickedButton.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        foreach (var button in stageButtons)
        {
            var image=  button.GetComponent<Image>();
            if(image!=null)
            {
                image.raycastTarget=(button==_clickedButton);
            }
            button.interactable=(button==_clickedButton);
        }

        AnimateButtonPress(_clickedButton.transform);
        if (stageSelectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(stageSelectSound);
        }

        selectedStage = stageInfos[_index].stageName;
        stageInfoText.text = $"[{stageInfos[_index].stageName} ����]\n{stageInfos[_index].stageDescription}";
        infoPanel.SetActive(true);

        ShowBestScore(_index + 1);
    }

    void StartSelectedStage()
    {

        if (string.IsNullOrEmpty(selectedStage))
        {
            Debug.Log("no selected stage");
            return;
        }

        // ������ �������� �̸� ����
        StageData.selectedStage = selectedStage;

        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlayBgm(selectedStage);
        }

        // ���� ������ �̵�
        SceneManager.LoadScene(selectedStage);
    }

    
    void AnimStageSelect(Button _selectedButton)
    {
        StartCoroutine(StageSelectAnimRoutine(_selectedButton));
    }
    
    
    IEnumerator StageSelectAnimRoutine(Button _selectedButton)
    {
        isAnimating = true;
        int animCount = 0;

        for(int i = 0; i < stageButtons.Count; i++)
        {
           
            Button button = stageButtons[i];
            bool isSelected = (button == _selectedButton);
            
            Vector3 targetPos= isSelected?buttonParent.localPosition:originalPosition[i]+new Vector3(0,0,offsetZ);
            Vector3 targetScale = isSelected ? Vector3.one * focusScale : Vector3.one * 0.08f;

            animCount++;
            StartCoroutine(AnimateButton(button.transform, targetPos, targetScale,() => { animCount--; }));
        }
        while(animCount > 0)
            yield return null;
        
        isAnimating = false;
    }

    void ResetButton()
    {

        StartCoroutine(ResetAnimButton());
    }
    
    private IEnumerator ResetAnimButton()
    {
        isAnimating = true;
        infoPanel.SetActive(false);
        int animIndex = 0;
        for (int i = 0; i < stageButtons.Count; i++)
        {
            animIndex++;
            stageButtons[i].transform.SetSiblingIndex(originalSiblingIndexs[i]);
            StartCoroutine(AnimateButton(stageButtons[i].transform,originalPosition[i],originalScale[i],() => { animIndex--; }));
        }
        while(animIndex > 0)
            yield return null;      
        for (int i = 0; i < stageButtons.Count; i++)
        {
            
            var image = stageButtons[i].GetComponent<Image>();
            if (image != null)
            {
                image.raycastTarget = true;
            }

            stageButtons[i].interactable = true;
        }
        
        isAnimating = false;
    }
    IEnumerator AnimateButton(Transform _buttonTransform, Vector3 _targetPos, Vector3 _targetScale, System.Action onComplete=null)
    {
        Vector3 targetPos = _buttonTransform.localPosition;
        Vector3 targetScale = _buttonTransform.localScale;

        float elapsed = 0f;
        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / animDuration;
            _buttonTransform.localPosition = Vector3.Lerp(targetPos, _targetPos, t);
            _buttonTransform.localScale = Vector3.Lerp(targetScale, _targetScale, t);
            yield return null;
        }

        _buttonTransform.localPosition = _targetPos;
        _buttonTransform.localScale = _targetScale;

        onComplete?.Invoke();
    }

    public void StageCleared(string stageName)
    {
        // �ܺο��� �������� Ŭ����� ȣ�� 
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
        Vector3 targetPos = originalPos + new Vector3(0, 20f, 0); //���� �ö󰡴�����

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

    private void ShowBestScore(int stage)
    {
        string key = $"bestScore_{stage}";
        int best = PlayerPrefs.GetInt(key, 0);
        bestScoreText.text = $"�ְ� ����: {best}";
    }
}