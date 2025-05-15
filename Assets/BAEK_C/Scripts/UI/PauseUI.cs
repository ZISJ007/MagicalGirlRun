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
        // �Ͻ����� UI ��Ȱ��ȭ
        pauseMenuUI.SetActive(false);
        resumeButton.onClick.AddListener(ResumeGame);
        exitButton.onClick.AddListener(ExitGame);
        pauseButton.onClick.AddListener(PauseGame); 
    }

    void Update()
    {
        //esc ������ �Ͻ����� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC");
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    // ���� �Ͻ�����
    void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;  //0=���߱�
        pauseMenuUI.SetActive(true);  //�Ͻ����� ����
    }

    // ���� �簳
    void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;  //1=��������
        pauseMenuUI.SetActive(false);  //�Ͻ����� ����
    }

    // ���� ����
    void ExitGame()
    {  
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlayBgm("StageSelectScene");
        }
        SceneManager.LoadScene("Scenes/StageSelectScene");
    }
}
