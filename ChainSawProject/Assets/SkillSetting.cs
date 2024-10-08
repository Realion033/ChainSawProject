using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSetting : MonoBehaviour
{
    [SerializeField] private float _finalAttackCool;
    [SerializeField] private float _rocketCool;
    [SerializeField] private float _coreUpgradeCool;
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
        foreach (var item in _skill)
        {
            item._skillEnum = Skills.RocketLuncer;
            _text.text = "Current Skill : ROCKET LUNHCER";
            _Ult.sprite = _r;
        }
    }

    public void Final()
    {
        Playerm._ultmateCool = _finalAttackCool;
        foreach (var item in _skill)
        {
            item._skillEnum = Skills.FinalAttack;
            _text.text = "Current Skill : FINAL ATTACK";
            _Ult.sprite = _f;
        }
    }

    public void Giman()
    {
        Playerm._ultmateCool = _coreUpgradeCool;
        foreach (var item in _skill)
        {
            item._skillEnum = Skills.Giman;
            _text.text = "Current Skill : CORE UPGRADE";
            _Ult.sprite = _c;
        }
    }
}
