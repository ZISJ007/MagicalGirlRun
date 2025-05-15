using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class JI_GM : MonoBehaviour
{
    public GameObject playerObject;
    private bool isMoving = false;
    public float moveSpeed = 5f;


    public void OnClickToStart()
    {
        isMoving = true; // Ŭ�� �� �̵� ����
        StartCoroutine(LoadSceneDelay(2.5f));
    }

    // delaySeconds �� ��ŭ ��ٷȴٰ� �� �ε�
    private IEnumerator LoadSceneDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene("StageSelectScene");
    }
    private void Update()
    {
        if (isMoving && playerObject != null)
        {
            // �� �����Ӹ��� ���������� moveSpeed��ŭ �̵�
            playerObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }
  
}
