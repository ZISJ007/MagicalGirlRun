using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    // 현재 스테이지를 구분
    public static bool[] isStage = new bool[4]; // 현재 스테이지(1~4)

    private void Awake()
    {
        for (int i = 0; i < isStage.Length; i++)
        {
            isStage[i] = false;
        }

        if (SceneManager.GetActiveScene().name == "Jinhwan")
        {
            isStage[1] = true;
            Debug.Log($"현재 스테이지는 1입니다");
        }
        else
        {
            Debug.Log("존재하지 않는 스테이지입니다");
        }

    }
    void Start()
    {

    }

    void Update()
    {

    }
}
