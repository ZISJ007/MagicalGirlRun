using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JI_HeartsUI : MonoBehaviour
{
    [Header("하트 프리팹")]
    [SerializeField] private GameObject heartPrefab; // 하트 프리팹
    [Header("하트 소환 기준")]
    [SerializeField] private RectTransform spawnPoint;

    [Header("하트 상태별 이미지")]
    public Sprite fullHeart;  // 2체력일 때
    public Sprite halfHeart;  // 1체력일 때
    public Sprite emptyHeart; // 0체력일 때

    private JI_PlayerStats playerStats;
    private List<Image> hearts = new List<Image>(); // 하트 UI 이미지 리스트
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
        CreateHearts(); // 하트 UI 생성
        UpdateHearts(); // 초기화 시 하트 UI 업데이트
    }
    public void CreateHearts()
    {
       
        foreach (Transform t in spawnPoint) // 하트 UI가 생성될 부모 오브젝트의 자식 오브젝트를 모두 삭제
        {
            Destroy(t.gameObject);
            hearts.Clear();
        }
        // 하트 개수 계산 (한 하트당 2체력)
        int heartCount = Mathf.CeilToInt(playerStats.MaxHp / 2f);

        //하트 생성 & 배치
        for (int i = 0; i < heartCount; i++)
        {
            // 하트 프리팹을 생성하고 RectTransform을 spawnPoint의 자식으로 설정
            Image img = Instantiate(heartPrefab, spawnPoint).GetComponent<Image>(); 
            // 부모 위치 기준으로 X축 오프셋 적용
            img.rectTransform.anchoredPosition = new Vector2(i * 66, 0);

            hearts.Add(img); // 하트 UI 이미지 리스트에 추가
        }
    }
    public void UpdateHearts()
    {
        int currentHp = playerStats.CurrentHp;

        for (int i = 0; i < hearts.Count; i++)
        {
            // i번째 하트는 0~1= 1번째 하트,2~3= 2번째 하트처럼 2체력 단위로 표시하기 때문에
            // 이전 하트들이 담당한 체력(i*2)을 빼서 이 하트에 남은 체력을 계산함
            int heartHp = currentHp - (i * 2);  
            if (heartHp >= 2) hearts[i].sprite = fullHeart; 
            else if (heartHp == 1) hearts[i].sprite = halfHeart; 
            else if (heartHp == 0) hearts[i].sprite = emptyHeart; 
        }
    }
}
