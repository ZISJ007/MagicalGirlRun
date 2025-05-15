using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BadEndingManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public FadeScene fadeInScene;
    public TextMeshProUGUI leftNameText;
    public TextMeshProUGUI rightNameText;
    public TextMeshProUGUI badstoryText;
    private bool isButtonEnd = false;
    public GameObject Panel;
    public GameObject character;
    public Button Button;

    public List<BadStoryLine> badstoryline;
    
    private Animator animator;
    
    private int currentIndex = 0;

    void Start()
    {
        animator = character.GetComponent<Animator>();
        Time.timeScale = 1;
        Button.onClick.AddListener(NextLine);
    }

    public void ShowLine()
    {
        if (currentIndex >= badstoryline.Count)
        {
            EndDialogue();
            return;
        }

        if (badstoryline[currentIndex].isCharacterChange)
        {
            Change();
        }

        BadStoryLine line = badstoryline[currentIndex];

        // �̸� �ؽ�Ʈ ����
        if (line.speaker == "Left")
        {
            leftNameText.text = line.speakerName; // ���� ĳ���� �̸�
            rightNameText.text = ""; // ������ ĳ���� �̸��� �����
        }
        else if (line.speaker == "Right")
        {
            leftNameText.text = ""; // ���� ĳ���� �̸��� �����
            rightNameText.text = line.speakerName; // ������ ĳ���� �̸�
        }

        badstoryText.text = line.badstoryText; // ��� �ؽ�Ʈ

        Panel.SetActive(true); // ��ȭ �г� Ȱ��ȭ
        if (bgmSource != null)
        {
            bgmSource.Stop();

            if (line.bgmClip != null)
            {
                bgmSource.clip = line.bgmClip;
                bgmSource.loop = false; // �ʿ��ϸ� true��
                bgmSource.Play();
            }
        }
    }

    private void Change()
    {
        animator.SetTrigger("Change");
    }

    // ��� ����
    void NextLine()
    {
        currentIndex++;
        ShowLine();
    }

    // ��ȭ ����
    void EndDialogue()
    {
        if (isButtonEnd) return;
        isButtonEnd = true;

        if (Button != null)
            Button.interactable = false; //�ߺ����� �׳� ��ư �ƿ� ������
        Panel.SetActive(false);

        if (fadeInScene != null)
            fadeInScene.StartFadeOutToBlack();
    }
}

[System.Serializable]
public class BadStoryLine
{
    public bool isCharacterChange = false;
    public string speaker; // � ĳ���Ͱ� ���� �ϴ��� (Left,Right)
    public string speakerName; // ���ϴ� ����� �̸�
    public string badstoryText; // ��� ����
    public AudioClip bgmClip;
}