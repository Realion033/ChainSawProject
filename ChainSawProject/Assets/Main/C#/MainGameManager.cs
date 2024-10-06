using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MainGameManager : MonoBehaviour
{
    public LivingEntity livinPlayer;
    public Player playerPlayer;
    public Volume Volume;
    private ColorAdjustments _colorAdjustments;
    
    private float playerMaxHealth;

    private void Awake()
    {
        playerMaxHealth = playerPlayer._playerStat.playerHealth;

        if (Volume.profile.TryGet<ColorAdjustments>(out _colorAdjustments))
        {
            Debug.Log("Color Adjustments 적용됨");
        }
        else
        {
            Debug.LogError("Color Adjustments를 찾을 수 없음!");
        }
    }

    private void FixedUpdate()
    {
        float currentHealth = livinPlayer.health;
        float healthPercentage = currentHealth / playerMaxHealth;

        _colorAdjustments.saturation.value = Mathf.Lerp(-100, 0, healthPercentage);
        
        
    }
}