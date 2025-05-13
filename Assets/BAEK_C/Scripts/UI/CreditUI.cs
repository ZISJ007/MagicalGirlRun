using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditUI : MonoBehaviour
{
    [Header("크레딧 초기 텍스트")]
    [SerializeField] private TextMeshProUGUI firstText;
    [SerializeField] private float firstDuration = 2f;
    [SerializeField] private float fadeDuration = 1f;

    [Header("크레딧 스크롤")]
    [SerializeField] private RectTransform scrollContent;
    [SerializeField] private float scrollSpeed = 70f;

    [Header("이미지 연출")]
    [SerializeField] private List<CreditImageData> creditImageDatas;
    [SerializeField] private float imageFadeDuration = 1f;
    [SerializeField] private float imageDisplayDuration = 2f;

    
    [SerializeField] private Canvas canvas;
    [SerializeField] private float triggerViewportY = 0.4f; //화면 중간
    [SerializeField] private float tolerance = 0.2f;        //오차 범위
    [SerializeField] private Button skipButton;
    [SerializeField] private float creditEndY = 1500f;   //스크롤 자동 끝나는 부분
    [SerializeField] private float ScrollSpeed = 200f;  //누르고 있을 때 속도
    [SerializeField] private KeyCode accelerateKey = KeyCode.Mouse0;  //왼쪽 마우스 버튼
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

        StartCoroutine(FirstTextStartCredits());
    }

    private void Update()
    {
        if (isScrolling)
        {
            float currentSpeed = Input.GetKey(accelerateKey) ? ScrollSpeed : scrollSpeed;

            scrollContent.anchoredPosition += Vector2.up * currentSpeed * Time.deltaTime;

            CheckShowImages();

            //크레딧 끝나면 자동 이동
            if (scrollContent.anchoredPosition.y >= creditEndY)
            {
                isScrolling = false;
                SceneManager.LoadScene("TitleScene");
            }
        }
    }
    private void CheckShowImages()
    {
        foreach (var data in creditImageDatas)
        {
            //뷰포트 좌표로 변환 (0~1)
            Vector2 viewportPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, data.image.transform.position);
            viewportPos.x /= Screen.width;
            viewportPos.y /= Screen.height;

            float alpha = 0f;

            if (viewportPos.y >= triggerViewportY - tolerance && viewportPos.y <= triggerViewportY + tolerance)
            {
                //중앙에 가까울수록 alpha 1 멀리 벗어날수록 alpha 0
                float t = Mathf.InverseLerp(triggerViewportY - tolerance, triggerViewportY + tolerance, viewportPos.y);
                alpha = 1f - Mathf.Abs(t - 0.5f) * 2f;  // 중앙이면 1, 양쪽 끝으로 갈수록 0
            }

            //부드럽게 보이게
            Color color = data.image.color;
            color.a = Mathf.Lerp(color.a, alpha, Time.deltaTime * imageFadeLerpSpeed);
            data.image.color = color;
        }
    }   
    private IEnumerator FirstTextStartCredits()
    {
        isScrolling = false;

        
        Color color = firstText.color;
        color.a = 0f;
        firstText.color = color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            firstText.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(firstDuration);

      
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            firstText.color = color;
            yield return null;
        }

        isScrolling = true;
    }

    private void SkipCredit()
    {
        isScrolling = false;
        SceneManager.LoadScene("TitleScene"); // 엔딩 후 이동할 씬
    }
}

