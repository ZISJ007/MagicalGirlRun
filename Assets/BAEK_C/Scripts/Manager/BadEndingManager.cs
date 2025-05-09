using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BadEndingManager : MonoBehaviour
{
    public TextMeshProUGUI badendingText; 
    public TextMeshProUGUI characterNameText; 
    public Image characterImage; 
    public Button nextButton; 

    public string[] dialogue = {
        "마법소녀 1: ",
        "마법소녀 2: ",
        "마법소녀 3: ",
        "주인공: "

    }; 
    public string[] characterNames = {
        "마법소녀 1",
        "마법소녀 2",
        "마법소녀 3",
        "주인공"}; // 캐릭터 이름
    public Sprite[] characterSprites = { 
    }; //캐릭터이미지

    private int currentDialogueIndex = 0; //현재 대사

    void Start()
    {
        nextButton.onClick.AddListener(OnNextText);
        ShowText();  // 첫번째대사
    }

    //대사 출력
    void ShowText()
    {
        if (currentDialogueIndex < dialogue.Length)
        {
            badendingText.text = dialogue[currentDialogueIndex];  
            characterNameText.text = characterNames[currentDialogueIndex]; 
            characterImage.sprite = characterSprites[currentDialogueIndex];  
        }
        else
        {
            
            Ending();
        }
    }

    //다음 대사로
    void OnNextText()
    {
        currentDialogueIndex++;  
        ShowText(); 
    }

    //대사가 끝나면 엔딩 처리나 씬 이동
    void Ending()
    {
       
    
      //SceneManager.LoadScene("EndingScene");
    }
}
