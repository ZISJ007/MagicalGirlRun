using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerUIManager : MonoBehaviour
{
    //플레이어가 피격시 추가 UI 뜨게하는 스크립트
    [Header("UI설정")] 
    [SerializeField]private TextMeshProUGUI managerText;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image managerImage;
    
    [Header("메세지 리스트")]
    [SerializeField] private List<string> managerMessage;
    
    [Header("UI애니메이션 설정")]
    [SerializeField]private float fadeDuration=0.5f;
    [SerializeField] private float textDisplayDuration = 1.5f;
    [Header("피격시 나올 확률(0~1까지, 1이 100%)"),Space(10f)]
    [SerializeField] private float randomShowText;
    
    private Coroutine displayCoroutine;

    private void Start()
    {
        if(canvasGroup==null)return;
        canvasGroup.alpha = 0;
    }

    public void TryShowRadomText()
    {
        if (Random.value <= randomShowText && managerMessage.Count > 0)
        {
            ShowRadomText();
        }
    }

    private void ShowRadomText()
    {
        string _randomMessage = managerMessage[Random.Range(0, managerMessage.Count)];
        managerText.text = _randomMessage;
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine);
        }

        displayCoroutine = StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        managerText.gameObject.SetActive(true);
        yield return StartCoroutine(FadeCanvasGroup(0f,1f,fadeDuration));
        yield return new WaitForSeconds(textDisplayDuration);
        yield return StartCoroutine(FadeCanvasGroup(1f,0f,fadeDuration));
        managerText.gameObject.SetActive(false);
    }

    private IEnumerator FadeCanvasGroup(float _from, float _to, float _duration)
    {
        float currentTime = 0f;
        canvasGroup.alpha = _from;

        while (currentTime<_duration)
        {
            currentTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(_from,_to,currentTime / _duration);
            yield return null;
        }
        canvasGroup.alpha = _to;
    }
}
