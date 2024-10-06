using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Video;

public class MainGameManager : MonoBehaviour
{
    public LivingEntity livinPlayer;
    public Player playerPlayer;
    public GameObject Playerobj;
    
    public Volume Volume;
    private ColorAdjustments _colorAdjustments;

    public LevelSO[] levelSOs;
    private GameObject CurrentLevelObj;
    
    public VideoPlayer videoPlayer;
    
    private int CurrentLevel;
    
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
        
        GameStart();
    }

    private void FixedUpdate()
    {
        float currentHealth = livinPlayer.health;
        float healthPercentage = currentHealth / playerMaxHealth;

        if (currentHealth <= 0)
        {
            GameOver();
        }
            
        _colorAdjustments.saturation.value = Mathf.Lerp(-100, 0, healthPercentage);
        
    }

    private void GameStart()
    {
        CurrentLevelObj = Instantiate(levelSOs[CurrentLevel].level, new Vector3(0, 0, 0), Quaternion.identity);
        livinPlayer.health = playerMaxHealth;
        Playerobj.transform.position = levelSOs[CurrentLevel].SpawnPoints;
    }

    private void GameOver()
    {
        videoPlayer.Play();
        Destroy(CurrentLevelObj);
        GameStart();
    }
}