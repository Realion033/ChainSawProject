using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coolset : MonoBehaviour
{
    private PlayerCooldownSO _playerCooldownSO;

    public virtual void Awake()
    {
        _playerCooldownSO = new PlayerCooldownSO();
    }
}
