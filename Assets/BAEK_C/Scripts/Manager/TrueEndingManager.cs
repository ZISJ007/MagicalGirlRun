using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TrueEndingManager : MonoBehaviour
{
    [Header("배경음악")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip trueEndingBGM;

    [Header("UI")]
    [SerializeField] private Image blackPanel;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI storyText;


    [Header("스토리")]
    [SerializeField] private StoryStep[] storySteps;

    [Header("속도조절")]
    [SerializeField] private float textFadeDuration = 1f;
    [SerializeField] private float textDisplayDuration = 3f;
    [SerializeField] private float imageFadeDuration = 1.5f;

 

    [System.Serializable]
    public class StoryStep
    {
        [TextArea(2, 5)]
        public string text;
        public Image illustration;
    }

    private void Start()
    {
        StartCoroutine(PlayTrueEnding());
    }

    private IEnumerator PlayTrueEnding()
    {
        
        bgmSource.clip = trueEndingBGM;
        bgmSource.Play();

        //검은배경이 천천히 사라지면서 뒷 배경등장
        yield return StartCoroutine(FadeImage(blackPanel, 1f, 0f, 5f));//더 서서히 나오게 숫자 수정

        //스토리 라인 순서대로 재생
        foreach (var step in storySteps)
        {
            //텍스트 & 이미지 페이드 인
            yield return StartCoroutine(FadeImage(step.illustration, 0f, 1f, imageFadeDuration));
            yield return StartCoroutine(FadeTextInOut(step.text));

            //텍스트 유지 시간
            yield return new WaitForSeconds(textDisplayDuration);

            //텍스트,이미지 페이드 아웃
            yield return StartCoroutine(FadeTextOut());
            yield return StartCoroutine(FadeImage(step.illustration, 1f, 0f, imageFadeDuration));
        }

     
        

        //크레딧 씬으로
        SceneManager.LoadScene("EndingCredit");
    }

    private IEnumerator FadeTextInOut(string text)
    {
        storyText.text = text;
        Color color = storyText.color;
        color.a = 0f;
        storyText.color = color;

        float elapsed = 0f;
        while (elapsed < textFadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsed / textFadeDuration);
            storyText.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeTextOut()
    {
        Color color = storyText.color;
        float elapsed = 0f;
        while (elapsed < textFadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / textFadeDuration);
            storyText.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeImage(Image img, float fromAlpha, float toAlpha, float duration)
    {
        Color color = img.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            
            float easedT = EaseInOut(t);

            color.a = Mathf.Lerp(fromAlpha, toAlpha, easedT);
            img.color = color;

            yield return null;
        }
    }

    private float EaseInOut(float t)
    {
        return t * t * (3f - 2f * t);  
    }
}
