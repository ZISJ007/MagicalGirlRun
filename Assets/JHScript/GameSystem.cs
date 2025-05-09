using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Player player;

    public void ChangeSpeed(float amount) // ¼Óµµ Áõ°¨
    {
        player.speed += amount;
        Debug.Log($"¼Óµµ {amount}");
    }

    public void ChangeLife(int amount) // Ã¼·Â Áõ°¨
    {
        player.life += amount;
        Debug.Log($"Ã¼·Â {amount}");
    }

    public void AddScore(int amount) // Á¡¼ö Áõ°¨
    {
        StageManager.stageScore += amount;
        Debug.Log($"È¹µæ Á¡¼ö {StageManager.stageScore}");
    }

    public void AddKey_1() // Å° 1~3 Áö±Þ
    {
        player.key_1 = true;
        Debug.Log($"key_1 È¹µæ");
    }
    public void AddKey_2()
    {
        player.key_2 = true;
        Debug.Log($"key_2 È¹µæ");
    }
    public void AddKey_3()
    {
        player.key_3 = true;
        Debug.Log($"key_3 È¹µæ");
    }
}
