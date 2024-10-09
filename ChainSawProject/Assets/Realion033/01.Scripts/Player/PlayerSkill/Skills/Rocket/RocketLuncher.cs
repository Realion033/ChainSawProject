using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLuncher : Skill
{
    [SerializeField] public LayerMask WhatisEnemy;
    [SerializeField] private GameObject Rocket;
    public float rocketSpeed = 20f;
    public float LuncherDamage = 150f;
    public float Distace = 3f;

    //private float speed = 8f;

    public override void Init()
    {
        base.Init();
        _playerInput.isSkillUse += HandleSkillUse;
    }

    public override void HandleSkillUse()
    {
        if (_skillEnum == Skills.RocketLuncer)
        {
            if (PlayerCooldownManager.Instance.UseUlt())
            {
                PlayerCooldownManager.Instance.RS();
                RocketAttack();
            }
        }
    }

    private void RocketAttack()
    {
        // 마우스의 화면 좌표를 가져옴
        Vector3 mousePosition = Input.mousePosition;

        // 카메라의 Z 깊이를 기준으로 마우스 위치를 월드 좌표로 변환
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.z * -1));

        // 방향 계산 (로켓의 위치에서 마우스 위치로 가는 벡터를 정규화하여 방향만 남김)
        Vector3 direction = (mousePosition - transform.position).normalized;

        // 로켓이 바라볼 방향을 계산
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 방향을 각도로 변환

        // 로켓 인스턴스화 (Quaternion.Euler를 사용해 로켓의 회전을 설정)
        GameObject rocketInstance = Instantiate(Rocket, transform.position, Quaternion.Euler(0f, 0f, angle));

        // 로켓의 Rigidbody2D를 가져와 속도를 설정
        Rigidbody2D rb = rocketInstance.GetComponent<Rigidbody2D>();
        if (rb != null)
        { // 로켓의 일정한 속도
            rb.velocity = direction * rocketSpeed; // 일정한 속도로 방향을 설정
        }
    }
}
