using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    //플레이어가 피격시 추가 UI 뜨게하는 스크립트
    [Header("랜덤으로 나올 텍스트")] 
    [SerializeField] private List<string> managerMessage;
    [SerializeField]private TextMeshProUGUI managerText;
}
