using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    protected bool isInitialized = false;

    //ÃÊ±âÈ­
    public virtual void Init()
    {
        if (isInitialized) return;
        isInitialized = true;
    }

   
    // UI Ç¥½Ã
    public virtual void Show()
    {
        gameObject.SetActive(true);
        OnOpen();
    }

    //UI ¼û±â±â
    public virtual void Hide()
    {
        OnClose();
        gameObject.SetActive(false);
    }

   //¿­¸±¶§
    protected virtual void OnOpen() { }

   //´ÝÈú¶§
    protected virtual void OnClose() { }
}

