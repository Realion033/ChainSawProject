using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Skill[] _skill;
    private PlayerPhysics _playerPhysics;
    private Vector2 moveVelocity;

    public Vector3 mousePos { get; private set; }
    public bool isDash { get; private set; } = false;
    public bool isSlash { get; private set; } = false;
    public bool isLeft { get; private set; } = false;

    public event Action isSkillUse;

    private bool isEsc = false;
    [SerializeField] GameObject SettingUI;

    private void Awake()
    {
        _playerPhysics = GetComponent<PlayerPhysics>();
    }

    void Update()
    {
        ProcessInput();
        DashInput();
        AttackInput();
        AirControlInput();
        SettingInput();
    }

    private void SettingInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isEsc = !isEsc;  // Toggle the state of isEsc
            SettingUI.SetActive(isEsc);  // Activate or deactivate the UI
        }
    }

    private void AirControlInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            isLeft = false;
        }
    }

    private void AttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSlash = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isSlash = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            isSkillUse?.Invoke();
        }
    }

    private void DashInput()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerCooldownManager.Instance.UseDash())
            {
                isDash = true;
            }
        }
        else
        {
            isDash = false;
        }
    }

    private void ProcessInput()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        moveVelocity = new Vector2(x, y);
    }

    public Vector2 GetMoveVelocity()
    {
        return moveVelocity;
    }
}
