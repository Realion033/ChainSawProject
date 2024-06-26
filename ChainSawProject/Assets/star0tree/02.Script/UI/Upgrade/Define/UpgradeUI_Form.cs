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
    protected float stat;
    protected int upGradeCount;
    protected bool multiplyOrPlus;
    protected UpgradeUI_Form(FormType type)
    {
        this.type = type;
    }

    protected virtual void Cardset(float stat, int upGradeCount, bool multiplyOrPlus)
    {
        this.stat = stat;
        this.upGradeCount = upGradeCount;
        this.multiplyOrPlus = multiplyOrPlus;
    }

    public float Stat
    {
        get
        {
            return stat;
        }
        set
        {
            if (value < 0) stat = 0;
            else stat = value;
        }
    }

    public int UpGradeCount
    {
        get
        {
            return upGradeCount;
        }
        set
        {
            if (value < 0) upGradeCount = 0;
            else upGradeCount = value;
        }
    }

    public void GradeUp(int stat)
    {
        this.stat += stat;
    }

    public float ApplyInformation(float stat)
    {
        if (multiplyOrPlus)
        {
            this.stat *= stat;
        }
        else
        {
            this.stat += stat;
        }
        upGradeCount++;
        return this.stat;
    }

}
// 그러니깐 타입을 선택 할때 마다 그래드를 업드레이드 해주고
// 와 잠깐만 그럼 그 정보를 다 어디서 저장해? 매 묘능 하다 2차원 배열을 만들어야하나?
// 아니 이상한 생각하지 말고 어디보자
// 줘야 될것 타입 그 타입의 정보 우선 숫자 그리고 사칙 연산 