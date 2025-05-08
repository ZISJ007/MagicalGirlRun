using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class TitleUI : UIManagerBase
{  

    public Button startButton;
    public Button quitButton;
    public Button fakeQuitButton;
    public VideoPlayer videoPlayer;

    public override void Init()
    {
        startButton.onClick.AddListener(OnStart);
        quitButton.onClick.AddListener(OnExit);
        fakeQuitButton.onClick.AddListener(OnFakeExit);

        videoPlayer.Play();  // 배경 영상 재생
    }

    private void OnStart()
    {
        Debug.Log("게임 시작!");
        Hide();
        // 씬 전환 또는 StageSelectUI.Show() 호출
    }

    private void OnExit()
    {
        Debug.Log("게임 종료 시도");
        Application.Quit();
    }

    private void OnFakeExit()
    {
        Debug.Log("어림도 없지! 가짜 종료!");
       
    }
}
