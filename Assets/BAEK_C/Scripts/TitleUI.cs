using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : BaseUI
{  

    public Button startButton;
    public Button exitButton;
    public Button fakeExitButton;
    public VideoPlayer videoPlayer;

    public override void Init()
    {
       
        startButton.onClick.AddListener(OnStart);
        exitButton.onClick.AddListener(OnExit);
        fakeExitButton.onClick.AddListener(OnFakeExit);

        videoPlayer.Play();  
    }

    public void OnStart()
    {
        bool tutorialDone = PlayerPrefs.GetInt("TutorialCompleted", 0) == 1;

        if (tutorialDone)
        {
            SceneManager.LoadScene("StageSelectScene");
        }
        else
        {
            SceneManager.LoadScene("StageScene"); // 튜토리얼 포함된 씬
            StageSelectUI.StageData.selectedStage = "Tutorial"; // 튜토리얼 지정
        }

    }

    public void OnExit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }

    public void OnFakeExit()
    {
        Debug.Log("하남자처럼 도망이야? 넌 마법소녀다");
        SceneManager.LoadScene("StageSelectScene");
    }
}
