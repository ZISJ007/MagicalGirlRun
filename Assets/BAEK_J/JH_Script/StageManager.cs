using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    // 현재 스테이지를 구분
    public static int isStage = 0; // 현재 스테이지(1~4)
    // 플레이어 참조
    private JHPlayer player;

    // 퀘스트 클리어 상태로 Stage 클리어시 보스 등장

    private void Start()
    {
        player = FindObjectOfType<JHPlayer>();
    }

    void Update()
    {

    }
}
