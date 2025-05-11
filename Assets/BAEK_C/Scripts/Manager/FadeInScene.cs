using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeInScene : MonoBehaviour
{
    public Image[] images;               // UI 이미지들 (배경 이미지 등)
    public TextMeshProUGUI[] textUI;     // UI 텍스트들 (대사, 이름 등)
    public GameObject[] panels;          // 여러 대화 패널들
    public float fadeDuration = 2f;      // 전체 씬이 서서히 나타나는 시간
    public BadEndingManager badEndingManager;
    void Start()
    {
        // 씬이 로드되면 모든 UI 요소의 알파값을 0으로 설정하여 숨기고, 서서히 보이게 한다.
        StartCoroutine(FadeIn());
    }

    // 씬의 UI 요소를 서서히 나타나게 하는 함수
    IEnumerator FadeIn()
    {
        // 모든 이미지와 텍스트의 알파값을 0으로 설정 (투명)
        SetUIAlpha(0f);

        // 각 패널에 대해 CanvasGroup을 추가하고 알파값을 0으로 설정
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

            // 각 UI 요소의 알파값을 서서히 증가시킴
            SetUIAlpha(alphaValue);

            // 각 패널의 알파값도 서서히 증가시킴
            foreach (GameObject panel in panels)
            {
                CanvasGroup panelCanvasGroup = panel.GetComponent<CanvasGroup>();
                panelCanvasGroup.alpha = alphaValue;
            }

            yield return null;
        }

        // 최종적으로 알파값을 1로 설정 (완전히 보이게)
        SetUIAlpha(1f);

        // 모든 패널의 알파값도 1로 설정
        foreach (GameObject panel in panels)
        {
            CanvasGroup panelCanvasGroup = panel.GetComponent<CanvasGroup>();
            panelCanvasGroup.alpha = 1f;
        }
    }

    // 모든 UI 요소의 알파값을 설정하는 함수
    private void SetUIAlpha(float alpha)
    {
        // 이미지들 알파값 변경
        foreach (Image img in images)
        {
            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }

        // 텍스트들 알파값 변경
        foreach (TextMeshProUGUI txt in textUI)
        {
            Color color = txt.color;
            color.a = alpha;
            txt.color = color;
        }

    }
}
