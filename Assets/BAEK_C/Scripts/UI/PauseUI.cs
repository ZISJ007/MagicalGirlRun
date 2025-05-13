using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public GameObject pauseMenuUI; 
    public Button resumeButton;     
    public Button exitButton;       
    public Button pauseButton;      

    private bool isPaused = false;  

    void Start()
    {
        Time.timeScale = 1;
        // 일시정지 UI 비활성화
        pauseMenuUI.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(ExitGame);
        pauseButton.onClick.AddListener(PauseGame); 
    }

    void Update()
    {
        //esc 눌러도 일시정지 가능
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC");
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // 게임 일시정지
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;  //0=멈추기
        pauseMenuUI.SetActive(true);  //일시정지 등장
    }

    // 게임 재개
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;  //1=정상으로
        pauseMenuUI.SetActive(false);  //일시정지 꺼짐
    }

    // 게임 종료
    void ExitGame()
    {  
        SceneManager.LoadScene("Scenes/StageSelectScene");
    }
}
