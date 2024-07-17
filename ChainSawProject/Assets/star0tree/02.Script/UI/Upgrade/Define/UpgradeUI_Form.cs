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
// �ϴ� ���� �������ϴ� �� ī��Ʈ�� 5�϶� ����Ʈ���� ������Ѵ�.
// �׷��ٸ� �ٸ� �ڵ忡�� ���� ���ִ°��� �Ұ��� �ϹǷ� bool ���� �������༭ �� ��?