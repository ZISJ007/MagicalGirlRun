using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    private QuestManager questManager; // 퀘스트 매니저 참조
    private GameSystem gameSystem; // 게임 시스템 참조
    private JI_ResourceController player; // 리소스 컨트롤러 참조

    void Update()
    {
        transform.position += Vector3.left * GameSystem.speed * Time.deltaTime; // 실제 이동
        gameSystem = FindObjectOfType<GameSystem>();
        questManager = FindObjectOfType<QuestManager>();
        player = FindObjectOfType<JI_ResourceController>();
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
                gameSystem.ChangeSpeed(+2f, 5f);
            }
            else if (gameObject.name.Contains("Slower")) // 슬로워
            {
                gameSystem.ChangeSpeed(-1.5f, 5f);
            }
            else if (gameObject.name.Contains("Bomb")) // 폭탄
            {
                player.TakeDamage(1);
            }
            else if (gameObject.name.Contains("Heart")) // 하트
            {
                player.Heal(1);
            }

            else if (gameObject.name.Contains("QuestItem")) // 퀘스트 아이템
            {
                questManager.GetQuestItem();
            }

            Destroy(this.gameObject); // 획득한 아이템 파괴
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ItemObstacle"))
        {
            Vector3 pos = transform.position;
            pos.y += 0.2f;
            transform.position = pos;
        }
    }
}
