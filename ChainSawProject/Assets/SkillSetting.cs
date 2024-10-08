using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSetting : MonoBehaviour
{
    public Skill[] _skill;
    public Text _text;

    public void Rocekt()
    {
        foreach (var item in _skill)
        {
            item._skillEnum = Skills.RocketLuncer;
            _text.text = "Current Skill : ROCKET LUNHCER";
        }
    }

    public void Final()
    {
        foreach (var item in _skill)
        {
            item._skillEnum = Skills.FinalAttack;
            _text.text = "Current Skill : FINAL ATTACK";
        }
    }

    public void Giman()
    {
        foreach (var item in _skill)
        {
            item._skillEnum = Skills.Giman;
            _text.text = "Current Skill : CORE UPGRADE";
        }
    }
}
