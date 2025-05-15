
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossButton : MonoBehaviour
{
    [Header("Boss 버튼 스프라이트")]
    public Image button;
    public Sprite unlockSprite;

    [Header("키 UI 설정")]
    [SerializeField] private TextMeshProUGUI keyCount;
    [SerializeField] private GameObject key;

    void Start()
    {
        int trueCount = GameSystem.key.Count(k => k);
        Debug.Log(trueCount);

        keyCount.text = $"X {trueCount}/3";
    }

    public void UnlockButton()
    {
        if (button == null || unlockSprite == null || key == null)
        {
            Debug.LogError("참조가 누락되었습니다.");
            return;
        }

        button.sprite = unlockSprite;
        key.gameObject.SetActive(false);
    }
}
