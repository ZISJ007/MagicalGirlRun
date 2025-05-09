using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    // 생성될 아이템 리스트
    [SerializeField]private List<GameObject> items;

    // 스탯시스템 참조
    private GameSystem gameSystem;

    private void OnTriggerEnter2D(Collider2D collision) // 플레이어와 접촉시
    {
        if (collision.CompareTag("Player"))
        {
            if(gameObject.name.Contains("Coin")) // 코인
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
}
