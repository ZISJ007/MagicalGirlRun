using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoSelectStageButton : MonoBehaviour
{
    public void ExitGame()
    {
        SceneManager.LoadScene("Scenes/StageSelectScene");
    }
}
