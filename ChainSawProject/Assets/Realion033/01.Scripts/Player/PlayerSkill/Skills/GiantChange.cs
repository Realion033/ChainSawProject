using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantChange : Skill
{
    public Transform player;

    private void Update()
    {
        if (_skillEnum == Skills.GiantChange)
        {
            if (PlayerCooldownManager.Instance.UseUlt())
            {
                UltAttack();
            }
        }
    }

    private void UltAttack()
    {
    
    }
}
