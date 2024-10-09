using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CurrentS
{
    _f,
    _r,
    _c
}

public class SkillSetting : MonoBehaviour
{
    public float _finalAttackCool;
    public float _rocketCool;
    public float _coreUpgradeCool;
    public PlayerCooldownManager Playerm;
    public Skill[] _skill;
    public Sprite _f;
    public Sprite _r;
    public Sprite _c;
    public Text _text;
    public Image _Ult;


    public void Rocekt()
    {
        Playerm._ultmateCool = _rocketCool;
        Playerm._rultmateCool = _rocketCool;
        foreach (var item in _skill)
        {
            item._skillEnum = Skills.RocketLuncer;
            _text.text = "Current Skill : ROCKET LUNHCER";
            _Ult.sprite = _r;
        }
        Playerm.setCoolz();
    }

    public void Final()
    {
        Playerm._ultmateCool = _finalAttackCool;
        Playerm._rultmateCool = _finalAttackCool;
        foreach (var item in _skill)
        {
            item._skillEnum = Skills.FinalAttack;
            _text.text = "Current Skill : FINAL ATTACK";
            _Ult.sprite = _f;
        }
        Playerm.setCoolz();
    }

    public void Giman()
    {
        Playerm._ultmateCool = _coreUpgradeCool;
        Playerm._rultmateCool = _coreUpgradeCool;
        foreach (var item in _skill)
        {
            item._skillEnum = Skills.Giman;
            _text.text = "Current Skill : CORE UPGRADE";
            _Ult.sprite = _c;
        }
        Playerm.setCoolz();
    }
}
