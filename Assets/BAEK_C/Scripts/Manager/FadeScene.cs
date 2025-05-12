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
    public float fadeDuration = 2f;      //씬이 서서히 나타나는 시간
    public BadEndingManager badEndingManager;
    void Start()
    {
        if (badEndingManager != null && badEndingManager.Button != null)
        {
            badEndingManager.Button.interactable = false;
        }
        StartCoroutine(FadeIn());
    }

    //UI서서히 나타나게 
    IEnumerator FadeIn()
    {
        //모든 이미지와 텍스트의 알파값 0
        SetUIAlpha(0f);

        //각 패널추가하고 알파값 0
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

            //알파값을 증가
            SetUIAlpha(alphaValue);

            //패널 알파값도 증가
            foreach (GameObject panel in panels)
            {
                CanvasGroup panelCanvasGroup = panel.GetComponent<CanvasGroup>();
                panelCanvasGroup.alpha = alphaValue;
            }

            yield return null;
        }

        //마무리 알파값을 1 
        SetUIAlpha(1f);

        // 모든패널 알파값 1
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
        //  이미지들 알파값 변경
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

        // 클릭하면 타이틀로 가는 이벤트 등록
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScene");  //씬 이름
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

        // 기존 UI 페이드아웃 & 블랙패널 페이드인
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            //이미지도지움
            foreach (Image img in images)
            {
                Color color = img.color;
                color.a = 1f - alpha;
                img.color = color;
            }

            //텍스트도 사라짐
            foreach (TextMeshProUGUI txt in textUI)
            {
                Color color = txt.color;
                color.a = 1f - alpha;
                txt.color = color;
            }

            //패널도 지움
            foreach (GameObject panel in panels)
            {
                CanvasGroup panelCanvasGroup = panel.GetComponent<CanvasGroup>();
                if (panelCanvasGroup != null)
                    panelCanvasGroup.alpha = 1f - alpha;
            }

            // 검은패널은 점점 나타남
            blackPanelGroup.alpha = alpha;
        
            
            yield return null;
        }

        //다지우고 검은패널
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

        // 엔딩 텍스트 띄우기
        StartCoroutine(EndingText());
    }

}
