using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBtn : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _text;
    
    [SerializeField] private Card _card;
    
    private short _rank;
    
    private void Awake()
    {
        _rank = _card.rank;
        _icon.sprite = _card.icon[_rank];
        _background.sprite = _card.bg.bg[_rank];
        _text.text = _card.discription;
    }

    // public void SetAlpha(bool bOnoff)
    // {
    //     if (bOnoff)
    //     {
    //         
    //         _icon.color = new Color32(255, 255, 255, 0);
    //     }
    // }
}
