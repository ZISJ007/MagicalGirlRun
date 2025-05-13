
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeleter : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //시간 멈추고 결과 UI출력
            
            //로드씬은 임시
            SceneManager.LoadScene("Scenes/StageSelectScene");
        }
    }
}
