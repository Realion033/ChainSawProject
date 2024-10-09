using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Video;

public class MainGameManager : MonoBehaviour
{
    public PlayerStatSO _playerStat;
    public LivingEntity livinPlayer;
    public Player playerPlayer;
    public GameObject Playerobj;

    public Volume Volume;
    private ColorAdjustments _colorAdjustments;

    public LevelSO[] levelSOs;
    private GameObject CurrentLevelObj;

    public VideoPlayer videoPlayer;

    public int CurrentLevel;

    private float playerMaxHealth;

    public GameObject GameClearflag;

    private Transform _playerT;
    private PlayerDash _player;

    private void Awake()
    {
        playerMaxHealth = _playerStat.playerHealth;
        Volume.profile.TryGet(out _colorAdjustments);

        _playerT = GameObject.FindGameObjectWithTag("KPlayer").transform;
        _player = _playerT.GetComponent<PlayerDash>();

        GameStart();
    }

    private void FixedUpdate()
    {
        float currentHealth = livinPlayer.health;
        float healthPercentage = currentHealth / playerMaxHealth;

        _colorAdjustments.saturation.value = Mathf.Lerp(-100, 0, healthPercentage);
    }

    private void GameStart()
    {
        CurrentLevelObj = Instantiate(levelSOs[CurrentLevel].level, Vector3.zero, Quaternion.identity);
        //livinPlayer.health = playerMaxHealth;
        _player.isDashing = false;
        Playerobj.transform.position = levelSOs[CurrentLevel].SpawnPoints;
    }

    public void GameOver()
    {
        videoPlayer.Play();
        playerPlayer.health = 100;
        Destroy(CurrentLevelObj);
        GameStart();
    }

    public void Thanks()
    {
        GameClearflag.SetActive(true);
        Time.timeScale = 0;
        _player.isDashing = false;
        StartCoroutine(ShotDownWait());
    }

    private IEnumerator ShotDownWait()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
