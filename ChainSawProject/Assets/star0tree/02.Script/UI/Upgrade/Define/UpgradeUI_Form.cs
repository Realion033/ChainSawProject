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
// �׷��ϱ� Ÿ���� ���� �Ҷ� ���� �׷��带 ���巹�̵� ���ְ�
// �� ��� �׷� �� ������ �� ��� ������? �� ���� �ϴ� 2���� �迭�� �������ϳ�?
// �ƴ� �̻��� �������� ���� �����
// ��� �ɰ� Ÿ�� �� Ÿ���� ���� �켱 ���� �׸��� ��Ģ ���� 