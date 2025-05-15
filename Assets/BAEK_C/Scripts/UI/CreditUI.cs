using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditUI : MonoBehaviour
{
    [Header("ũ���� �ʱ� �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI FinalText;
    [SerializeField] private float firstDuration = 2f;
    [SerializeField] private float fadeDuration = 1f;

    [Header("ũ���� ��ũ��")]
    [SerializeField] private RectTransform scrollContent;
    [SerializeField] private float scrollSpeed = 70f;

    [Header("�̹��� ����")]
    [SerializeField] private List<CreditImageData> creditImageDatas;
    [SerializeField] private float imageFadeDuration = 1f;
    [SerializeField] private float imageDisplayDuration = 2f;

    
    [SerializeField] private Canvas canvas;
    [SerializeField] private float triggerViewportY = 0.4f; //ȭ�� �߰�
    [SerializeField] private float tolerance = 0.2f;        //���� ����
    [SerializeField] private Button skipButton;
    [SerializeField] private float creditEndY = 1500f;   //��ũ�� �ڵ� ������ �κ�
    [SerializeField] private float ScrollSpeed = 200f;  //������ ���� �� �ӵ�
    [SerializeField] private KeyCode accelerateKey = KeyCode.Mouse0;  //���� ���콺 ��ư
    [SerializeField] private float imageFadeLerpSpeed = 2f;
    private bool isScrolling = true;
    

    [System.Serializable]
    public class CreditImageData
    {
        public Image image;
    }

    private void Start()
    {
        skipButton.onClick.AddListener(SkipCredit);

        
        foreach (var data in creditImageDatas)
        {
            Color color = data.image.color;
            color.a = 0f;
            data.image.color = color;
        }

       
    }

    private void Update()
    {
        if (isScrolling)
        {
            float currentSpeed = Input.GetKey(accelerateKey) ? ScrollSpeed : scrollSpeed;

            scrollContent.anchoredPosition += Vector2.up * currentSpeed * Time.deltaTime;

            CheckShowImages();

            //ũ���� ������ �ڵ� �̵�
            if (scrollContent.anchoredPosition.y >= creditEndY)
            {
                isScrolling = false;
                StartCoroutine(FinalyText());
            }
        }
    }
    private void CheckShowImages()
    {
        foreach (var data in creditImageDatas)
        {
            //����Ʈ ��ǥ�� ��ȯ (0~1)
            Vector2 viewportPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, data.image.transform.position);
            viewportPos.x /= Screen.width;
            viewportPos.y /= Screen.height;

            float alpha = 0f;

            if (viewportPos.y >= triggerViewportY - tolerance && viewportPos.y <= triggerViewportY + tolerance)
            {
                //�߾ӿ� �������� alpha 1 �ָ� ������� alpha 0
                float t = Mathf.InverseLerp(triggerViewportY - tolerance, triggerViewportY + tolerance, viewportPos.y);
                alpha = 1f - Mathf.Abs(t - 0.5f) * 2f;  // �߾��̸� 1, ���� ������ ������ 0
            }

            //�ε巴�� ���̰�
            Color color = data.image.color;
            color.a = Mathf.Lerp(color.a, alpha, Time.deltaTime * imageFadeLerpSpeed);
            data.image.color = color;
        }
    }   
    private IEnumerator FinalTextCredits()
    {
        isScrolling = false;

        
        Color color = FinalText.color;
        color.a = 0f;
        FinalText.color = color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            FinalText.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(firstDuration);

      
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            FinalText.color = color;
            yield return null;
        }

        
    }
    private IEnumerator FinalyText()
    {
        
        yield return StartCoroutine(FinalTextCredits());
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("TitleScene");
    }
    private void SkipCredit()
    {
        isScrolling = false;
        SceneManager.LoadScene("TitleScene"); 
    }
}

