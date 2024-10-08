using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCooldownManager : MonoSingleton<PlayerCooldownManager>
{
    public PlayerCooldownSO playerCooldownSO;

    public float _dashCool;
    public float _attackEnergeCool;
    public float _attackTick;
    public float _ultmateCool;

    public Text _tultCool;
    public Text _tdashCool;

    public GameObject _tui;
    public GameObject _tdi;

    protected override void Awake()
    {
        _dashCool = playerCooldownSO.DashCoolDown;
        _attackEnergeCool = playerCooldownSO.AttackMaxEnergyCoolDown;
        _attackTick = playerCooldownSO.AttackTick;
        _ultmateCool = playerCooldownSO.UltmateSkill;   
    }

    private void Update()
    {
        CooltimeManage();

        // "ready"로 표시하거나 소수점 없는 숫자 출력
        if (_ultmateCool <= 0)
        {
            _tultCool.text = "ready";
            _tui.SetActive(false);
        }
        else
        {
            _tultCool.text = $"{Mathf.FloorToInt(_ultmateCool + 1)}";
            _tui.SetActive(true);
        }

        if (_dashCool <= 0)
        {
            _tdashCool.text = "ready";
            _tdi.SetActive(false);
        }
        else
        {
            _tdashCool.text = $"{Mathf.FloorToInt(_dashCool + 1)}";
            _tdi.SetActive(true);
        }

    }

    private void CooltimeManage()
    {
        // Dash cooldown 감소
        if (_dashCool > 0)
        {
            _dashCool -= Time.deltaTime; // 쿨다운을 감소시킴
            if (_dashCool < 0) _dashCool = 0; // 0 이하로 떨어지지 않게 설정
        }

        // AttackTick 감소
        if (_attackTick > 0)
        {
            _attackTick -= Time.deltaTime;
            if (_attackTick < 0) _attackTick = 0;
        }

        // Ultmate cooldown 감소
        if (_ultmateCool > 0)
        {
            _ultmateCool -= Time.deltaTime;
            if (_ultmateCool < 0) _ultmateCool = 0;
        }
    }

    public bool UseDash()
    {
        if (_dashCool == 0)
        {
            _dashCool = playerCooldownSO.DashCoolDown;
            return true;
        }
        return false;
    }

    public bool AttackTick()
    {
        if (_attackTick == 0)
        {
            _attackTick = playerCooldownSO.AttackTick;
            return true;
        }
        return false;
    }

    private bool AttackEnerge()
    {
        throw new NotImplementedException();
    }

    public bool UseUlt()
    {
        if (_ultmateCool == 0)
        {
            _ultmateCool = playerCooldownSO.UltmateSkill;
            return true;
        }
        return false;
    }
}
