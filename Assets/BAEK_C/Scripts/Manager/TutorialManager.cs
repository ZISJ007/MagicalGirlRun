using System;
using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class TutorialManager : MonoBehaviour
{

    [System.Serializable]
    public class TutorialStep
    {
        public string message;
        public List<KeyCode> requiredKeys;
        public bool isRunning;
    }
    [SerializeField]private GameObject player;
    
    private Animator animator;
    private static readonly int isTalk = Animator.StringToHash("IsTalk");
    
    [SerializeField]private GameObject tutorialPanel;
    [SerializeField]private TextMeshProUGUI tutorialText;

    [Header("대사 & 조건 키 추가")]
    [SerializeField] private List<TutorialStep> steps = new List<TutorialStep>();
    
    private int currentStepIndex = 0;
    private bool tutorialActive = true;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlayBgm("Tutorial");
        }
        if (steps.Count == 0)
        {
            tutorialActive = false;
            return;
        }
        tutorialPanel.SetActive(true);
        ShowCurrentStep();
    }

    void Update()
    {
        if(currentStepIndex >= steps.Count) return;

        if (tutorialActive)
        {
            foreach (KeyCode key in steps[currentStepIndex].requiredKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    NextStep();
                    return;
                }
            }
        }

        if (steps[currentStepIndex].isRunning)
        {
            player.transform.position += Vector3.right * 6f * Time.deltaTime;
        }
    }

    void NextStep()
    {
        currentStepIndex++;

        if (currentStepIndex >= steps.Count)
        {
            StartCoroutine(EndTutorial());
        }
        else
        {
            ShowCurrentStep();
        }
    }

    void ShowCurrentStep()
    {
        animator.SetTrigger(isTalk);
        tutorialText.text = steps[currentStepIndex].message;
    }
   
    IEnumerator EndTutorial()
    {
        if (steps.Count > 0)
        {
            currentStepIndex = steps.Count - 1;
            steps[currentStepIndex].isRunning = true;
        }

        yield return new WaitForSeconds(1f);
        tutorialPanel.SetActive(false);

        tutorialActive = false;

        // 여기서 게임 시작 로직 연결
        Debug.Log("튜토리얼 끝 → 게임 시작!");
    }
}
