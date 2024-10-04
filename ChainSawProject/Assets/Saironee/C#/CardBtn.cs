using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardBtn : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _text;
    
    public Card _card;
    
    private short _rank;
    
    private void Awake()
    {
        if (_card == null)
        {
            _icon.color = Color.clear;
            _background.color = Color.clear;
            _text.text = "";

            Debug.Log("Card is Null!");
        }
        _rank = _card.level;
        _icon.sprite = _card.icon[_rank];
        _background.sprite = _card.bg.bg[_rank];
        _text.text = _card.discription;

    }
}
