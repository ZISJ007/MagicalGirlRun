using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JI_PointerManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("마우스 커서 감지 시 활성화할 오브젝트")]
    public GameObject toolTipObject;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (toolTipObject != null)
            toolTipObject.SetActive(true);
    }

    // 마우스 내리면 툴팁 숨기기
    public void OnPointerExit(PointerEventData eventData)
    {
        if (toolTipObject != null)
            toolTipObject.SetActive(false);
    }
}
