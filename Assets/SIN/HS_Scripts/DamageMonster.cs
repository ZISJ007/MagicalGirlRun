using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMonster : MonsterController
{
    public int damage = 1;

    protected override void OnPlayerCatch()
    {
        Debug.Log($"�÷��̾�� {damage} �������� ����");
    }
}