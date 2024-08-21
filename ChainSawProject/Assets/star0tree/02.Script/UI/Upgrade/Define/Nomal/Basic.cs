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
    protected BasicType type = BasicType.None;
    protected Basic(BasicType type) : base(FormType.Basic)
    {
        UpGradeCount_UP();
        this.type = type;
    }
}
class Health_Up : Basic
{
    public Health_Up() : base(BasicType.Health_Up) { CardSet(); }

    public override Player ChangeInformation(Player player, int upGradeCount)
    {
        UpGradeCount_UP();
        return player;
    }
}
class EnergyRecovery_Up : Basic
{
    public EnergyRecovery_Up() : base(BasicType.EnergyRecovery_Up) { CardSet(); }
    public override Player ChangeInformation(Player player, int upGradeCount)
    {
        UpGradeCount_UP();
        return player;
    }
}
class EnergyChargeAmount_Up : Basic
{
    public EnergyChargeAmount_Up() : base(BasicType.EnergyChargeAmount_Up) { CardSet(); }
    public override Player ChangeInformation(Player player, int upGradeCount)
    {
        UpGradeCount_UP();
        return player;
    }
}
class DashSpeed_Up : Basic
{
    public DashSpeed_Up() : base(BasicType.DashSpeed_Up) { CardSet(); }
    public override Player ChangeInformation(Player player, int upGradeCount)
    {
        switch(upGradeCount)
        {
            case 1:
                player._playerStat.dashDistance *= 1.2f;
                player._playerStat.dashSpeed *= 1.2f;
                break;
            case 2:
                player._playerStat.dashDistance *= 1.4f;
                player._playerStat.dashSpeed *= 1.4f;
                break;
            case 3:
                player._playerStat.dashDistance *= 1.6f;
                player._playerStat.dashSpeed *= 1.6f;
                break;
            case 4:
                player._playerStat.dashDistance *= 1.8f;
                player._playerStat.dashSpeed *= 1.8f;
                break;
            case 5:
                player._playerStat.dashDistance *= 2f;
                player._playerStat.dashSpeed *= 2f;
                break;
        }
        UpGradeCount_UP();
        return player;
    }
}
class Atk_UP : Basic
{
    public Atk_UP() : base(BasicType.Atk_UP) { CardSet(); }
    public override Player ChangeInformation(Player player, int upGradeCount)
    {
        UpGradeCount_UP();
        return player;
    }
}
class AtkSpeed_Up : Basic
{
    public AtkSpeed_Up() : base(BasicType.AtkSpeed_Up) { CardSet(); }
    public override Player ChangeInformation(Player player, int upGradeCount)
    {
        UpGradeCount_UP();
        return player;
    }
}
