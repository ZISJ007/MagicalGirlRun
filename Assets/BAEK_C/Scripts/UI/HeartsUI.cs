using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    [Header("��Ʈ ������")]
    [SerializeField] private GameObject heartPrefab; // ��Ʈ ������
    [Header("��Ʈ ��ȯ ����")]
    [SerializeField] private RectTransform spawnPoint;

    [Header("��Ʈ ���º� �̹���")]
    public Sprite fullHeart;  // 2ü���� ��
    public Sprite halfHeart;  // 1ü���� ��
    public Sprite emptyHeart; // 0ü���� ��

    private JI_PlayerStats playerStats;
    private List<Image> hearts = new List<Image>(); // ��Ʈ UI �̹��� ����Ʈ
    private void Awake()
    {
        playerStats = FindObjectOfType<JI_PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("�÷��̾� ������ �����ϴ�.");
            return;
        }
    }

    private void Start()
    {
        CreateHearts(); // ��Ʈ UI ����
        UpdateHearts(); // �ʱ�ȭ �� ��Ʈ UI ������Ʈ
    }
    public void CreateHearts()
    {

        foreach (Transform t in spawnPoint) // ��Ʈ UI�� ������ �θ� ������Ʈ�� �ڽ� ������Ʈ�� ��� ����
        {
            Destroy(t.gameObject);
            hearts.Clear();
        }
        // ��Ʈ ���� ��� (�� ��Ʈ�� 2ü��)
        int heartCount = Mathf.CeilToInt(playerStats.MaxHp / 2f);

        //��Ʈ ���� & ��ġ
        for (int i = 0; i < heartCount; i++)
        {
            // ��Ʈ �������� �����ϰ� RectTransform�� spawnPoint�� �ڽ����� ����
            Image img = Instantiate(heartPrefab, spawnPoint).GetComponent<Image>();
            // �θ� ��ġ �������� X�� ������ ����
            img.rectTransform.anchoredPosition = new Vector2(i * 66, 0);

            hearts.Add(img); // ��Ʈ UI �̹��� ����Ʈ�� �߰�
        }
    }
    public void UpdateHearts()
    {
        int currentHp = playerStats.CurrentHp;

        for (int i = 0; i < hearts.Count; i++)
        {
            // i��° ��Ʈ�� 0~1= 1��° ��Ʈ,2~3= 2��° ��Ʈó�� 2ü�� ������ ǥ���ϱ� ������
            // ���� ��Ʈ���� ����� ü��(i*2)�� ���� �� ��Ʈ�� ���� ü���� �����
            int heartHp = currentHp - (i * 2);
            if (heartHp >= 2) hearts[i].sprite = fullHeart;
            else if (heartHp == 1) hearts[i].sprite = halfHeart;
            else if (heartHp == 0) hearts[i].sprite = emptyHeart;
        }
    }
}
