using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMonster : MonsterController
{
    public int damage = 1;

    protected override void OnPlayerCatch()
    {
        Debug.Log($"플레이어에게 {damage} 데미지를 입힘");
    }
}