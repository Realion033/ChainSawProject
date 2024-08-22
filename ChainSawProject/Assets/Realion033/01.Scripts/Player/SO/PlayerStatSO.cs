using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PlayerStatSO")]
public class PlayerStatSO : ScriptableObject
{
    public float playerDamage;
    public float playerHealth;
    public float speed = 5f;

    public float dashDistance = 5f;
    public float dashSpeed = 20f;
}
