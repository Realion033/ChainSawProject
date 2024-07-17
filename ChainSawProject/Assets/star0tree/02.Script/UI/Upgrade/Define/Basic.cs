using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BasicType
{
    None, Health_Up, EnergyRecovery_Up, EnergyChargeAmount_Up,
    DashSpeed_Up, Atk_UP, AtkSpeed_Up,
}

public class Basic : UpgradeUI_Form
{
    protected new BasicType type = BasicType.None;
    protected Basic(BasicType type) : base(FormType.Basic)
    {
        this.type = type;
    }
}
class Health_Up : Basic
{
    public Health_Up() : base(BasicType.Health_Up) { CardSet(); }
    public override Player ChangeInformation(Player player)
    {
        UpGradeCount_UP();
        Debug.Log($"체력 증가 한 횟수 : {UpGradeCount}");
        return player;
    }
}
class EnergyRecovery_Up : Basic
{
    public EnergyRecovery_Up() : base(BasicType.EnergyRecovery_Up) { CardSet(); }
    public override Player ChangeInformation(Player player)
    {
        UpGradeCount_UP();
        Debug.Log($"에너지 충전 속도 증가 한 횟수 : {UpGradeCount}");
        return player;
    }
}
class EnergyChargeAmount_Up : Basic
{
    public EnergyChargeAmount_Up() : base(BasicType.EnergyChargeAmount_Up) { CardSet(); }
    public override Player ChangeInformation(Player player)
    {
        UpGradeCount_UP();
        Debug.Log($"최대 충전량 증가 한 횟수 : {UpGradeCount}");
        return player;
    }
}
class DashSpeed_Up : Basic
{
    public DashSpeed_Up() : base(BasicType.DashSpeed_Up) { CardSet(); }
    public override Player ChangeInformation(Player player)
    {
        switch(upGradeCount)
        {
            case 1:
                player.dashDistance *= 1.2f;
                player.dashSpeed *= 1.2f;
                break;
            case 2:
                player.dashDistance *= 1.4f;
                player.dashSpeed *= 1.4f;
                break;
            case 3:
                player.dashDistance *= 1.6f;
                player.dashSpeed *= 1.6f;
                break;
            case 4:
                player.dashDistance *= 1.8f;
                player.dashSpeed *= 1.8f;
                break;
            case 5:
                player.dashDistance *= 2f;
                player.dashSpeed *= 2f;
                break;
        }
        UpGradeCount_UP();
        Debug.Log($"대쉬 거리 증가 한 횟수 : {UpGradeCount}");
        return player;
    }
}
class Atk_UP : Basic
{
    public Atk_UP() : base(BasicType.Atk_UP) { CardSet(); }
    public override Player ChangeInformation(Player player)
    {
        UpGradeCount_UP();
        Debug.Log($"공격력 증가 한 횟수 : {UpGradeCount}");
        return player;
    }
}
class AtkSpeed_Up : Basic
{
    public AtkSpeed_Up() : base(BasicType.AtkSpeed_Up) { CardSet(); }
    public override Player ChangeInformation(Player player)
    {
        UpGradeCount_UP();
        Debug.Log($"공격속도 증가 한 횟수 : {UpGradeCount}");
        return player;
    }
}
