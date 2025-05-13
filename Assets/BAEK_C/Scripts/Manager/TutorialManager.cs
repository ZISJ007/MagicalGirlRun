using TMPro;
using UnityEngine;
using System.Collections;
public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialCharacter;
    public TextMeshProUGUI tutorialText;

    private int tutorialStep = 0;
    private bool TutorialActive = true;

    void Start()
    {
        tutorialCharacter.SetActive(true);
        ShowMessage("스페이스바로 점프할 수 있도록 합니다. 실시");
    }

    void Update()
    {
        if (!TutorialActive) return;

        if (tutorialStep == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            ShowMessage("이번엔 Shift로 슬라이딩해 보도록 합니다.");
            tutorialStep++;
        }
        else if (tutorialStep == 1 && Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift))
        {
            ShowMessage("좋아, 제군들 마법소녀런에 온걸 환영한다");
            tutorialStep++;
        }
        else if (tutorialStep == 2)
        {
            StartCoroutine(EndTutorial());
            tutorialStep++;
        }
    }

    void ShowMessage(string message)
    {
        tutorialText.text = message;
    }
   
    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(1f);
        tutorialCharacter.SetActive(false);

        TutorialActive = false;

        // 여기서 게임 시작 로직 연결
        Debug.Log("튜토리얼 끝 → 게임 시작!");
    }
}
