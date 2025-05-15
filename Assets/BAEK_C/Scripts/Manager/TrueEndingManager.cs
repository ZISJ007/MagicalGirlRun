using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TrueEndingManager : MonoBehaviour
{
    [Header("�������")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioClip trueEndingBGM;

    [Header("UI")]
    [SerializeField] private Image blackPanel;
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI storyText;


    [Header("���丮")]
    [SerializeField] private StoryStep[] storySteps;

    [Header("�ӵ�����")]
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

        //��������� õõ�� ������鼭 �� ������
        yield return StartCoroutine(FadeImage(blackPanel, 1f, 0f, 5f));//�� ������ ������ ���� ����

        //���丮 ���� ������� ���
        foreach (var step in storySteps)
        {
            //�ؽ�Ʈ & �̹��� ���̵� ��
            yield return StartCoroutine(FadeImage(step.illustration, 0f, 1f, imageFadeDuration));
            yield return StartCoroutine(FadeTextInOut(step.text));

            //�ؽ�Ʈ ���� �ð�
            yield return new WaitForSeconds(textDisplayDuration);

            //�ؽ�Ʈ,�̹��� ���̵� �ƿ�
            yield return StartCoroutine(FadeTextOut());
            yield return StartCoroutine(FadeImage(step.illustration, 1f, 0f, imageFadeDuration));
        }


        yield return StartCoroutine(FadeImage(blackPanel, 0f, 1f, 2f));

        //ũ���� ������
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
