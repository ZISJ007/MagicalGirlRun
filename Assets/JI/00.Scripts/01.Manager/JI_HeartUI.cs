using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JI_HeartsUI : MonoBehaviour
{
    [Header("하트 UI 이미지")]
    public Image[] hearts;

    [Header("하트 상태별 이미지")]
    public Sprite fullHeart;  // 2체력일 때
    public Sprite halfHeart;  // 1체력일 때
    public Sprite emptyHeart; // 0체력일 때

    private JI_PlayerStats playerStats;

    private void Awake()
    {
        playerStats = FindObjectOfType<JI_PlayerStats>();
        if(playerStats == null)
        {
            Debug.LogError("플레이어 스탯이 없습니다.");
            return;
        }
    }

    private void Start()
    {
        UpdateHearts(); // 초기화 시 하트 UI 업데이트
    }

    public void UpdateHearts() // 하트 UI 업데이트
    {
        int curentHp = playerStats.CurrentHp; // 현재 체력
        int maxHp = playerStats.MaxHp;  // 최대 체력

        // 하트 개수는 최대 체력 / 2로 계산(한 하트당 2체력)
        // CeilToInt 함수는 소수점 올림 > maxhp = 3 일 때 나누기 2하면 1.5가 나오고 올림을 하면 2가 됨
        int heartCount = Mathf.CeilToInt(maxHp / 2f);

        // hearts 배열에서 사용할 하트 개수만 켜고, heartCount를 넘는 하트는 끔
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < heartCount);
        }

        // i번째 하트는 0~1= 1번째 하트,2~3= 2번째 하트처럼 2체력 단위로 표시하기 때문에
        // 이전 하트들이 담당한 체력(i*2)을 빼서 이 하트에 남은 체력을 계산함
        for (int i = 0; i < heartCount; i++)
        {
            int heartHp = curentHp - (i * 2); // i번째 하트의 체력 계산
            if (heartHp >= 2)
                hearts[i].sprite = fullHeart; // 체력 2 이상이면 hullHeart
            else if (heartHp == 1)
                hearts[i].sprite = halfHeart; // 체력 1이면 halfHeart
            else
                hearts[i].sprite = emptyHeart; // 체력 0이면 emptyHeart
        }
    }
}
