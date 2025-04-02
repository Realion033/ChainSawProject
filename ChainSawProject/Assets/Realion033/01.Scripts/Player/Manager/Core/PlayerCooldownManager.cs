using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCooldownManager : MonoSingleton<PlayerCooldownManager>
{
    // 매애ㅐㅐㅐㅐㅐㅐㅐㅐㅐㅐㅐㅐㅐㅐㅐㅐㅐㅐ우 나쁜예시코드
    public AudioClip Rocket;
    public AudioClip Fianl;
    public AudioClip Core;
    public GameObject chosse;
    public AudioSource audioSource;
    public AudioClip sattack;
    public AudioClip sdash;
    public PlayerCooldownSO playerCooldownSO;
    public PlayerInput playerInput;
    public Slider slider;

    public float _dashCool;
    public float _attackEnergeCool;
    public float _attackTick;
    public float _ultmateCool;
    public float _rultmateCool;
    public SkillSetting SkillSetting;

    public Text _tultCool;
    public Text _tdashCool;

    public GameObject _tui;
    public GameObject _tdi;

    private float deAtk;

    private void Start()
    {
        _dashCool = playerCooldownSO.DashCoolDown;
        _attackEnergeCool = playerCooldownSO.AttackMaxEnergyCoolDown;
        _attackTick = playerCooldownSO.AttackTick;
        _ultmateCool = 0;
        _rultmateCool = 15;

        deAtk = _attackEnergeCool;
    }

    private void Update()
    {
        slider.value = _attackEnergeCool / deAtk;
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
    public void setCoolz()
    {
        _ultmateCool = 0;
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

        if (playerInput.isSlash == true)
        {
            if (_attackEnergeCool > 0)
            {
                _attackEnergeCool -= Time.deltaTime;
                if (_attackEnergeCool < 0.1f)
                {
                    playerInput.isSlash = false;
                }
            }
        }
        if (playerInput.isSlash == false)
        {
            if (_attackEnergeCool > 0)
            {
                _attackEnergeCool += Time.deltaTime;
                if (_attackEnergeCool > deAtk) _attackEnergeCool = deAtk;
            }
        }
    }

    public bool UseDash()
    {
        if (_dashCool == 0)
        {
            audioSource.PlayOneShot(sdash);
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

    public bool UseEnery()
    {
        if (_attackEnergeCool >= 0.1f)
        {
            return true;
        }
        return false;
    }
    public void sssss()
    {
        if (!chosse.activeSelf)
        {
            audioSource.PlayOneShot(sattack);
        }
    }

    public bool UseUlt()
    {
        if (_ultmateCool == 0)
        {
            _ultmateCool = _rultmateCool;
            return true;
        }
        return false;
    }

    public void RS()
    {
        audioSource.PlayOneShot(Rocket);
    }
    public void CS()
    {
        audioSource.PlayOneShot(Core);
    }
    public void FS()
    {
        audioSource.PlayOneShot(Fianl);
    }
}