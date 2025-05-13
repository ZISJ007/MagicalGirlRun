
using UnityEngine;

public class PlayerDeleter : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Time.timeScale = 0f;
        }
    }
}
