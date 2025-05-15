using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    protected bool isInitialized = false;

    //�ʱ�ȭ
    public virtual void Init()
    {
        if (isInitialized) return;
        isInitialized = true;
    }

   
    // UI ǥ��
    public virtual void Show()
    {
        gameObject.SetActive(true);
        OnOpen();
    }

    //UI �����
    public virtual void Hide()
    {
        OnClose();
        gameObject.SetActive(false);
    }

   //������
    protected virtual void OnOpen() { }

   //������
    protected virtual void OnClose() { }
}

