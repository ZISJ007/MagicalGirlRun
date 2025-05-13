using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JI_GM : MonoBehaviour
{
    public GameObject playerObject;
    private bool isMoving = false;
    public float moveSpeed = 5f;
    public void OnClickToStart()
    {
        isMoving = true; // 클릭 시 이동 시작
        StartCoroutine(LoadSceneDelay(2.5f));
    }

    // delaySeconds 초 만큼 기다렸다가 씬 로드
    private IEnumerator LoadSceneDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene("JI_Scene");
    }
    private void Update()
    {
        if (isMoving && playerObject != null)
        {
            // 매 프레임마다 오른쪽으로 moveSpeed만큼 이동
            playerObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }
}
