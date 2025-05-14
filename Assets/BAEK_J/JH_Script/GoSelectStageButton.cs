using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoSelectStageButton : MonoBehaviour
{
    public void ExitGame()
    {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlayBgm("StageSelectScene");
        }
        SceneManager.LoadScene("Scenes/StageSelectScene");
    }
}
