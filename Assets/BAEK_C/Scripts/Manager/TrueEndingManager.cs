using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TrueEndingManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip trueEndingBGM;

    [SerializeField] private TextMeshProUGUI storyText;
    [SerializeField] private string[] storyLines;
    [SerializeField] private float storyDisplayDuration = 3f;

    [SerializeField] private Image specialIllustration;
    [SerializeField] private float illustrationFadeDuration = 1.5f;

    private void Start()
    {
        StartCoroutine(PlayTrueEndingSequence());
    }

    private IEnumerator PlayTrueEndingSequence()
    {
        // BGM
        bgmSource.clip = trueEndingBGM;
        bgmSource.Play();

        // 스토리 라인 순차 출력
        foreach (var line in storyLines)
        {
            storyText.text = line;
            yield return StartCoroutine(FadeTextInOut(storyText, storyDisplayDuration));
        }

        // 스페셜 일러스트 연출
        yield return StartCoroutine(FadeImageInOut(specialIllustration, illustrationFadeDuration));

        // 다 끝나면 Credit 씬으로 넘어가기
        SceneManager.LoadScene("CreditScene");
    }

    private IEnumerator FadeTextInOut(TextMeshProUGUI text, float duration)
    {
        Color color = text.color;
        color.a = 0f;
        text.color = color;

        // 페이드 인
        float elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsed / 1f);
            text.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(duration);

        // 페이드 아웃
        elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / 1f);
            text.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeImageInOut(Image img, float duration)
    {
        Color color = img.color;
        color.a = 0f;
        img.color = color;

        // 페이드 인
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsed / duration);
            img.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        // 페이드 아웃
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / duration);
            img.color = color;
            yield return null;
        }
    }
}
