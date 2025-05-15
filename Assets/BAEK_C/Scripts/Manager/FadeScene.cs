using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FadeScene : MonoBehaviour
{
    public GameObject BlackPanel;
    public TextMeshProUGUI endingText;
    public Image endingImage;
    public Image endingImage1;




    public Image[] images;               
    public TextMeshProUGUI[] textUI;     
    public GameObject[] panels;          
    public float fadeDuration = 2f;      //���� ������ ��Ÿ���� �ð�
    public BadEndingManager badEndingManager;
    void Start()
    {
        if (badEndingManager != null && badEndingManager.Button != null)
        {
            badEndingManager.Button.interactable = false;
        }
        StartCoroutine(FadeIn());
    }

    //UI������ ��Ÿ���� 
    IEnumerator FadeIn()
    {
        //��� �̹����� �ؽ�Ʈ�� ���İ� 0
        SetUIAlpha(0f);

        //�� �г��߰��ϰ� ���İ� 0
        foreach (GameObject panel in panels)
        {
            CanvasGroup panelCanvasGroup = panel.GetComponent<CanvasGroup>();
            if (panelCanvasGroup == null)
            {
                panelCanvasGroup = panel.AddComponent<CanvasGroup>();
            }
            panelCanvasGroup.alpha = 0f;
            
        }

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alphaValue = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            //���İ��� ����
            SetUIAlpha(alphaValue);

            //�г� ���İ��� ����
            foreach (GameObject panel in panels)
            {
                CanvasGroup panelCanvasGroup = panel.GetComponent<CanvasGroup>();
                panelCanvasGroup.alpha = alphaValue;
            }

            yield return null;
        }

        //������ ���İ��� 1 
        SetUIAlpha(1f);

        // ����г� ���İ� 1
        foreach (GameObject panel in panels)
        {
            CanvasGroup panelCanvasGroup = panel.GetComponent<CanvasGroup>();
            panelCanvasGroup.alpha = 1f;
            
        }
        if (badEndingManager != null && badEndingManager.Button != null)
        {
            
            badEndingManager.Button.interactable = true;           
        }
    }

    private void SetUIAlpha(float alpha)
    {
        //  �̹����� ���İ� ����
        foreach (Image img in images)
        {
            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }

        foreach (TextMeshProUGUI txt in textUI)
        {
            Color color = txt.color;
            color.a = alpha;
            txt.color = color;
        }

    }
    IEnumerator EndingText()
    {
        float duration = 1f;
        float elapsed = 0f;

        endingText.gameObject.SetActive(true);
        Color color = endingText.color;
        color.a = 0f;
        endingText.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / duration);
            color.a = alpha;
            endingText.color = color;

            yield return null;
        }

        // Ŭ���ϸ� Ÿ��Ʋ�� ���� �̺�Ʈ ���
        StartCoroutine(ClickGoTitle());
    }

    IEnumerator ClickGoTitle()
    {    
       bool clicked = false;

        while (!clicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clicked = true;
            }
            yield return null;
        }      
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");  //�� �̸�
    }
    public void StartFadeOutToBlack()
    {
        StartCoroutine(FadeOutToBlack());
    }

    IEnumerator FadeOutToBlack()
    {
        float fadeDuration = 2f;
        float elapsedTime = 0f;

        endingImage.gameObject.SetActive(true);
        endingImage1.gameObject.SetActive(true);
        CanvasGroup blackPanelGroup = BlackPanel.GetComponent<CanvasGroup>();
        if (blackPanelGroup == null)
            blackPanelGroup = BlackPanel.AddComponent<CanvasGroup>();

        blackPanelGroup.alpha = 0f;
        BlackPanel.SetActive(true);

        // ���� UI ���̵�ƿ� & ���г� ���̵���
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            //�̹���������
            foreach (Image img in images)
            {
                Color color = img.color;
                color.a = 1f - alpha;
                img.color = color;
            }

            //�ؽ�Ʈ�� �����
            foreach (TextMeshProUGUI txt in textUI)
            {
                Color color = txt.color;
                color.a = 1f - alpha;
                txt.color = color;
            }

            //�гε� ����
            foreach (GameObject panel in panels)
            {
                CanvasGroup panelCanvasGroup = panel.GetComponent<CanvasGroup>();
                if (panelCanvasGroup != null)
                    panelCanvasGroup.alpha = 1f - alpha;
            }

            // �����г��� ���� ��Ÿ��
            blackPanelGroup.alpha = alpha;
        
            
            yield return null;
        }

        //������� �����г�
        foreach (Image img in images)
        {
            Color color = img.color;
            color.a = 0f;
            img.color = color;
        }

        foreach (TextMeshProUGUI txt in textUI)
        {
            Color color = txt.color;
            color.a = 0f;
            txt.color = color;
        }

        foreach (GameObject panel in panels)
        {
            CanvasGroup panelCanvasGroup = panel.GetComponent<CanvasGroup>();
            if (panelCanvasGroup != null)
                panelCanvasGroup.alpha = 0f;
        }

        blackPanelGroup.alpha = 1f;

        // ���� �ؽ�Ʈ ����
        StartCoroutine(EndingText());
    }

}
