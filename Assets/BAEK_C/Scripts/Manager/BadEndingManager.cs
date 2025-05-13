using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BadEndingManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public FadeScene fadeInScene;
    public TextMeshProUGUI leftNameText;           
    public TextMeshProUGUI rightNameText;          
    public TextMeshProUGUI badstoryText;
    private bool isButtonEnd = false;
    public GameObject Panel;    
    public Button Button;          

    public List<BadStoryLine> badstoryline;   
    private int currentIndex = 0;       
    
    void Start()
    {          
     Button.onClick.AddListener(NextLine); 
    }
    public void ShowLine()
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
        if (bgmSource != null)
        {
            bgmSource.Stop();

            if (line.bgmClip != null)
            {
                bgmSource.clip = line.bgmClip;
                bgmSource.loop = false; // 필요하면 true로
                bgmSource.Play();
            }
        }
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
        if (isButtonEnd) return;
        isButtonEnd = true;    

        if (Button != null)
            Button.interactable = false;  //중복방지 그냥 버튼 아예 꺼버림
        Panel.SetActive(false);

        if (fadeInScene != null)
            fadeInScene.StartFadeOutToBlack();
    }

}

[System.Serializable]
public class BadStoryLine
{
    public string speaker;             // 어떤 캐릭터가 말을 하는지 (Left,Right)
    public string speakerName;         // 말하는 사람의 이름
    public string badstoryText;        // 대사 내용
    public AudioClip bgmClip;
}
