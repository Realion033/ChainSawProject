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
    public virtual Player ChangeInformation(Player player)
    {
        return player;
    }

    public int UpGradeCount
    {
        get { return upGradeCount; }
    }

    protected void UpGradeCount_UP()
    {
        upGradeCount += 1;
    }
}
// 일단 원래 만들어야하는 것 카운트가 5일때 리스트에서 뺴줘야한다.
// 그렇다면 다른 코드에서 직접 해주는것은 불가능 하므로 bool 값을 리턴해줘서 아 아?