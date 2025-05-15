using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JI_PointerManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("���콺 Ŀ�� ���� �� Ȱ��ȭ�� ������Ʈ")]
    public GameObject toolTipObject;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (toolTipObject != null)
            toolTipObject.SetActive(true);
    }

    // ���콺 ������ ���� �����
    public void OnPointerExit(PointerEventData eventData)
    {
        if (toolTipObject != null)
            toolTipObject.SetActive(false);
    }
}
