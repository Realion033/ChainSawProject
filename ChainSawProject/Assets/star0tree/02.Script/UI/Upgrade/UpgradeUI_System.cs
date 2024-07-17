using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class UpgradeUI_System : MonoBehaviour
{
    [SerializeField] private Player player;
    private Basic Health_Up = new Health_Up();
    private Basic EnergyRecovery_Up = new EnergyRecovery_Up();
    private Basic EnergyChargeAmount_Up = new EnergyChargeAmount_Up();
    private Basic DashSpeed_Up = new DashSpeed_Up();
    private Basic Atk_UP = new Atk_UP();
    private Basic AtkSpeed_Up = new AtkSpeed_Up();

    private SpecialStat specialStat = null;

    private Special special = null;

    private int TypeNomber;
    //Image
    [SerializeField] private Image form1;
    [SerializeField] private Image form2;
    [SerializeField] private Image form3;
    [SerializeField] private Image form4;
    [SerializeField] private Image form5;
    [SerializeField] private Sprite Health_UpImage;
    [SerializeField] private Sprite EnergyRecovery_UpImage;
    [SerializeField] private Sprite EnergyChargeAmount_UpImage;
    [SerializeField] private Sprite DashSpeed_UpImage;
    [SerializeField] private Sprite Atk_UPImage;
    [SerializeField] private Sprite AtkSpeed_UpImage;
    [SerializeField] private Sprite None;
    BasicType basicType;

    //Text
    [SerializeField] private TMP_Text form1_Text;
    [SerializeField] private TMP_Text form2_Text;
    [SerializeField] private TMP_Text form3_Text;
    [SerializeField] private TMP_Text form4_Text;
    [SerializeField] private TMP_Text form5_Text;

    //color
    [SerializeField] private Image panel1;
    [SerializeField] private Image panel2;
    [SerializeField] private Image panel3;
    [SerializeField] private Image panel4;


    List<BasicType> BasicList = new List<BasicType>();
    // selected photo
    private Sprite sprite;
    // selected letters
    private Basic basic;
    // selected grade
    private string effectName;
    private void Awake()
    {
        gameObject.SetActive(false);
        BasicList.Add(BasicType.Health_Up);
        BasicList.Add(BasicType.EnergyRecovery_Up);
        BasicList.Add(BasicType.EnergyChargeAmount_Up);
        BasicList.Add(BasicType.DashSpeed_Up);
        BasicList.Add(BasicType.Atk_UP);
        BasicList.Add(BasicType.AtkSpeed_Up);
    }

    private void OnEnable()
    {
        RandBasic(form1, form2);
        RandSpecialStat();
        RandSpecial();
    }

    private void RandBasic(Image form1, Image form2)
    {
        for (int i = 0; i < 3;  i++) {
        TypeNomber = Random.Range(0, BasicList.Count);

            if (BasicList.Count == 0)
            {
                sprite = None;
                effectName = "��� ���׷��̵尡 �� �Ǿ����ϴ�.";
            }
            else
            {
                basicType = BasicList[TypeNomber];
                switch (basicType)
                {
                    case BasicType.Health_Up:
                        sprite = Health_UpImage;
                        effectName = "ü�� ����";
                        basic = Health_Up;
                        break;
                    case BasicType.EnergyRecovery_Up:
                        sprite = EnergyRecovery_UpImage;
                        effectName = "������ ȸ��\n�ӵ� ����";
                        basic = EnergyRecovery_Up;
                        break;
                    case BasicType.EnergyChargeAmount_Up:
                        sprite = EnergyChargeAmount_UpImage;
                        effectName = "������ ������ ����";
                        basic = EnergyChargeAmount_Up;
                        break;
                    case BasicType.DashSpeed_Up:
                        sprite = DashSpeed_UpImage;
                        effectName = "�뽬 �Ÿ���\n�뽬 �ӵ� ����";
                        basic = DashSpeed_Up;
                        break;
                    case BasicType.Atk_UP:
                        sprite = Atk_UPImage;
                        effectName = "���ݷ� ����";
                        basic = Atk_UP;
                        break;
                    case BasicType.AtkSpeed_Up:
                        sprite = AtkSpeed_UpImage;
                        effectName = "���� �ӵ� ����";
                        basic = AtkSpeed_Up;
                        break;
                }
            }
            if (i == 1)
            {
                BasicColor(basic.UpGradeCount, panel1);
                form1.sprite = sprite;
                form1_Text.text = effectName;
            }
            else if (i == 2)
            {
                BasicColor(basic.UpGradeCount, panel2);
                form2.sprite = sprite;
                form2_Text.text = effectName;
            }
        }
    }
    public void BasicChose()
    {
        GameObject clickTarget = EventSystem.current.currentSelectedGameObject;
        switch (clickTarget.name)
        {
            case "Panel":
                Effective(form1_Text.text);
                break;
            case "Panel (1)":
                Effective(form2_Text.text);
                break;
        }
        gameObject.SetActive(false);
    }
    private void Effective(string text)
    {
        Debug.Log("{text} ȿ���۵� ����");
        switch (text)
        {
            case "ü�� ����":
                Health_Up.ChangeInformation(player);
                if (Health_Up.UpGradeCount == 6)
                {
                    Debug.Log("ü�¾� ����");
                    BasicList.Remove(BasicType.Health_Up);
                }
                break;
            case "������ ȸ��\n�ӵ� ����":
                EnergyRecovery_Up.ChangeInformation(player);
                if (EnergyRecovery_Up.UpGradeCount == 6)
                {
                    Debug.Log("������ ȸ�� �ӵ� ����");
                    BasicList.Remove(BasicType.EnergyRecovery_Up);
                }
                break;
            case "������ ������ ����":
                EnergyChargeAmount_Up.ChangeInformation(player);
                if (EnergyChargeAmount_Up.UpGradeCount == 6)
                {
                    Debug.Log("������ �ִ� �� ����");
                    BasicList.Remove(BasicType.EnergyChargeAmount_Up);
                }
                break;
            case "�뽬 �Ÿ���\n�뽬 �ӵ� ����":
                DashSpeed_Up.ChangeInformation(player);
                if (DashSpeed_Up.UpGradeCount == 6)
                {
                    Debug.Log("�뽬 �ִ� ��Ÿ� ����");
                    BasicList.Remove(BasicType.DashSpeed_Up);
                }
                break;
            case "���ݷ� ����":
                Atk_UP.ChangeInformation(player);
                if (Atk_UP.UpGradeCount == 6)
                {
                    Debug.Log("���ݷ� ���� ����");
                    BasicList.Remove(BasicType.Atk_UP);
                }
                break;
            case "���� �ӵ� ����":
                AtkSpeed_Up.ChangeInformation(player);
                if (AtkSpeed_Up.UpGradeCount == 6)
                {
                    Debug.Log("���� ����");
                    BasicList.Remove(BasicType.AtkSpeed_Up);
                }
                break;
            default:
                break;
        }

    }

    private void RandSpecialStat()
    {

    }

    private void RandSpecial()
    {

    }

    private void BasicColor(int Grade, Image panel)
    {
        switch(Grade)
        {
            case 1:
                panel.color = Color.gray;
                break;
            case 2:
                panel.color = Color.blue;
                break;
            case 3:
                panel.color = Color.magenta;
                break;
            case 4:
                panel.color = Color.yellow;
                break;
            case 5:
                panel.color = Color.red;
                break;
        }
    }
}
