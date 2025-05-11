using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMonster : MonsterController
{
    protected override void OnPlayerCatch()
    {
        Debug.Log("몬스터 효과");
    }
}
