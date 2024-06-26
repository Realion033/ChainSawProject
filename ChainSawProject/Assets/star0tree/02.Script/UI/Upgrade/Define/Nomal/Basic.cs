using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EfficacyType
{
    None, Health_Up, EnergyRecovery_Up, EnergyChargeAmount_Up,
    DashSpeed_Up, Atk_UP, AtkSpeed_Up,
}

public class Basic : UpgradeUI_Form
{
    protected EfficacyType type = EfficacyType.None;
    protected Basic(EfficacyType type) : base(FormType.Basic)
    {
        this.type = type;
    }
}
class Health_Up : Basic
{
    public Health_Up() : base(EfficacyType.Health_Up)
    {
        Cardset(20, 1, false);
    }
}
class EnergyRecovery_Up : Basic
{
    public EnergyRecovery_Up() : base(EfficacyType.EnergyRecovery_Up)
    {
        Cardset(1.2f, 1, true);
    }
}
class EnergyChargeAmount_Up : Basic
{
    public EnergyChargeAmount_Up() : base(EfficacyType.EnergyChargeAmount_Up)
    {
        Cardset(2, 1, false);
    }
}
class Speed_Up : Basic
{
    public Speed_Up() : base(EfficacyType.DashSpeed_Up)
    {
        Cardset(1.2f, 1, true);
    }
}
class Atk_UP : Basic
{
    public Atk_UP() : base(EfficacyType.Atk_UP)
    {
        Cardset(1.2f, 1, true);
    }
}
class AtkSpeed_Up : Basic
{
    public AtkSpeed_Up() : base(EfficacyType.AtkSpeed_Up)
    {
        Cardset(1.2f, 1, true);
    }
}
