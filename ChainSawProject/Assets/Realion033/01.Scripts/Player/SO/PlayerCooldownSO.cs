using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PlayerCoolStat")]
public class PlayerCooldownSO : ScriptableObject
{
    public float DashCoolDown = 3f;
    public float AttackMaxEnergyCoolDown = 8f;
    public float AttackTick = 0.8f;
    public float UltmateSkill = 30f;
}
