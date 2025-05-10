using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemMover : MonoBehaviour
{
    public float moveSpeed = 5; // 아이템 이동 속도

    private GameSystem gameSystem; // 게임 시스템 참조

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime; // 실제 이동
        gameSystem = FindObjectOfType<GameSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision) // 플레이어와 접촉시
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.name.Contains("Coin")) // 코인
            {
                gameSystem.AddScore(+100);
            }
            else if (gameObject.name.Contains("Booster")) // 부스터
            {
                gameSystem.ChangeSpeed(+1f);
            }
            else if (gameObject.name.Contains("Slower")) // 슬로워
            {
                gameSystem.ChangeSpeed(-1f);
            }
            else if (gameObject.name.Contains("Bomb")) // 폭탄
            {
                gameSystem.ChangeLife(-1);
            }
            else if (gameObject.name.Contains("Heart")) // 하트
            {
                gameSystem.ChangeLife(+1);
            }
            else if (gameObject.name.Contains("Key_1")) // 열쇠
            {
                gameSystem.AddKey_1();
            }
            else if (gameObject.name.Contains("Key_2")) // 열쇠
            {
                gameSystem.AddKey_2();
            }
            else if (gameObject.name.Contains("Key_3")) // 열쇠
            {
                gameSystem.AddKey_3();
            }

            Destroy(this.gameObject); // 획득한 아이템 파괴
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Vector3 pos = transform.position;
            pos.y += 0.2f;
            transform.position = pos;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Vector3 pos = transform.position;
            pos.y += 0.5f;
            transform.position = pos;
        }
    }
}
