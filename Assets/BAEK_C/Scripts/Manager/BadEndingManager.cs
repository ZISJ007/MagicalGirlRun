using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BadEndingManager : MonoBehaviour
{
    public TextMeshProUGUI leftNameText;           // 왼쪽 캐릭터 이름 텍스트
    public TextMeshProUGUI rightNameText;          // 오른쪽 캐릭터 이름 텍스트
    public TextMeshProUGUI badstoryText;           // 대사 텍스트
   
    public GameObject Panel;    // 대화 패널
    public Button Button;           // 대사 진행 버튼

    public List<BadStoryLine> badstoryline;    // 대사 리스트
    private int currentIndex = 0;       // 현재 대사 인덱스

    void Start()
    {
        // 대사 리스트가 에디터에서 추가되었는지 확인
        

        Button.onClick.AddListener(NextLine); // 버튼 클릭 시 대사 진행
        ShowLine();
    }

    // 대사 출력
    void ShowLine()
    {
        if (currentIndex >= badstoryline.Count)
        {
            EndDialogue();
            return;
        }

        BadStoryLine line = badstoryline[currentIndex];

        // 이름 텍스트 설정
        if (line.speaker == "Left")
        {
            leftNameText.text = line.speakerName;   // 왼쪽 캐릭터 이름
            rightNameText.text = "";                // 오른쪽 캐릭터 이름은 비워둠
        }
        else if (line.speaker == "Right")
        {
            leftNameText.text = "";                // 왼쪽 캐릭터 이름은 비워둠
            rightNameText.text = line.speakerName;  // 오른쪽 캐릭터 이름
        }

        badstoryText.text = line.badstoryText;      // 대사 텍스트
        

        Panel.SetActive(true);  // 대화 패널 활성화
    }

    // 대사 진행
    void NextLine()
    {
        currentIndex++;
        ShowLine();
    }

    // 대화 종료
    void EndDialogue()
    {
        Panel.SetActive(false);  // 대화 종료 후 패널 비활성화
        // 대화 끝난 후 다른 처리
    }

}

[System.Serializable]
public class BadStoryLine
{
    public string speaker;             // 어떤 캐릭터가 말을 하는지 (Left / Right)
    public string speakerName;         // 말하는 사람의 이름
    public string badstoryText;        // 대사 내용
   
}
