
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossButton : MonoBehaviour
{
    [Header("Boss ��ư ��������Ʈ")]
    public Image button;
    public Sprite unlockSprite;

    [Header("Ű UI ����")]
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
            Debug.LogError("������ �����Ǿ����ϴ�.");
            return;
        }

        button.sprite = unlockSprite;
        key.gameObject.SetActive(false);
    }
}
