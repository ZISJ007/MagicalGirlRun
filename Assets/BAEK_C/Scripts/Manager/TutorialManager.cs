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
    }
    
    [FormerlySerializedAs("tutorialCharacter")] public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;

    [SerializeField] private List<TutorialStep> steps = new List<TutorialStep>();
    
    private int currentStepIndex = 0;
    private bool tutorialActive = true;

    void Start()
    {
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
        if(!tutorialActive||currentStepIndex >= steps.Count) return;

        foreach (KeyCode key in steps[currentStepIndex].requiredKeys)
        {
            if (Input.GetKeyDown(key))
            {
                NextStep();
                break;
            }
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
        tutorialText.text = steps[currentStepIndex].message;
    }
   
    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(1f);
        tutorialPanel.SetActive(false);

        tutorialActive = false;

        // 여기서 게임 시작 로직 연결
        Debug.Log("튜토리얼 끝 → 게임 시작!");
    }
}
