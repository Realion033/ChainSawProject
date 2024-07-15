using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FormType
{
    None, Basic, SpecialStat, Special
}

public class UpgradeUI_Form
{
    protected FormType type;
    protected int upGradeCount = 0;
    protected UpgradeUI_Form(FormType type)
    {
        this.type = type;
    }

    protected void CardSet()
    {
        upGradeCount = 1;
    }
    public virtual Player ChangeInformation(Player player, int upGradeCount)
    {
        return player;
    }

    public int UpGradeCount
    {
        get { return upGradeCount; }
    }

    protected int UpGradeCount_UP()
    {
        if(upGradeCount == 5)
        {
            return 0;
        }
        return upGradeCount + 1;
    }
}